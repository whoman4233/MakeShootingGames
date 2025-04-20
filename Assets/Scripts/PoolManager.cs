using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Singleton;
using Chapter.ObjectPool;
using UnityEditor;

namespace Chapter.ObjectPool
{
    public class PoolManager : Singleton<PoolManager>
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int capacity = 10;
            public int MaxSize = 20;
        }

        [Header("Ç® ¼³Á¤")]
        public List<Pool> pools;

        private Dictionary<string, object> poolDict;

        private void Start()
        {
            poolDict = new Dictionary<string, object>();

            foreach (var pool in pools)
            {
                var type = pool.prefab.GetComponent<Component>().GetType();
                var genericType = typeof(GenericObjectPool<>).MakeGenericType(type);
                var instance = System.Activator.CreateInstance(genericType, pool.prefab, pool.capacity, pool.MaxSize);
                poolDict.Add(pool.tag, instance);
            }
        }

        public T Get<T>(string tag) where T : Component
        {
            if (!poolDict.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist");
                return null;
            }

            var objectPool = poolDict[tag] as GenericObjectPool<T>;
            return objectPool?.Get();
        }

        public void Release<T>(string tag, T obj) where T : Component
        {
            if (!poolDict.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist");
                return;
            }

            var objectPool = poolDict[tag] as GenericObjectPool<T>;
            objectPool?.Release(obj);
        }
    }
}
