using IPA;
using IPALogger = IPA.Logging.Logger;
using SiraUtil.Zenject;

namespace SimpleClock
{
    [Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }


        [Init]
        public Plugin(IPALogger logger, Zenjector zenject)
        {
            Instance = this;
            Log = logger;
            Log.Info("SimpleClock initialized.");

            zenject.UseLogger(logger);
            zenject.UseMetadataBinder<Plugin>();

            //Install zenject installers
            zenject.Install<SimpleClockInstaller>(Location.Menu);
            zenject.Install<SimpleClockInstaller>(Location.Player);
            zenject.Install<SimpleClockInstaller>(Location.Tutorial);
        }
    }
}
