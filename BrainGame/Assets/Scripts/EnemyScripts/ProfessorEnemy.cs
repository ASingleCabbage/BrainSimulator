using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorEnemy : MonoBehaviour {
    //this might be abstractified by the dialogue manager
    //public GameObject dialoguePanel;
    //private DialoguePanelController dialogueController;

    private DialogueManager dialogueManager;

    private List<InsightSystem.InsightObject> insightList;
    private List<DialogueManager.DialogueNode> dialogueList;
    private bool eventCompleted = false;

	void Start () {
        //dialogueController = dialoguePanel.GetComponent<DialoguePanelController>();

        dialogueManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueManager>();

        insightList = new List<InsightSystem.InsightObject> {
            new InsightSystem.InsightObject(InsightSystem.BrainRegion.TemporalLobe, 2),
            new InsightSystem.InsightObject(InsightSystem.BrainRegion.FrontalLobe, 3)
        };

        InsightSystem insightSystem = GameObject.Find("GameController").GetComponent<InsightSystem>();
        insightSystem.loadInsightList(insightList);
        

        SetupDialogues();
        dialogueManager.LoadDialogueList(dialogueList);
    }

    private void SetupDialogues() {
        List<DialogueManager.DialogueNode> failDialogues = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("Professor", "Oh don't give me that nonesense. Out of my way!", "Ouch", delegate {
                //deal damage to player here
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ReduceHealth(20);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Knockback(new Vector2(-10, 10));
                OnEventComplete();
                return true;
            })
        };

        dialogueList = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("Professor", "Hi how are you. I hope you have completed all the assignments due today", "...", delegate {
                int temporalWorkerCount = GameObject.Find("TemporalLobe").GetComponent<WorkerContainer>().GetWorkerCount();
                switch(temporalWorkerCount){
                    case 0:
                        dialogueManager.OverrideButtonText("Uhhh...oh right use my Temporal Lobe");
                        break;
                    case 1:
                        dialogueManager.OverrideButtonText("Use more Temporal Lobe...I still don't know what to say");
                        break;
                    case 2:
                        dialogueManager.OverrideButtonText("Ehhh...extension...please?");
                        break;
                    default:
                        dialogueManager.OverrideButtonText("I'd like to ask for an extension please");
                        break;
                }
            }, delegate{
                if(GameObject.Find("TemporalLobe").GetComponent<WorkerContainer>().GetWorkerCount() >= 2){
                    return true;
                }else{
                    return false;
                }
            }),
            new DialogueManager.DialogueNode("Professor", "Okay but why do you need the extension?", "...", delegate {
                int frontalWorkerCount = GameObject.Find("FrontalLobe").GetComponent<WorkerContainer>().GetWorkerCount();
                switch(frontalWorkerCount){
                    case 0:
                        dialogueManager.OverrideButtonText("Uhhh...I should use my Frontal Lobe now");
                        break;
                    case 1:
                        dialogueManager.OverrideButtonText("My...dog...no that wouldn't work. More Frontal Lobe");
                        break;
                    case 2:
                        dialogueManager.OverrideButtonText("I really need it!");
                        break;
                    default:
                        dialogueManager.OverrideButtonText("I'm stressed out with all my other assignments due tomorrow");
                        break;
                }
            }, delegate{
                if(GameObject.Find("FrontalLobe").GetComponent<WorkerContainer>().GetWorkerCount() >= 2){
                    return true;
                }else{
                    //switch to fail condition dialogues
                    dialogueManager.LoadDialogueList(failDialogues);
                    return true;
                }
            }),
            new DialogueManager.DialogueNode("Professor", "Alright then. I'll give you one more week to work on it your game", "Thanks! I appreciate it"),
            new DialogueManager.DialogueNode("", "You feel substantially less stressed and much more relaxed. Some health and stamina restored", "Noice!", delegate{
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddHealth(25);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddStamina(25);
                OnEventComplete();
                return true;
            })
        };
    }

    //maybe walk the professor out of the scene, then delete it here?
    public void OnEventComplete() {
        GameObject.Find("GameController").GetComponent<InsightSystem>().Deactivate();
        gameObject.transform.parent.Find("LeftMapTrigger").GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.transform.parent.Find("RightMapTrigger").GetComponent<BoxCollider2D>().isTrigger = true;
        eventCompleted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Player") {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue() {
        //disables collider of professor sprite
        GameObject.Find("PlayerInfoPanel").GetComponent<InfoPanel>().overrideCancel();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        GameObject.Find("GameController").GetComponent<InsightSystem>().Activate();
        dialogueManager.Activate();
    }

    private void OnEnable() {
        if (eventCompleted) {
            return;
        }

        GameObject.Find("PlayerInfoPanel").GetComponent<InfoPanel>().displayTextOverride("Thats my professor. I should ask for an extension.", 10.0f);
        //disables scene transition colliders
        gameObject.transform.parent.Find("LeftMapTrigger").GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.transform.parent.Find("RightMapTrigger").GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
