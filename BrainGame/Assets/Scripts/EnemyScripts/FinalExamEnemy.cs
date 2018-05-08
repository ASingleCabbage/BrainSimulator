using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalExamEnemy : MonoBehaviour {
    //movement parameters
    public float rotateSpeed = 5.0f;
    public float rotateRad = 0.1f;

    public KeyCode attackKey = KeyCode.Space;

    //HP for the enemy I guess
    public int maxAttacks = 2;
    private int currentAttacks = 0;
    public UnityEvent deathEvents;

    //enemy attack parameters
    public float attackFrequency = 2.0f;
    public GameObject projectileObject;
    private float currentAttackCounter;

    public float immuneDuration = 1.0f;
    private float immuneCounter = 0.0f;
    private bool isImmune = false;

    private Vector2 rotateCenter;
    private float rotateAngle = 0.0f;

    private bool isDead = false;
    private float deathEffectDuration = 2.5f;
    private float deathEffectCounter = 0.0f;

    private DialogueManager dialogueManager;

    private List<InsightSystem.InsightObject> insightList;
    private List<DialogueManager.DialogueNode> dialogueList;
    private bool dialogueCompleted = false;

    private FlashEffect_Sprite flashEffect;
    private GameObject playerObject;
    private GameController controller;

    void Start() {
        rotateCenter = transform.position;
        
        dialogueManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueManager>();

        insightList = new List<InsightSystem.InsightObject> {
            new InsightSystem.InsightObject(InsightSystem.BrainRegion.TemporalLobe, 1),
            new InsightSystem.InsightObject(InsightSystem.BrainRegion.FrontalLobe, 4),
            new InsightSystem.InsightObject(InsightSystem.BrainRegion.OccipitalLobe, 1)
        };

        GameObject.FindGameObjectWithTag("GameController").GetComponent<InsightSystem>().loadInsightList(insightList);

        SetupDialogues();
        dialogueManager.LoadDialogueList(dialogueList);

        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        flashEffect = gameObject.GetComponent<FlashEffect_Sprite>();
    }

    private void Update() {

        if (dialogueCompleted) {
            currentAttackCounter += Time.deltaTime;
            Debug.Log("spawn counter " + currentAttackCounter);
            if (currentAttackCounter > attackFrequency) {
                currentAttackCounter = 0.0f;
                GameObject newProj = Instantiate(projectileObject);
                newProj.transform.localPosition = gameObject.transform.localPosition;
                newProj.transform.SetParent(gameObject.transform.parent.transform, false);
                newProj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15, 0), ForceMode2D.Impulse);
                //newProj.transform.parent = gameObject.transform.parent.transform;
                
                //might want to set speed or sth here
            }
        }

        if (isImmune && !isDead) {
            if (immuneCounter > immuneDuration) {
                isImmune = false;
                immuneCounter = 0.0f;
                flashEffect.Deactivate();
            } else {
                immuneCounter += Time.deltaTime;
            }
        }
        if (isDead) {
            deathEffectCounter += Time.deltaTime;
            if (deathEffectCounter > deathEffectDuration) {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
                deathEvents.Invoke();
            } else {
                //rotateRad *= (deathEffectDuration - deathEffectCounter) / deathEffectDuration;
                rotateAngle += (rotateSpeed * deathEffectCounter * deathEffectCounter) * Time.deltaTime;
                Vector2 offset = new Vector2(Mathf.Sin(rotateAngle) * rotateRad, Mathf.Cos(rotateAngle) * rotateRad);
                transform.position = rotateCenter + offset;
            }
        } else {
            rotateAngle += rotateSpeed * Time.deltaTime;
            Vector2 offset = new Vector2(Mathf.Sin(rotateAngle) * rotateRad, Mathf.Cos(rotateAngle) * rotateRad);
            transform.position = rotateCenter + offset;

        }
    }

    private void SetupDialogues() {
        dialogueList = new List<DialogueManager.DialogueNode> {
            new DialogueManager.DialogueNode("", "The final exam stares at you menacingly. You've made peace with yourself and turn to face it.", "Let's do this"),
            new DialogueManager.DialogueNode("Tutorial", "To fight the final, attack it by pressing the Space Bar. You must have enough workers in the neccessary regions for your attack to be effective. Remember to dodge incoming attacks too.", "I got this!", delegate{
                OnEventComplete();
                return true;
            })
        };
    }

    public void OnEventComplete() {
        GameObject.Find("GameController").GetComponent<InsightSystem>().Activate();
        //gameObject.transform.parent.Find("LeftMapTrigger").GetComponent<BoxCollider2D>().isTrigger = true;
        //gameObject.transform.parent.Find("RightMapTrigger").GetComponent<BoxCollider2D>().isTrigger = true;
        playerObject.GetComponent<PlayerController>().Knockback(new Vector2(-20, 5));
        dialogueCompleted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Player") {
            if (!dialogueCompleted) {
                TriggerDialogue();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.transform.tag == "Player" && dialogueCompleted) {
            if (Input.GetKeyDown(attackKey) && !isImmune) {
                if (CheckAttackValidity()) {
                    TriggerDamageEffect();
                } 
            }
        }
    }

    private void TriggerDamageEffect() {
        currentAttacks++;
        if (currentAttacks >= maxAttacks) {
            TriggerDeathEffect();
        } else {
            isImmune = true;
            flashEffect.Activate();

            playerObject.GetComponent<PlayerController>().Knockback(new Vector2(-12, 5));
        }
    }

    public void TriggerDeathEffect() {
        playerObject.GetComponent<PlayerController>().isImmune = true;
        isDead = true;
        flashEffect.flashInterval = 0.001f;
        flashEffect.Activate();
    }

    //frick function overheads let the compiler worry about this
    //also hardcoded values because yolo
    private bool CheckAttackValidity() {
        int tempWorker = GameObject.Find("TemporalLobe").GetComponent<WorkerContainer>().GetWorkerCount();
        int occiWorker = GameObject.Find("OccipitalLobe").GetComponent<WorkerContainer>().GetWorkerCount();
        int fronWorker = GameObject.Find("FrontalLobe").GetComponent<WorkerContainer>().GetWorkerCount();

        if (tempWorker <= 1 && occiWorker <= 1 && fronWorker <= 4) {
            return true;
        } else {
            return false;
        }

    }

    public void TriggerDialogue() {
        GameObject.Find("PlayerInfoPanel").GetComponent<InfoPanel>().overrideCancel();
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        dialogueManager.Activate();
    }

    private void OnEnable() {
        if (dialogueCompleted) {
            return;
        }
        GameObject.Find("PlayerInfoPanel").GetComponent<InfoPanel>().displayTextOverride("Oh no that's the final.", 10.0f);
        gameObject.transform.parent.Find("LeftMapTrigger").GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.transform.parent.Find("LeftMapTrigger").GetComponent<MapTriggerLeft>().enabled = false;
        //gameObject.transform.parent.Find("RightMapTrigger").GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
