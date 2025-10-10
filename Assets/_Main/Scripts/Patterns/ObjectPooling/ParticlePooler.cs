using System;
using System.Collections.Generic;
using _Main.Patterns.Singleton;
using UnityEngine;

namespace _Main.Patterns.ObjectPooling
{
    public class ParticlePooler : SingletonMonoBehaviour<ParticlePooler>
    {
        [Serializable]
        public class ParticlePool
        {
            public string tag;
            public Queue<ParticleSystem> pooledParticles;
            public ParticleSystem pooledParticle;
            public int poolCount;
        }

        private Dictionary<string, ParticlePool> particlePoolDictionary = new Dictionary<string, ParticlePool>();
        public List<ParticlePool> particlePool = new List<ParticlePool>();

        private void Awake()
        {
            GenerateAllPool();
        }

        private void GenerateAllPool()
        {
            for (int i = 0; i < particlePool.Count; i++)
            {
                particlePool[i].pooledParticles = new Queue<ParticleSystem>();
                particlePoolDictionary.Add(particlePool[i].tag, particlePool[i]);
                GenerateObjects(particlePool[i]);
            }
        }

        private void GenerateObjects(ParticlePool pool)
        {
            for (int i = 0; i < pool.poolCount; i++)
            {
                var spawnedPoolCard = Instantiate(pool.pooledParticle, Vector3.zero, Quaternion.identity,
                    this.gameObject.transform);
                spawnedPoolCard.gameObject.SetActive(false);
                pool.pooledParticles.Enqueue(spawnedPoolCard);
            }
        }

        #region Get pooled particles methods with changing color property

        public ParticleSystem GetPooledParticle(string particleTag, Vector3 position, Quaternion rotation,
            Vector3 scale, bool activate, bool shouldPlay, Color color)
        {
            // if(poolIndex > particlePool.Count) {Debug.Log("<color=#FF0000><b>Out of Range!</b></color>");
            //     return null;}

            var selectedObjectPool = particlePoolDictionary[particleTag].pooledParticles;
            if (selectedObjectPool.Count > 0)
            {
                var usedParticle = selectedObjectPool.Dequeue();

                while (usedParticle.gameObject.activeSelf)
                {
                    selectedObjectPool.Enqueue(usedParticle);
                    usedParticle = selectedObjectPool.Dequeue();
                }

                usedParticle.transform.position = position;
                usedParticle.transform.rotation = rotation;
                usedParticle.transform.localScale = scale;
                usedParticle.gameObject.SetActive(activate);
                ChangeParticleColor(usedParticle, color);
                selectedObjectPool.Enqueue(usedParticle);

                if (shouldPlay)
                    usedParticle.Play();

                return usedParticle;
            }
            else
            {
                Debug.Log("<color=#8E44AD>Particle</color> pool is <color=#FF0000><b>empty!</b></color>");
                return null;
            }
        }

        public ParticleSystem GetPooledParticle(string particleTag, Vector3 position, Quaternion rotation,
            bool activate, bool shouldPlay, Color color)
        {
            // if(poolIndex > particlePool.Count) {Debug.Log("<color=#FF0000><b>Out of Range!</b></color>");
            //     return null;}

            var selectedObjectPool = particlePoolDictionary[particleTag].pooledParticles;
            if (selectedObjectPool.Count > 0)
            {
                var usedParticle = selectedObjectPool.Dequeue();

                while (usedParticle.gameObject.activeSelf)
                {
                    selectedObjectPool.Enqueue(usedParticle);
                    usedParticle = selectedObjectPool.Dequeue();
                }

                usedParticle.transform.position = position;
                usedParticle.transform.rotation = rotation;
                usedParticle.gameObject.SetActive(activate);

                ChangeParticleColor(usedParticle, color);
                selectedObjectPool.Enqueue(usedParticle);

                if (shouldPlay)
                    usedParticle.Play();

                return usedParticle;
            }
            else
            {
                Debug.Log("<color=#8E44AD>Particle</color> pool is <color=#FF0000><b>empty!</b></color>");
                return null;
            }
        }

        public ParticleSystem GetPooledParticle(string particleTag, Vector3 position, bool activate, bool shouldPlay,
            Color color)
        {
            // if(poolIndex > particlePool.Count) {Debug.Log("<color=#FF0000><b>Out of Range!</b></color>");
            //     return null;}

            var selectedObjectPool = particlePoolDictionary[particleTag].pooledParticles;
            if (selectedObjectPool.Count > 0)
            {
                var usedParticle = selectedObjectPool.Dequeue();

                while (usedParticle.gameObject.activeSelf)
                {
                    selectedObjectPool.Enqueue(usedParticle);
                    usedParticle = selectedObjectPool.Dequeue();
                }

                usedParticle.transform.position = position;
                usedParticle.gameObject.SetActive(activate);
                ChangeParticleColor(usedParticle, color);
                selectedObjectPool.Enqueue(usedParticle);

                if (shouldPlay)
                    usedParticle.Play();

                return usedParticle;
            }
            else
            {
                Debug.Log("<color=#8E44AD>Particle</color> pool is <color=#FF0000><b>empty!</b></color>");
                return null;
            }
        }

        #endregion


        #region Get pooled particles methods (without changing color property)

        public ParticleSystem GetPooledParticle(string particleTag, Vector3 position, Quaternion rotation,
            bool activate, bool shouldPlay)
        {
            // if(poolIndex > particlePool.Count) {Debug.Log("<color=#FF0000><b>Out of Range!</b></color>");
            //     return null;}

            var selectedObjectPool = particlePoolDictionary[particleTag].pooledParticles;
            if (selectedObjectPool.Count > 0)
            {
                var usedParticle = selectedObjectPool.Dequeue();
                while (usedParticle.gameObject.activeSelf)
                {
                    selectedObjectPool.Enqueue(usedParticle);
                    usedParticle = selectedObjectPool.Dequeue();
                }

                usedParticle.transform.position = position;
                usedParticle.transform.rotation = rotation;
                usedParticle.gameObject.SetActive(activate);
                selectedObjectPool.Enqueue(usedParticle);

                if (shouldPlay)
                    usedParticle.Play();

                return usedParticle;
            }
            else
            {
                Debug.Log("<color=#8E44AD>Particle</color> pool is <color=#FF0000><b>empty!</b></color>");
                return null;
            }
        }

        public ParticleSystem GetPooledParticle(string particleTag, Vector3 position, bool activate, bool shouldPlay)
        {
            // if(poolIndex > particlePool.Count) {Debug.Log("<color=#FF0000><b>Out of Range!</b></color>");
            //     return null;}

            var selectedObjectPool = particlePoolDictionary[particleTag].pooledParticles;
            ;
            if (selectedObjectPool.Count > 0)
            {
                var usedParticle = selectedObjectPool.Dequeue();
                usedParticle.transform.position = position;
                usedParticle.gameObject.SetActive(activate);
                selectedObjectPool.Enqueue(usedParticle);

                if (shouldPlay)
                    usedParticle.Play();

                return usedParticle;
            }
            else
            {
                Debug.Log("<color=#8E44AD>Particle</color> pool is <color=#FF0000><b>empty!</b></color>");
                return null;
            }
        }

        public ParticleSystem GetPooledParticle(string particleTag, Vector3 position, Quaternion rotation,
            Vector3 scale, bool activate, bool shouldPlay)
        {
            // if(poolIndex > particlePool.Count) {Debug.Log("<color=#FF0000><b>Out of Range!</b></color>");
            //     return null;}

            var selectedObjectPool = particlePoolDictionary[particleTag].pooledParticles;
            ;
            if (selectedObjectPool.Count > 0)
            {
                var usedParticle = selectedObjectPool.Dequeue();
                usedParticle.transform.position = position;
                usedParticle.transform.rotation = rotation;
                usedParticle.transform.localScale = scale;
                usedParticle.gameObject.SetActive(activate);
                selectedObjectPool.Enqueue(usedParticle);

                if (shouldPlay)
                    usedParticle.Play();

                return usedParticle;
            }
            else
            {
                Debug.Log("<color=#8E44AD>Particle</color> pool is <color=#FF0000><b>empty!</b></color>");
                return null;
            }
        }

        #endregion

        private void ChangeParticleColor(ParticleSystem particle, Color color)
        {
            ParticleSystem.MainModule mainSettings = particle.main;
            mainSettings.startColor = new ParticleSystem.MinMaxGradient(color);

            ParticleSystem[] particles = particle.GetComponentsInChildren<ParticleSystem>();

            for (int i = 0; i < particles.Length; i++)
            {
                mainSettings = particles[i].main;
                mainSettings.startColor = new ParticleSystem.MinMaxGradient(color);
            }
        }
    }
}