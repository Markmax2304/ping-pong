using UnityEngine;
using Zenject;

namespace Pong
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public Player.Settings playerSettings;
        public Ball.Settings ballSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(playerSettings).IfNotBound();
            Container.BindInstance(ballSettings).IfNotBound();
        }
    }
}