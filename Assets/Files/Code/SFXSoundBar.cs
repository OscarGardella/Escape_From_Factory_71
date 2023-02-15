using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SFXSoundBar : MonoBehaviour
{
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.Drag;
        entry.eventID = EventTriggerType.PointerClick;
        //entry.callback.AddListener(data => { OnDrag((PointerEventData)data); });
        entry.callback.AddListener(data => { OnPointerClick((PointerEventData)data); });
        trigger.triggers.Add(entry);

    }

    public void OnDrag(PointerEventData data)
    {
        //Debug.Log("Mouse is over GameObject");
        AudioManager.Instance.PlaySFX("Player Shooting");
    }

    public void OnPointerClick(PointerEventData data)
    {
        AudioManager.Instance.PlaySFX("Player Shooting");
    }
}
