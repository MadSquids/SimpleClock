using IPA;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace SimpleClock
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("SimpleClock initialized.");
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            //Delay the start of simpleClock to allow for BSML to start.
            GameObject initObject = new GameObject("SimpleClockInitializer");
            initObject.AddComponent<DelayedInitializer>();
            GameObject.DontDestroyOnLoad(initObject);

            
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }

        private class DelayedInitializer : MonoBehaviour
        {
            private void Start()
            {
                //Invoke after 5 seconds to wait for BSML.
                Invoke(nameof(StartClockController), 10f);
            }

            private void StartClockController()
            {
                Log.Debug("Starting SimpleClockController");
                new GameObject("SimpleClockController").AddComponent<SimpleClockController>();
            }
        }
    }
}
