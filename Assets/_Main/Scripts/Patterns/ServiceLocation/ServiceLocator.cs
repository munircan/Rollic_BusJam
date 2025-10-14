using System;
using System.Collections.Generic;
// using UnityEngine;

namespace _Main.Scripts.Patterns.ServiceLocation
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void RegisterService<T>(T service) where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                // Debug.LogWarning($"{type} already registered.");
                return;
            }

            _services[type] = service;
        }
        
        public static bool TryRegisterService<T>(T service) where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                return false;
            }

            _services[type] = service;
            return true;
        }
        
        public static T GetService<T>() where T : class
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return service as T;
            }

            // Debug.LogError($"{type} is not registered in the service locator.");
            return null;
        }
        
        public static bool TryGetService<T>(out T service) where T : class
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var foundService))
            {
                service = foundService as T;
                return true;
            }

            // Debug.LogError($"{type} is not registered in the service locator.");
            service = null;
            return false;
        }
        
        public static bool IsRegistered<T>() where T : class
        {
            return _services.ContainsKey(typeof(T));
        }

        public static void UnregisterService<T>() where T : class
        {
            var type = typeof(T);
            _services.Remove(type);
        }
    }
}