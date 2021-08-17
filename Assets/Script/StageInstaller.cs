using UnityEngine;
using Zenject;

namespace GoGooseGo
{
    [CreateAssetMenu(fileName = "StageInstaller", menuName = "Installers/StageInstaller")]
    public class StageInstaller : ScriptableObjectInstaller<StageInstaller>
    {
        public PlayerData playerData;
        public ItemCollection defaultItems;
        public GameObject itemSlot;
        // It is disabled by default, so `FindObjectOfType` can not find it
        public GameObject restartMenu;

        public override void InstallBindings()
        {
            Container.BindInstance(new GameState()).AsSingle();
            this.playerData.Init();
            Container.BindInstance(this.playerData).AsSingle();
            var player = FindObjectOfType<PlayerControl>();
            Container.BindInstance(player).AsSingle();
            // Clone a deafult item collection
            Container.BindInstance(GameObject.Instantiate(this.defaultItems).Init());
            Container.Bind<GameObject>().WithId("ItemSlot").FromInstance(this.itemSlot);
            Container.Bind<GameObject>().WithId("RestartMenu").FromInstance(this.restartMenu);
        }
    }
}