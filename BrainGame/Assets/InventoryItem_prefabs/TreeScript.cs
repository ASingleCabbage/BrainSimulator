using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeScript : MonoBehaviour {
    public float effectDuration;
    public string effectKey;

	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate {
            Debug.Log("Activated Tree Powers");
            GameObject.Find("Main Camera").GetComponent<CameraEffectsController>().SpawnPostProcessVolume(effectKey, 10.0f);
        });
	}
}
