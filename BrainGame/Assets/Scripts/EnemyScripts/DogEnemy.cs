using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEnemy : MonoBehaviour {
    private DialogueManager dialogueManager;

    private List<InsightSystem.InsightObject> insightList;
    private List<DialogueManager.DialogueNode> dialogueList;
    private bool eventCompleted = false;

    void Start() {
        dialogueManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueManager>();

        insightList = new List<InsightSystem.InsightObject> {
            new InsightSystem.InsightObject(InsightSystem.BrainRegion.MotorCortex, 4),
        };

        GameObject.FindGameObjectWithTag("GameController").GetComponent<InsightSystem>().loadInsightList(insightList);

        SetupDialogues();
        dialogueManager.LoadDialogueList(dialogueList);
    }

    private void SetupDialogues() {
        List<DialogueManager.DialogueNode> failDialogues = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "You tried to reach for the head, but your outstretched arm looked like food to the dog, and it bit your hands.", "ouch", delegate {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ReduceHealth(20);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Knockback(new Vector2(-10, 10));
                OnEventComplete();
                return true;
            })
        };

        List<DialogueManager.DialogueNode> passDialogues = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "You said hi and played with the dog, but you stepped on its tail as you stood up. He looked hurt, and you feel extremely guilty. Slight reduce in stamina but minor increase in health.", "oops", delegate {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ReduceStamina(20);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddHealth(10);

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Knockback(new Vector2(-10, 10));
                OnEventComplete();
                return true;
            })
        };

        List<DialogueManager.DialogueNode> successDialogues = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "You rubbed the good boy's belly, and felt overwhelming joy. Large amounts of stamina and some health restored", "Awww yisss", delegate {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddStamina(40);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddHealth(20);
                OnEventComplete();
                return true;
            })
        };

        dialogueList = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "The dog is quite large. If you don't have enough worker's in the motor cortex, the dog might not like it. It certainly bites", "Who's a good boye", delegate {
                 int motorWorkerCount = GameObject.Find("MotorCortex").GetComponent<WorkerContainer>().GetWorkerCount();
               switch(motorWorkerCount){
                    case 0:
                        dialogueManager.OverrideButtonText("Workers in the motor cortex allows for finer muscle controls");
                        break;
                    case 1:
                        dialogueManager.OverrideButtonText("I'll hurt the good boy if I'm this rough...");
                        break;
                    case 2:
                        dialogueManager.OverrideButtonText("I should be careful...and have more workers in motor");
                        break;
                    case 3:
                        dialogueManager.OverrideButtonText("My thighs are numb. What if I step on his tail?");
                        break;
                    default:
                        dialogueManager.OverrideButtonText("I think I'll do okay. It's just a dog right?");
                        break;
                }
            }, delegate {
                int motorWorkerCount = GameObject.Find("MotorCortex").GetComponent<WorkerContainer>().GetWorkerCount();
                Debug.Log("Worker count " + motorWorkerCount);
                switch(motorWorkerCount){
                    case 0:
                    case 1:
                    case 2:
                        dialogueManager.LoadDialogueList(failDialogues);
                        break;
                    case 3:
                        dialogueManager.LoadDialogueList(passDialogues);
                        break;
                    default:
                        dialogueManager.LoadDialogueList(successDialogues);
                        break;
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
        GameObject.Find("PlayerInfoPanel").GetComponent<InfoPanel>().displayTextOverride("I should go pet that good boy.", 10.0f);
        GameObject.Find("GameController").GetComponent<InsightSystem>().Activate();
        gameObject.transform.parent.Find("LeftMapTrigger").GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.transform.parent.Find("RightMapTrigger").GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
