using UnityEngine;

namespace com.trashpandaboy.core
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance
        {
            get { return _instance; }
        }
        static Manager _instance = null;

        protected virtual void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }
    }
}
