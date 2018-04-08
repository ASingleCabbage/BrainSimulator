using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour {
    public GameObject infoText;
    public float displayTimeSeconds = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void displayText(string s) {
        GameObject textObject = Instantiate(infoText);
        textObject.GetComponent<UnityEngine.UI.Text>().text = s;
        textObject.transform.SetParent(gameObject.transform, false);
        Destroy(textObject, displayTimeSeconds);
    }
}
