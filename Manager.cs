using UnityEngine;

namespace com.trashpandaboy.core
{
    public class Manager<T> : MonoBehaviour where T : Manager<T>
    {
        public static T Instance
        {
            get { return _instance; }
        }
        protected static T _instance;

        protected virtual void Awake()
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
