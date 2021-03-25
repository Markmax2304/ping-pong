using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Zenject;
using Mirror;

using Pong.Inputs;
using Pong.Movement;
using Pong.Physics;

namespace Pong 
{
    public class Player : NetworkBehaviour, IMovable
    {
        public const string injectId = "player";
        public const string playerTag = "player";

        private Settings settings;
        private IInputService inputService;
        private IMoveHandler moveHandler;
        private ICollisionHandler collisionHandler;

        private Transform ownTransform;
        private BoxCollider2D ownCollider;
        private Rigidbody2D ownRigidbody;

        private LayerMask raycastLayer;
        private RaycastHit2D[] raycastResults = new RaycastHit2D[1];
        private RaycastHit2D HitResult => raycastResults[0];

        public Vector3 ActualPosition { get; set; }

        [Inject]
        public void Construct(Settings settings, IInputService inputService,
            [Inject(Id = injectId)]IMoveHandler moveHandler, [Inject(Id = injectId)]ICollisionHandler collisionHandler)
        {
            this.settings = settings;
            this.inputService = inputService;
            this.moveHandler = moveHandler;
            this.collisionHandler = collisionHandler;
        }

        public override void OnStartClient()
        {
            GameInstaller.GameContainer.Inject(this);

            ownTransform = transform;
            ownCollider = GetComponent<BoxCollider2D>();
            ownRigidbody = GetComponent<Rigidbody2D>();

            ownCollider.size = settings.racketSize;
            raycastLayer = LayerMask.GetMask(settings.raycastLayerNames);
            moveHandler.SetPosition(this, ownTransform.position);
        }

        void FixedUpdate()
        {
            if (!isLocalPlayer)
                return;

            if (inputService.GetVerticalInput(out float verticalInput))
            {
                Vector3 moveDir = Vector3.up * Mathf.Sign(verticalInput);
                float moveForce = Mathf.Abs(verticalInput) * settings.moveSpeed * Time.fixedDeltaTime;

                Vector2 originPos = (Vector2)ownTransform.position + (Vector2.up * settings.racketSize.y / 2f) * Mathf.Sign(verticalInput);
                int hitCount = Physics2D.RaycastNonAlloc(originPos, moveDir, raycastResults, moveForce, raycastLayer);

                // NOTE: such as movement implemented with rigidbody, collision handler becomes quite useless
                // BUT if we want to change collision logic this piece of code will be usefull
                if (hitCount > 0)
                    collisionHandler.SolveCollision(HitResult, ref moveDir, ref moveForce);

                moveHandler.MovePosition(this, moveDir * moveForce);
            }
        }

        public void MoveTo(Vector3 pos)
        {
            ownRigidbody.MovePosition(pos);
        }

        [System.Serializable]
        public class Settings
        {
            public float moveSpeed = 10;
            public Vector2 racketSize;
            public string[] raycastLayerNames;
        }
    }
}