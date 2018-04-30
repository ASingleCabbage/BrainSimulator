using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A barebones tutorial system
 */
public class TutorialScript : MonoBehaviour {
    public GameObject tutorialDialogue; //tutorial dialogue should have a text and button element as children

    private List<TutorialStep> tutorialSteps;
    private int stepIndex = 0;

    // Use this for initialization
    void Start() {
        tutorialDialogue.GetComponentInChildren<Button>().onClick.AddListener(dialogueDisplayNext);
        setupTutorial();
    }

    // Displays the next message if current message condition is met
    void dialogueDisplayNext() {
        if (tutorialSteps[stepIndex].waitCondition()) {
            stepIndex++;
            if (stepIndex >= tutorialSteps.Count) {
                endTutorial();
            } else {
                tutorialDialogue.GetComponentInChildren<Text>().text = tutorialSteps[stepIndex].message;
            }
        }
    }

    void endTutorial() {
        tutorialDialogue.SetActive(false);
    }

    void setupTutorial() {
        GameObject.Find("DormRoom").transform.Find("RightMapTrigger").gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        GameObject.Find("DormRoom").transform.Find("LeftMapTrigger").gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        GameObject.Find("DormRoom").transform.Find("DeskInteractible").gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        Time.timeScale = 0; //pausing game time initially to prevent loosing health
        tutorialSteps = new List<TutorialStep> {
            new TutorialStep("Welcome to the final day of the semester."),
            new TutorialStep("Quick! There are no workers in your brain right now.", delegate(){
                Time.timeScale = 1; //restores game time scale after player clicks next on this prompt
                return true;
            }),
            new TutorialStep("Press B and H to manually control your breathing and heart rate. If you don't do it frequently enough, you'll lose health and die."),
            new TutorialStep("Try allocating 2 workers to the brain stem to let it automate vital functions", delegate(){
                if(GameObject.Find("BrainStem").GetComponent<WorkerContainer>().GetWorkerCount() == 2){
                    return true;
                } else {
                    return false;
                }
            }),
            new TutorialStep("You can't see well because you dont have enough workers in your occipital lobe, try assigning 2 workers there too.", delegate(){
                if(GameObject.Find("OccipitalLobe").GetComponent<WorkerContainer>().GetWorkerCount() == 2){
                    return true;
                } else {
                    return false;
                }
            }),
            new TutorialStep("Try moving around...Feels sluggish? Assign 2 workers to your motor cortex to move faster.", delegate(){
                if(GameObject.Find("MotorCortex").GetComponent<WorkerContainer>().GetWorkerCount() == 2){
                    return true;
                }else{
                    return false;
                }
            }),
            new TutorialStep("Notice that you only have 8 workers to spare. Make sure you put them to good use. Each region would require 2 to reach normal function."),
            new TutorialStep("Each worker uses up some brain stamina every second. The more workers you have active, the faster you'll loose stamina. Like your health, if you run out of stamina, you'll collapse and end the game early."),
            new TutorialStep("You can use items in your inventory to replenish stamina or health. Use the coffee in your inventory to top off your stamina before you start the day.", delegate(){
                if (GameObject.Find("BrainStaminaBar").GetComponent<Slider>().value >= 90.0f){
                    GameObject.Find("DormRoom").transform.Find("RightMapTrigger").gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    GameObject.Find("DormRoom").transform.Find("LeftMapTrigger").gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    GameObject.Find("DormRoom").transform.Find("DeskInteractible").gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    return true;
                }else{
                    return false;
                }
            })
        };

        tutorialDialogue.GetComponentInChildren<Text>().text = tutorialSteps[0].message;
    }

    /*
     * Tutorial step object
     * Has OOP gone too far? You decide.
     */
    private class TutorialStep{
        public string message;
        public Func<Boolean> waitCondition;

        public TutorialStep() {
            message = "";
            waitCondition = defaultCondition;
        }

        public TutorialStep(string s) {
            message = s;
            waitCondition = defaultCondition;
        }

        public TutorialStep(string s, Func<bool> condition) {
            message = s;
            waitCondition = condition;
        }

        Boolean defaultCondition() {
            return true;
        }
    }
}
