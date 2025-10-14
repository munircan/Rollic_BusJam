using _Main.Scripts.GamePlay.BusSystem.Manager;
using _Main.Scripts.GamePlay.PersonSystem.Manager;
using _Main.Scripts.GamePlay.SlotSystem.Manager;
using _Main.Scripts.GamePlay.TileSystem.Manager;
using _Main.Scripts.Patterns.ServiceLocation;
using UnityEngine;

namespace _Main.Scripts.GamePlay.ServiceManager
{
    // EXECUTION ORDER IS -1
    public class ServiceManager : MonoBehaviour
    {
        [SerializeField] private TileManager _tileManager;
        [SerializeField] private PersonManager _personManager;
        [SerializeField] private SlotManager  _slotManager;
        [SerializeField] private BusManager _busManager;

        private void Awake()
        {
            RegisterServices();
        }
        
        private void RegisterServices()
        {
            ServiceLocator.RegisterService(_personManager);
            ServiceLocator.RegisterService(_tileManager);
            ServiceLocator.RegisterService(_slotManager);
            ServiceLocator.RegisterService(_busManager);
        }
        
        private void UnregisterServices()
        {
            ServiceLocator.UnregisterService<PersonManager>();
            ServiceLocator.UnregisterService<TileManager>();
            ServiceLocator.UnregisterService<SlotManager>();
            ServiceLocator.UnregisterService<BusManager>();
        }

        private void OnDestroy()
        {
            UnregisterServices();
        }
    }
}