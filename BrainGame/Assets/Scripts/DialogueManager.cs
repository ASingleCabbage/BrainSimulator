using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//I wanted some sort of dialogue tree, but I guess a dialogue list works too
//whipped up in half an hour and probably terribly designed
public class DialogueManager : MonoBehaviour {
    //delegate definitions
    public delegate bool Condition();
    public delegate void UpdateFunction();

    public GameObject dialoguePanel;

    private List<DialogueNode> dialogueList;
    private int dialogueIndex = 0;

    private DialoguePanelController panelController;

    private bool loadedNewList = false;

    void Start() {
        dialogueList = new List<DialogueNode>();
        dialoguePanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { NextDialogue(); });
        panelController = dialoguePanel.GetComponent<DialoguePanelController>();
    }

    private void Update() {
        if (dialogueIndex >= dialogueList.Count) {
            return;
        }

        if (dialogueList[dialogueIndex].updateFunction != null) {
            dialogueList[dialogueIndex].updateFunction();
        }
    }

    public void Activate() {
        enabled = true;         //do I really need this?
        dialoguePanel.SetActive(true);
    }

    public void Deactivate() {
        Debug.Log("Dialogue deactivated");
        enabled = false;
        dialoguePanel.SetActive(false);
    }

    public void OverrideButtonText(string bText) {
        panelController.LoadButtontText(bText);
    }

    //this thing is too messy but forgive me for it is 4am
    public void NextDialogue() {
        if (loadedNewList) {
            loadedNewList = false;
        }
        if (dialogueIndex >= dialogueList.Count) {
            return;
        }

        if (dialogueList[dialogueIndex].condition != null) {
            if (!dialogueList[dialogueIndex].condition()) {
                Debug.Log("Condition reports false");
                return;
            }
        }

        if (loadedNewList) {
            loadedNewList = false;
        } else {
            dialogueIndex++;
        }
        //disables self if no more dialogue to run
        if (dialogueIndex >= dialogueList.Count) {
            Deactivate();
        } else {
            loadDialogueNode(dialogueList[dialogueIndex]);
        }
    }
    
    public void LoadDialogueList(List<DialogueNode> diaList) {
        dialogueList = diaList;
        if (dialogueList.Count == 0) {
            Debug.LogWarning("WARN: dialogue list is empty");
        } else {
            loadDialogueNode(dialogueList[0]);
            dialogueIndex = 0;
            loadedNewList = true;
        }
    }

    public void AppendDialogieList(List<DialogueNode> diaList) {
        //not sure if this is how you do list concat in c#
        dialogueList.AddRange(diaList);
    }

    private void loadDialogueNode(DialogueNode node) {
        panelController.LoadTitle(node.title);
        panelController.LoadContent(node.content);
        panelController.LoadButtontText(node.buttonText);
    }

    //TODO: add function pointer in here
    public class DialogueNode{
        //okay this is my first time doing this in c# this is amazing
        public string title;
        public string content;
        public string buttonText;
        public Condition condition;     //function runs after button press; returns true to continue
        public UpdateFunction updateFunction;

        public DialogueNode(string title, string content, string bText) {
            setupValues(title, content, bText);
        }

        public DialogueNode(string title, string content, string bText, Condition condition) {
            setupValues(title, content, bText);
            this.condition = condition;
        }

        public DialogueNode(string title, string content, string bText, UpdateFunction upFunc) {
            setupValues(title, content, bText);
            updateFunction = upFunc;
        }

        public DialogueNode(string title, string content, string bText, UpdateFunction upFunc, Condition condition) {
            setupValues(title, content, bText);
            this.condition = condition;
            updateFunction = upFunc;
        }

        private void setupValues(string title, string content, string bText) {
            this.title = title;
            this.content = content;
            buttonText = bText;
            condition = null;
            updateFunction = null;
        }
    }
}
