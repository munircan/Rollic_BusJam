using UnityEngine;

namespace _Main.Patterns.ServiceLocation
{
    public class ServiceManager : MonoBehaviour
    {
        private void Awake()
        {
            RegisterServices();
        }


        private void RegisterServices()
        {
        }


        private void UnregisterServices()
        {
        }

        private void OnDestroy()
        {
            UnregisterServices();
        }
    }
}