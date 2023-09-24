using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.trashpandaboy.core.Pooling
{
    public class PoolsManager : Manager<PoolsManager>
    {
        [SerializeField] GameObject _objectToolPrefab;

        protected override void Awake()
        {
            _pools = new Dictionary<GameObject, ObjectPool>();
            base.Awake();
        }

        Dictionary<GameObject, ObjectPool> _pools;

        public ObjectPool GetObjectPool(GameObject _prefabOfPool)
        {
            if (!_pools.ContainsKey(_prefabOfPool))
            {
                var goPool = Instantiate(_objectToolPrefab, transform.position, Quaternion.identity);
                var objectPoolInstance = goPool.GetComponent<ObjectPool>();
                objectPoolInstance.Setup(_prefabOfPool);
                _pools[_prefabOfPool] = objectPoolInstance;
            }

            return _pools[_prefabOfPool];
        }

        public ObjectPool GetObjectPoolOfType(Type gameobjectType)
        {
            foreach (var pool in _pools)
            {
                if (pool.Key.GetType() == gameobjectType)
                    return pool.Value;
            }
            return null;
        }

        public void ReleaseGameobject(GameObject objToRelease)
        {
            foreach (var kvp in _pools)
            {
                if (objToRelease.GetType() == kvp.Key.GetType())
                {
                    kvp.Value.ReleaseGameobject(objToRelease);
                    break;
                }
            }
        }

    }
}