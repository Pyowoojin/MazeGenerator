using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    eBlockDeleted,
    eScoreChanged,
    eBlockMovingFalse,
    eGamePaused,
    eBGMONOFF,
    eGameExit,
    eGameOver
}

public class GM_Script : MonoBehaviour
{
    private Dictionary<EventType, Action> eventDictionary;

    private static GM_Script eventManager;
    public static bool isPlaying = true;
    public static GM_Script instance
    {
        get
        {
            if (!eventManager)
            {
                Debug.Log("이벵메니터");
                eventManager = FindObjectOfType(typeof(GM_Script)) as GM_Script;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObeject in your scene.");
                }
                else
                {
                    Debug.Log("실행");
                    eventManager.Init();
                    DontDestroyOnLoad(eventManager);
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<EventType, Action>();
        }
    }

    public void StartInit()
	{
        Debug.Log("start");
	}

	public static void StartListening(EventType eventName, Action listener)
    {
        Action thisEvent;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(EventType eventName, Action listener)
    {
        if (eventManager == null) return;
        Action thisEvent;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(EventType eventName, Action listener)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent.Invoke();
            return;
        }
        else
        {
            Debug.LogError("해당 이벤트는 존재하지 않습니다." + eventName.ToString());
        }
    }
}