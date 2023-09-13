using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using com.trashpandaboy.core.Utils;

namespace com.trashpandaboy.core
{
    public class EventDispatcher : MonoBehaviour
    {
        /// <summary>
        /// Contains all the available Events
        /// </summary>
        private static Dictionary<string, UnityAction<DataSet>> eventDictionary;

        /// <summary>
        /// Initialize the dictionary
        /// </summary>
        public virtual void Awake()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, UnityAction<DataSet>>();
            }
        }

        /// <summary>
        /// Add a given listening action to a specified event name
        /// </summary>
        /// <param name="eventName">The name of event who will trigger the action</param>
        /// <param name="listeningAction">The action to trigger</param>
        public static void StartListening(string eventName, UnityAction<DataSet> listeningAction)
        {
            UnityAction<DataSet> eventObject;
            if (eventDictionary.TryGetValue(eventName, out eventObject))
            {
                //Add more listening action to the existing event
                eventObject += listeningAction;

                //Update the Dictionary
                eventDictionary[eventName] = eventObject;
            }
            else
            {
                //Add event to the Dictionary with the given listening action
                eventObject += listeningAction;
                eventDictionary.Add(eventName, eventObject);
            }
        }

        /// <summary>
        /// Remove the given listener from the event
        /// </summary>
        /// <param name="eventName">The name of event</param>
        /// <param name="listeningAction">The listening action</param>
        public static void StopListening(string eventName, UnityAction<DataSet> listeningAction)
        {
            UnityAction<DataSet> eventObject;
            if (eventDictionary.TryGetValue(eventName, out eventObject))
            {
                //Remove event from the existing one
                eventObject -= listeningAction;

                //Update the Dictionary
                eventDictionary[eventName] = eventObject;
            }
        }

        /// <summary>
        /// If found in the eventDictionary the event with event name specified will be Invoked
        /// 
        /// </summary>
        /// <param name="eventName">Event name contained in the dictionary.</param>
        public static void TriggerEvent(string eventName, DataSet eventParameters = null)
        {
            UnityAction<DataSet> eventObject;
            if (eventDictionary.TryGetValue(eventName, out eventObject))
            {
                eventObject?.Invoke(eventParameters);
            }
        }
    }
}