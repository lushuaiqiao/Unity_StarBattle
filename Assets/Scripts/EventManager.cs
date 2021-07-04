using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager: MonoBehaviour
{
    public static EventManager me;
    public Dictionary<string, Func<object[], object>> eventList;

    void Awake()
    {
        me = this;
        eventList = new Dictionary<string, Func<object[], object>>();
    }

    public void AddEventListener(string eventName, Func<object[], object> eventMethod)
    {
        if (eventList.ContainsKey(eventName))
        {
            eventList[eventName] += eventMethod;
        }
        else
        {
            eventList.Add(eventName, eventMethod);
        }
    }
    public void RemoveEventListener(string eventName, Func<object[], object> eventMethod)
    {
        if (eventList.ContainsKey(eventName))
        {
            if (eventList[eventName]!=null)
            {
                eventList[eventName] -= eventMethod;
            }
            else
            {
                Debug.Log("no events be matched 0");
            }
            
        }
        else
        {
            Debug.Log("no events be matched 1");
        }
        if (eventList[eventName]==null)
        {
            eventList.Remove(eventName);
        }

    }

    public object TriggerEvent(string eventname, object[] parameter)
    {
        if (eventList.ContainsKey(eventname))
        {
            return eventList[eventname](parameter);

        }
        else
        {
            Debug.Log("There is no event named " + eventname);
            return null;
        }

    }

}

