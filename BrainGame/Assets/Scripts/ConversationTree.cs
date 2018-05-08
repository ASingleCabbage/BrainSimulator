using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTree : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public class ConversationStep {
        public string message;
        public Func<Boolean> waitCondition;

        public ConversationStep() {
            message = "";
            waitCondition = defaultCondition;
        }

        public ConversationStep(string s) {
            message = s;
            waitCondition = defaultCondition;
        }

        public ConversationStep(string s, Func<bool> condition) {
            message = s;
            waitCondition = condition;
        }

        Boolean defaultCondition() {
            return true;
        }
    }
}
