namespace _Main.Scripts.Patterns.Singleton
{
    public class Singleton<T> where T : new()
    {
        private static T _instance;
        private static object _lock = new object();
        private static bool _hasInstanceSet;

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_hasInstanceSet) return _instance;
                    
                    _instance = new T();
                    _hasInstanceSet = true;

                    return _instance;
                }
            }
        }
    }
}