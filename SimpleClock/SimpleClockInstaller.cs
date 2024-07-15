using Zenject;

namespace SimpleClock
{
    public class SimpleClockInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SimpleClockController>().AsSingle();
        }
    }
}
