using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {
    public int maxStamina = 5;
    private int currentStamina;

    public string workerType;

	// Use this for initialization
	void Start () {
        currentStamina = maxStamina;
	}

    // Decreases stamina level by 1. Returns true if operation successful, else false
    public bool DecreaseStamina() {
        if (IsDead()) {
            return false;
        } else {
            currentStamina--;
            return true;
        }
    }

    public void IncreaseStamina(int amount) {
        Debug.Assert(amount >= 0);
        currentStamina += amount;
        if (currentStamina > maxStamina) {
            currentStamina = maxStamina;
        }
    }

    public bool IsDead() {
        if (currentStamina == 0) {
            return true;
        } else {
            return false;
        }
    }
}
