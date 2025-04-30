using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Chapter.ObjectPool
{
    public class BaseObjectPool<T> where T : Component, IPoolable
    {
        private readonly IObjectPool<T> pool;
        private readonly GameObject prefab;

        public BaseObjectPool(GameObject prefab, int capacity = 10, int maxSize = 20)
        {
            this.prefab = prefab;
            pool = new ObjectPool<T>(
                CreatePooledItem,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                true, capacity, maxSize);
        }

        private T CreatePooledItem()
        {
            var obj = GameObject.Instantiate(prefab);
            return obj.GetComponent<T>();
        }

        private void OnTakeFromPool(T obj)
        {
            obj.OnGet();
            obj.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(T obj)
        {
            if(obj != null)
            {
                obj.OnRelease();
                obj.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("풀에 반환된 오브젝트가 null입니다!");
            }
        }

        private void OnDestroyPoolObject(T obj)
        {
            GameObject.Destroy(obj.gameObject);
        }

        public T Get() => pool.Get();
        public void Release(T obj) => pool.Release(obj);
    }
}
