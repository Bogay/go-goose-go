using UnityEngine;
using Zenject;

namespace GoGooseGo
{
    [CreateAssetMenu(fileName = "StageInstaller", menuName = "Installers/StageInstaller")]
    public class StageInstaller : ScriptableObjectInstaller<StageInstaller>
    {
        public PlayerData playerData;
        public ItemCollection defaultItems;

        public override void InstallBindings()
        {
            this.playerData.Init();
            Container.BindInstance(this.playerData).AsSingle();
            var player = FindObjectOfType<PlayerControl>();
            Container.BindInstance(player).AsSingle();
            // Clone a deafult item collection
            Container.BindInstance(GameObject.Instantiate(this.defaultItems));
        }
    }
}