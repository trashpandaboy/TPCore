using UnityEngine;

namespace com.trashpandaboy.core
{
    public class Manager<T> : MonoBehaviour where T : Manager<T>
    {
        [SerializeField]
        bool UseSingleton = false;

        public static T Instance => _instance;
        protected static T _instance;

        protected virtual void Awake()
        {
            if(UseSingleton)
            {
                if(_instance != null && _instance != (T)this)
                {
                    Destroy(this);
                }
                else
                {
                    _instance = (T)this;
                }
            }
        }
    }
}
