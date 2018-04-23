using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelController : MonoBehaviour {  // Use this for initialization
    public GameObject title;
    public GameObject content;
    public GameObject exitButton;

    public void LoadTitle(string title) {
        this.title.GetComponent<Text>().text = title;
    }

    public void LoadContent(string content) {
        this.content.GetComponent<Text>().text = content;
    }

    public void LoadButtontText(string buttonText) {
        exitButton.GetComponentInChildren<Text>().text = buttonText;
    }

    public void LoadNewDialogue(string title, string content, string exitButtonText) {
        this.title.GetComponent<Text>().text = title;
        this.content.GetComponent<Text>().text = content;
        this.exitButton.GetComponentInChildren<Text>().text = exitButtonText;
    }

    private void OnEnable() {
        Time.timeScale = 0.0f;
    }

    private void OnDisable() {
        Time.timeScale = 1.0f;
    }
}
