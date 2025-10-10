using _Main.GamePlay.TileSystem.Manager;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.SlotSystem;
using UnityEngine;

namespace _Main.Patterns.ServiceLocation
{
    // EXECUTION ORDER IS -1
    public class ServiceManager : MonoBehaviour
    {
        [SerializeField] private TileManager _tileManager;
        [SerializeField] private PersonManager _personManager;
        [SerializeField] private SlotManager  _slotManager;

        private void Awake()
        {
            RegisterServices();
        }

        /// <summary>
        /// Registers services into the ServiceLocator.
        /// </summary>
        private void RegisterServices()
        {
            ServiceLocator.RegisterService(_personManager);
            ServiceLocator.RegisterService(_tileManager);
            ServiceLocator.RegisterService(_slotManager);
        }

        /// <summary>
        /// Unregisters services from the ServiceLocator.
        /// </summary>
        private void UnregisterServices()
        {
            ServiceLocator.UnregisterService<PersonManager>();
            ServiceLocator.UnregisterService<TileManager>();
            ServiceLocator.UnregisterService<SlotManager>();
        }

        private void OnDestroy()
        {
            UnregisterServices();
        }
    }
}