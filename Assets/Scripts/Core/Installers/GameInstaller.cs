using GameScripts;
using Zenject;

namespace Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Game>().AsSingle();
        }
    }
}