using Core;
using Core.Installers;

namespace GameScripts.Installers
{
    public class GameApplicationInstaller : ApplicationInstaller
    {
        protected override void BindClasses()
        {
            Container.BindInterfacesTo<GameApplicationController>().AsSingle();
            Container.Bind<MonoEventsBroadcaster>().FromComponentInHierarchy().AsSingle();
        }
    }
}