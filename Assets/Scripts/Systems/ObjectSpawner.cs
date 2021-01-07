using System;
using System.Collections.Generic;
using DefaultNamespace.Interfaces;
using Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace.Systems
{
    public class ObjectSpawner : IGameSystem
    {
        private Dictionary<Type, List<IPoolable>> pools = new Dictionary<Type, List<IPoolable>>();

        public void Init()
        {
        }

        public void Start()
        {
        }

        public void Destroy()
        {
        }

        public T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : Object, IPoolable
        {
            var type = typeof(T);
            if (pools.TryGetValue(type, out var list) && list.Count > 0)
            {
                var item = list.Shift();
                item.gameObject.SetActive(true);
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.transform.parent = parent;
                return (T) item;
            }

            return Object.Instantiate(prefab, position, rotation, parent);
        }

        public void Destroy<T>(T item) where T : IPoolable
        {
            item.gameObject.SetActive(false);
            item.Reset();
            var type = typeof(T);
            if (pools.TryGetValue(type, out var list))
            {
                list.Add(item);
            }
            else
            {
                pools[type] = new List<IPoolable> {item};
            }
        }
    }
}