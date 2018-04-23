﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractPrompt : MonoBehaviour {
    public float maxScaleChange;
    public float scalingSpeedMultiplier = 1.0f;
    public KeyCode triggerKey;
    public UnityEvent interactEvents;

    private float timeSinceActive = 0.001f;
    private RectTransform rt;
    private Vector3 scaleChangeVector;

    private void Start() {
        rt = gameObject.GetComponent<RectTransform>();
        scaleChangeVector = new Vector3(maxScaleChange, maxScaleChange);
    }

    private void OnEnable() {
        timeSinceActive = 0.0f;    
    }

    private void Update() {
        if (Input.GetKeyDown(triggerKey)) {
            Debug.Log("INTERACTING NOW");
            interactEvents.Invoke();
        }

        timeSinceActive += Time.deltaTime;
        rt.localScale = rt.localScale +- scaleChangeVector * Mathf.Sin(timeSinceActive * scalingSpeedMultiplier) ;
    }

}