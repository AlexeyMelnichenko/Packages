using Core.Installers;

namespace GameScripts.Installers
{
    public class GameApplicationInstaller : ApplicationInstaller
    {
        protected override void BindClasses()
        {
            Container.BindInterfacesAndSelfTo<GameApplicationController>().AsSingle();
        }
    }
}