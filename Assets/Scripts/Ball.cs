using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Zenject;
using Mirror;

using Pong.Movement;
using Pong.Physics;

namespace Pong
{
    public class Ball : NetworkBehaviour, IMovable
    {
        public const string injectId = "ball";

        private Settings settings;
        private IMoveHandler moveHandler;
        private ICollisionHandler collisionHandler;

        private Transform ownTransform;
        private BoxCollider2D ownCollider;
        private Rigidbody2D ownRigidbody;

        private Vector3 moveDirection;
        private LayerMask raycastLayer;
        private RaycastHit2D[] raycastResults = new RaycastHit2D[1];
        private RaycastHit2D HitResult => raycastResults[0];

        public Vector3 ActualPosition { get; set; }

        [Inject]
        public void Construct(Settings settings, [Inject(Id = injectId)]IMoveHandler moveHandler,
            [Inject(Id = injectId)] ICollisionHandler collisionHandler)
        {
            this.settings = settings;
            this.moveHandler = moveHandler;
            this.collisionHandler = collisionHandler;

        }

        public override void OnStartServer()
        {
            GameInstaller.GameContainer.Inject(this);

            ownTransform = transform;
            ownCollider = GetComponent<BoxCollider2D>();
            ownRigidbody = GetComponent<Rigidbody2D>();

            ownCollider.size = settings.ballSize;
            raycastLayer = LayerMask.GetMask(settings.raycastLayerNames);
            moveHandler.SetPosition(this, ownTransform.position);

            moveDirection = Quaternion.Euler(0f, 0f, 70f * Random.Range(-1f, 1f)) * Vector3.right;
        }

        void FixedUpdate()
        {
            if (!isServer)
                return;

            float moveForce = settings.moveSpeed * Time.fixedDeltaTime;

            int hitCount = Physics2D.BoxCastNonAlloc(ownTransform.position, settings.ballSize, 0f, moveDirection, raycastResults, moveForce, raycastLayer);

            if (hitCount > 0)
                collisionHandler.SolveCollision(HitResult, ref moveDirection, ref moveForce);

            moveHandler.MovePosition(this, moveDirection * moveForce);
            Debug.DrawRay(ownTransform.position, moveDirection, Color.red);
        }

        public void MoveTo(Vector3 pos)
        {
            ownRigidbody.MovePosition(pos);
        }

        [System.Serializable]
        public class Settings
        {
            public float moveSpeed = 5;
            public Vector2 ballSize;
            public string[] raycastLayerNames;
        }
    }
}