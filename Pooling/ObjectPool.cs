using System.Collections.Generic;
using UnityEngine;

namespace com.trashpandaboy.core.Pooling
{
    /// <summary>
    /// Represent a generic ObjectPool that will let you implement Pooling of any kind of Prefab
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        /// <summary>
        /// Prefab of the pool
        /// </summary>
        GameObject _prefab;
        /// <summary>
        /// List of all the object contained in the pool
        /// </summary>
        [SerializeField] List<GameObject> _pool;

        /// <summary>
        /// Setup the Pool with the given gameobject's Prefab and with a starting pool size
        /// </summary>
        /// <param name="prefab">GameObject's Prefab of the pool</param>
        /// <param name="poolSize">Starting pool size</param>
        public void Setup(GameObject prefab, int poolSize = 5)
        {
            _prefab = prefab;
            _pool = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < poolSize; i++)
            {
                tmp = Instantiate(_prefab, transform);
                tmp.SetActive(false);
                _pool.Add(tmp);
            }
        }

        /// <summary>
        /// Return the reference of a GameObject contained in the pool
        /// </summary>
        /// <returns>An available GameObject</returns>
        public GameObject ProvideGameobject()
        {
            GameObject toReturn = null;

            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].activeInHierarchy)
                {
                    toReturn = _pool[i];
                    break;
                }
            }

            if (toReturn == null)
            {
                var newInstance = Instantiate(_prefab, transform);
                _pool.Add(newInstance);
                toReturn = newInstance;
            }
            return toReturn;
        }

        /// <summary>
        /// Release the GameObject back in the pool and set it back as available
        /// </summary>
        /// <param name="gameObject">GameObject that will be set as available in the pool</param>
        public void ReleaseGameobject(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}