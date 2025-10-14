using System;
using System.Collections.Generic;
using _Main.Scripts.Patterns.Singleton;
using UnityEngine;
using UnityEngine.Pool;

namespace _Main.Scripts.Patterns.ObjectPooling
{
    public class ObjectPooler : SingletonMonoBehaviour<ObjectPooler>
    {
        [SerializeField] private List<Pool> objectPools;
        [SerializeField] private List<Pool<MonoBehaviour>> scriptPools;

        private Dictionary<string, ObjectPool<GameObject>> objectPoolDictionary;
        private Dictionary<string, ObjectPool<MonoBehaviour>> scriptPoolDictionary;

        #region PoolClasses

        [Serializable]
        public class Pool
        {
            [Tooltip("Give a tag to the pool for calling it")]
            public string tag;

            [Tooltip("Prefab of the GameObject to be pooled")]
            public GameObject prefab;

            [Tooltip("The size (count) of the pool")]
            public int softCap, hardCap;

            [Tooltip("Whether the GameObject create at Start")]
            public bool createAtStart;
        }

        [Serializable]
        public class Pool<T> where T : MonoBehaviour
        {
            [Tooltip("Give a tag to the pool for calling it")]
            public string tag;

            [Tooltip("Prefab of the GameObject to be pooled")]
            public T prefab;

            [Tooltip("The size (count) of the pool")]
            public int softCap, hardCap;

            [Tooltip("Whether the GameObject create at Start")]
            public bool createAtStart;
        }

        #endregion

        private void Awake()
        {
            Initialize();
            CreateAtStart();
        }

        private void Initialize()
        {
            objectPoolDictionary = new Dictionary<string, ObjectPool<GameObject>>();
            var objectPoolsCount = objectPools.Count;
            for (int i = 0; i < objectPoolsCount; i++)
            {
                ObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreateFunction(i), OnGameObjectGet,
                    OnGameObjectRelease, OnGameObjectDestroy, true, objectPools[i].softCap, objectPools[i].hardCap);
                string prefabName = objectPools[i].tag;
                objectPoolDictionary.Add(prefabName, pool);
            }

            scriptPoolDictionary = new Dictionary<string, ObjectPool<MonoBehaviour>>();
            var scriptPoolCount = scriptPools.Count;
            for (int i = 0; i < scriptPoolCount; i++)
            {
                ObjectPool<MonoBehaviour> pool = new ObjectPool<MonoBehaviour>(CreateFunctionSc(i), OnGameObjectGet,
                    OnGameObjectRelease, OnGameObjectDestroy, true, scriptPools[i].softCap, scriptPools[i].hardCap);
                string prefabName = scriptPools[i].tag;
                scriptPoolDictionary.Add(prefabName, pool);
            }
        }

        private void CreateAtStart()
        {
            for (int i = 0; i < objectPools.Count; i++)
            {
                var p = objectPools[i];
                if (p.createAtStart)
                {
                    for (int j = 0; j < p.softCap; j++)
                    {
                        objectPoolDictionary[p.tag].Get();
                    }
                }
            }

            for (int i = 0; i < scriptPools.Count; i++)
            {
                var p = scriptPools[i];
                if (p.createAtStart)
                {
                    for (int j = 0; j < p.softCap; j++)
                    {
                        scriptPoolDictionary[p.tag].Get();
                    }
                }
            }
        }

        #region GameObjectSpawningMethods

        /// <summary>
        /// Spawns the pooled GameObject to given position
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="position">Set the world position of the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public GameObject Spawn(string poolTag, Vector3 position)
        {
            var pooledObject = objectPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.position = position;
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        /// <summary>
        /// Spawns the pooled GameObject to given position and rotation
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="position">Set the world position of the GameObject</param>
        /// <param name="rotation">Set the rotation of the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public GameObject Spawn(string poolTag, Vector3 position, Quaternion rotation)
        {
            var pooledObject = objectPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.position = position;
            t.rotation = rotation;
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        /// <summary>
        /// Spawns the pooled GameObject and parents the GameObject to given Transform
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="parent">Parent that will be assigned to the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public GameObject Spawn(string poolTag, Transform parent)
        {
            var pooledObject = objectPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.transform.parent = parent;
            t.localPosition = Vector3.zero;
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        /// <summary>
        /// Spawns the pooled GameObject to given position and parents the GameObject to given Transform
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="position">Set the world position of the GameObject</param>
        /// <param name="parent">Parent that will be assigned to the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public GameObject Spawn(string poolTag, Vector3 position, Transform parent)
        {
            var pooledObject = objectPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.position = position;
            t.SetParent(parent);
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        /// <summary>
        /// Spawns the pooled GameObject to given position and rotation and parents the GameObject to given Transform
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="position">Set the world position of the GameObject</param>
        /// <param name="rotation">Set the rotation of the GameObject</param>
        /// <param name="parent">Parent that will be assigned to the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public GameObject Spawn(string poolTag, Vector3 position, Quaternion rotation, Transform parent)
        {
            var pooledObject = objectPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.position = position;
            t.rotation = rotation;
            t.SetParent(parent);
            pooledObject.SetActive(true);
            return pooledObject;
        }

        #endregion

        #region ScriptSpawningMethods

        /// <summary>
        /// Spawns the pooled GameObject to given position
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="position">Set the world position of the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public T SpawnSc<T>(string poolTag, Vector3 position) where T : MonoBehaviour
        {
            var pooledObject = scriptPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.position = position;
            pooledObject.gameObject.SetActive(true);
            return (T)pooledObject;
        }

        /// <summary>
        /// Spawns the pooled GameObject to given position and rotation
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="position">Set the world position of the GameObject</param>
        /// <param name="rotation">Set the rotation of the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public T SpawnSc<T>(string poolTag, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            var pooledObject = scriptPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.position = position;
            t.rotation = rotation;
            pooledObject.gameObject.SetActive(true);
            return (T)pooledObject;
        }

        /// <summary>
        /// Spawns the pooled GameObject and parents the GameObject to given Transform
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="parent">Parent that will be assigned to the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public T SpawnSc<T>(string poolTag, Transform parent) where T : MonoBehaviour
        {
            var pooledObject = scriptPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.transform.parent = parent;
            t.localPosition = Vector3.zero;
            pooledObject.gameObject.SetActive(true);
            return (T)pooledObject;
        }

        /// <summary>
        /// Spawns the pooled GameObject to given position and parents the GameObject to given Transform
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="position">Set the world position of the GameObject</param>
        /// <param name="parent">Parent that will be assigned to the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public T SpawnSc<T>(string poolTag, Vector3 position, Transform parent) where T : MonoBehaviour
        {
            var pooledObject = scriptPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.position = position;
            t.SetParent(parent);
            pooledObject.gameObject.SetActive(true);
            return (T)pooledObject;
        }

        /// <summary>
        /// Spawns the pooled GameObject to given position and rotation and parents the GameObject to given Transform
        /// </summary>
        /// <param name="poolTag">Tag of the GameObject to be spawned</param>
        /// <param name="position">Set the world position of the GameObject</param>
        /// <param name="rotation">Set the rotation of the GameObject</param>
        /// <param name="parent">Parent that will be assigned to the GameObject</param>
        /// <returns>The GameObject found matching the tag specified</returns>
        public T SpawnSc<T>(string poolTag, Vector3 position, Quaternion rotation, Transform parent)
            where T : MonoBehaviour
        {
            var pooledObject = scriptPoolDictionary[poolTag].Get();
            var t = pooledObject.transform;
            t.position = position;
            t.rotation = rotation;
            t.SetParent(parent);
            pooledObject.gameObject.SetActive(true);
            return (T)pooledObject;
        }

        #endregion


        private void OnGameObjectGet(GameObject pooledObject)
        {
            pooledObject.transform.SetParent(transform);
            pooledObject.gameObject.SetActive(false);
        }

        private void OnGameObjectGet(MonoBehaviour pooledObject)
        {
            pooledObject.transform.SetParent(transform);
            pooledObject.gameObject.SetActive(false);
        }

        private Func<GameObject> CreateFunction(int i)
        {
            return new Func<GameObject>(() =>
            {
                var pooledObject = Instantiate(objectPools[i].prefab);
                return pooledObject;
            });
        }

        private Func<MonoBehaviour> CreateFunctionSc(int i)
        {
            return new Func<MonoBehaviour>(() =>
            {
                var pooledObject = Instantiate(this.scriptPools[i].prefab);
                return pooledObject;
            });
        }

        private void OnGameObjectRelease(GameObject pooledObject)
        {
            pooledObject.transform.SetParent(transform);
            pooledObject.SetActive(false);
        }

        private void OnGameObjectRelease(MonoBehaviour pooledObject)
        {
            pooledObject.transform.SetParent(transform);
            pooledObject.gameObject.SetActive(false);
        }

        public void ReleasePooledObject(string poolTag, GameObject pooledObject)
        {
            objectPoolDictionary[poolTag].Release(pooledObject);
        }

        public void ReleasePooledObject(string poolTag, MonoBehaviour pooledObject)
        {
            scriptPoolDictionary[poolTag].Release(pooledObject);
        }

        private void OnGameObjectDestroy(GameObject pooledObject)
        {
            Destroy(pooledObject);
        }

        private void OnGameObjectDestroy(MonoBehaviour pooledObject)
        {
            Destroy(pooledObject);
        }
    }
}