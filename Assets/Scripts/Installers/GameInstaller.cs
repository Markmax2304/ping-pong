using UnityEngine;
using Zenject;

using Pong.Inputs;
using Pong.Movement;
using Pong.Physics;

namespace Pong
{
    public class GameInstaller : MonoInstaller
    {
        public static DiContainer GameContainer { get; private set; }

        [SerializeField] private Ball ball;

        public override void InstallBindings()
        {
            GameContainer = Container;

            Container.BindInstance<Ball>(ball).AsSingle();

            Container.Bind<IInputService>().To<KeyboardInputService>().AsSingle();
            Container.Bind<IMoveHandler>().WithId(Player.injectId).To<SnapMoveHandler>().AsTransient();
            Container.Bind<IMoveHandler>().WithId(Ball.injectId).To<SmoothMoveHandler>().AsTransient();
            Container.Bind<ICollisionHandler>().WithId(Player.injectId).To<StopperCollisionHandler>().AsTransient();
            Container.Bind<ICollisionHandler>().WithId(Ball.injectId).To<PongBallCollisionHandler>().AsTransient();
        }
    }
}
