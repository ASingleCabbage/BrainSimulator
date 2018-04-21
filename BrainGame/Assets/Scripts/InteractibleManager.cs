using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractibleManager : MonoBehaviour {
    public UnityEvent enterEvents;
    public UnityEvent exitEvents;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("triggered collider");
        enterEvents.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("exited collider");
        exitEvents.Invoke();
    }
}
