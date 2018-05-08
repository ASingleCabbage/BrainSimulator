using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightSceneEnemy : MonoBehaviour {
    private DialogueManager dialogueManager;

    private List<InsightSystem.InsightObject> insightList;
    private List<DialogueManager.DialogueNode> dialogueList;
    private bool eventCompleted = false;

    void Start() {
        //dialogueController = dialoguePanel.GetComponent<DialoguePanelController>();

        dialogueManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueManager>();

        insightList = new List<InsightSystem.InsightObject> {
            new InsightSystem.InsightObject(InsightSystem.BrainRegion.TemporalLobe, 4),
            new InsightSystem.InsightObject(InsightSystem.BrainRegion.FrontalLobe, 1)
        };

        GameObject.FindGameObjectWithTag("GameController").GetComponent<InsightSystem>().loadInsightList(insightList);
        
        SetupDialogues();
        dialogueManager.LoadDialogueList(dialogueList);
    }

    private void SetupDialogues() {
        List<DialogueManager.DialogueNode> failNoFrontalDialogues = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "You can't think of anything to say. Your friend, thinking that you dont feel like talking, pretended not to see you.", "oops shoud've had workers in Frontal Lobe", delegate{
                OnEventComplete();
                return true;
            })
        };

        List<DialogueManager.DialogueNode> failDialogues = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "You tried to say hi, but you paniced and nothing came out of your mouth.", "oops"),
            new DialogueManager.DialogueNode("Friend", "Oh! I see how it is. I'm busy anyway and don't have time for you", "You feel hurt in the heart", delegate {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ReduceStamina(20);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Knockback(new Vector2(-10, 10));
                OnEventComplete();
                return true;
            })
        };

        List<DialogueManager.DialogueNode> passDialogues = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "You said hi and tried to recall a joke, but you gave out the punchline early.", "oops"),
            new DialogueManager.DialogueNode("Friend", "Hahaha what a story Mark. I loved that...like...a lot. I...uhh...have an appointment soon. See you later I guess.", "Okay"),
            new DialogueManager.DialogueNode("", "Not realizing that your friend is trying to be nice, you feel great and think you should become a comedian in the future. You feel somewhat more motivated.", "I should be a comedian", delegate {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddStamina(40);
                OnEventComplete();
                return true;
            })
        };

        List<DialogueManager.DialogueNode> successDialogues = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "You said hi and tried to recall a joke, and nailed it.", "Knock knock jokes never get old"),
            new DialogueManager.DialogueNode("Friend", "Hahaha what a story Mark. That was awesome!", "Okay"),
            new DialogueManager.DialogueNode("", "Despite being in finals week, your friend looks very cheerful. You feel strongly motivated. Somehow, your neural efficiency also increased. Totally unscientific, but it happened", "I should be a comedian", delegate {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddStamina(30);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddTotalWorker();
                OnEventComplete();
                return true;
            })
        };

        dialogueList = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "Your friend seems to be in a hurry but still wants to talk. Hopefully you have allocated your workers in advance", "I think I'm ready", delegate {
                int frontalWorkerCount = GameObject.Find("FrontalLobe").GetComponent<WorkerContainer>().GetWorkerCount();
                Debug.Log("frontal worker count " + frontalWorkerCount);
                if (GameObject.Find("FrontalLobe").GetComponent<WorkerContainer>().GetWorkerCount() < 1){
                    dialogueManager.LoadDialogueList(failNoFrontalDialogues);
                }else{ 
                    int temporalWorkerCount = GameObject.Find("TemporalLobe").GetComponent<WorkerContainer>().GetWorkerCount();
                    Debug.Log("Worker count " + temporalWorkerCount);
                    switch(temporalWorkerCount){
                        case 0:
                        case 1:
                            dialogueManager.LoadDialogueList(failDialogues);
                            break;
                        case 2:
                        case 3:
                            dialogueManager.LoadDialogueList(passDialogues);
                            break;
                        default:
                            dialogueManager.LoadDialogueList(successDialogues);
                            break;
                    }
                }
                return true;
            })
        };
    }

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
        //pause movement of friend here?
        GameObject.Find("PlayerInfoPanel").GetComponent<InfoPanel>().overrideCancel();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        dialogueManager.Activate();
    }

    private void OnEnable() {
        if (eventCompleted) {
            return;
        }
        GameObject.Find("PlayerInfoPanel").GetComponent<InfoPanel>().displayTextOverride("That's my friend. I should get ready to say hi.", 10.0f);
        GameObject.Find("GameController").GetComponent<InsightSystem>().Activate();
        gameObject.transform.parent.Find("LeftMapTrigger").GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.transform.parent.Find("RightMapTrigger").GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
