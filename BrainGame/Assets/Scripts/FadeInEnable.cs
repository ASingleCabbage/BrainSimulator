using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInEnable : MonoBehaviour {
    public float fadeInTime;

    private CanvasGroup canvasGroup;
    private float timeSinceActive = 0.0f;
    private bool active = false;

    void Start() {
        gameObject.SetActive(false);
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    void Update() {
        if (active) {
            CalculateFadeIn();
        }
    }

    void CalculateFadeIn() {
        timeSinceActive += Time.deltaTime;
        if (timeSinceActive < fadeInTime) {
            canvasGroup.alpha = timeSinceActive / fadeInTime;
        } else {
            canvasGroup.alpha = 1;
        }
    }

    public void ActivateFadeIn() {
        gameObject.SetActive(true);
        active = true;
    }
}
