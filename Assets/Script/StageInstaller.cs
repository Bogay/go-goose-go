using UnityEngine;
using Zenject;

namespace GoGooseGo
{
    [CreateAssetMenu(fileName = "StageInstaller", menuName = "Installers/StageInstaller")]
    public class StageInstaller : ScriptableObjectInstaller<StageInstaller>
    {
        public PlayerData playerData;
        public override void InstallBindings()
        {
            Container.BindInstance(this.playerData).AsSingle();
        }
    }
}