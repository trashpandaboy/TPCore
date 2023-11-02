using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.trashpandaboy.core
{
    /// <summary>
    /// Purpose of the ServiceLocator is to avoid multiple Singleton in project
    /// ServiceLocator will be in charge of store references and give the correct one
    /// when someone ask for it
    /// Each class that need to be accessed will need to Add itself to the ServiceLocator and
    /// remove itself when his lifecycle will end
    /// </summary>
    public class ServiceLocator : MonoBehaviour
    {
        #region Singleton

        public static ServiceLocator Instance => _instance;
        protected static ServiceLocator _instance;

        #endregion

        /// <summary>
        /// Contains all references to Services added
        /// </summary>
        Dictionary<Type, object> _servicesIntances;


        /// <summary>
        /// Implement Singleton
        /// </summary>
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }

        /// <summary>
        /// Add the specified reference to the availables Services
        /// </summary>
        /// <typeparam name="T">Generic Type to handle multiple types</typeparam>
        /// <param name="serviceToAdd">The instance reference to add</param>
        public void AddService<T>(T serviceToAdd) where T : class
        {
            if (_servicesIntances == null)
            {
                _servicesIntances = new Dictionary<Type, object>();
            }

            if (_servicesIntances.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"ServiceLocator already has a reference for type '{typeof(T)}', can't add it...");
                return;
            }

            _servicesIntances[typeof(T)] = serviceToAdd;
        }

        /// <summary>
        /// Remove the reference for specified type
        /// </summary>
        /// <typeparam name="T">Type of reference to remove</typeparam>
        public void RemoveService<T>()
        {
            if (!_servicesIntances.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"ServiceLocator has no reference for type '{typeof(T)}', can't remove it...");
                return;
            }

            _servicesIntances.Remove(typeof(T));
        }

        /// <summary>
        /// Retrieve the reference of specified type, if exist
        /// </summary>
        /// <typeparam name="T">Type of reference to retrieve</typeparam>
        /// <returns></returns>
        public T GetServiceInstance<T>() where T : class
        {
            if (_servicesIntances == null)
                return null;

            if (!_servicesIntances.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"ServiceLocator has no reference for type '{typeof(T)}'");
                return null;
            }

            return _servicesIntances[typeof(T)] as T;
        }
    }
}