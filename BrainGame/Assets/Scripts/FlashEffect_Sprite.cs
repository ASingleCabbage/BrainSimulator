using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect_Sprite : MonoBehaviour {
    public Color targetColor;
    public float flashInterval = 1.0f;
    public bool activateOnStart = false;

    private Color initialColor;
    private bool isActive;
    private bool isTargetColor;
    private float currTime;
    private SpriteRenderer gameObjectSpriteRenderer;

    void Start() {
        gameObjectSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        initialColor = gameObjectSpriteRenderer.color;
        if (activateOnStart) {
            isActive = true;
        } else {
            isActive = false;
        }
        isTargetColor = false;
        currTime = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if (isActive) {
            if (currTime > flashInterval) {
                currTime = 0.0f;
                if (isTargetColor) {
                    isTargetColor = false;
                    gameObjectSpriteRenderer.color = initialColor;
                } else {
                    isTargetColor = true;
                    gameObjectSpriteRenderer.color = targetColor;
                }
            }
            currTime += Time.deltaTime;
        }
    }

    public void Activate() {
        isActive = true;
    }

    public void Deactivate() {
        isActive = false;
        gameObjectSpriteRenderer.color = initialColor;
    }
}
