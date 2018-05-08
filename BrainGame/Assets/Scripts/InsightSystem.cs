using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsightSystem : MonoBehaviour {
    //experimenting with enums
    public enum BrainRegion { FrontalLobe = 0, OccipitalLobe = 1, MotorCortex = 2, TemporalLobe = 3, BrainStem = 4 };

    private WorkerContainer[] regionContainers;
    private FlashEffect_Sprite[] flashEffects;

    private List<InsightObject> insightList;
    private bool isActive = false;

    void Start() {
        insightList = new List<InsightObject>();

        int regionCount = Enum.GetNames(typeof(BrainRegion)).Length;
        regionContainers = new WorkerContainer[regionCount];
        flashEffects = new FlashEffect_Sprite[regionCount];

        for (int i = 0; i < regionCount; i++) {
            GameObject regionObject = GameObject.Find(Enum.GetNames(typeof(BrainRegion))[i]);
            regionContainers[i] = regionObject.GetComponent<WorkerContainer>();
            flashEffects[i] = regionObject.transform.GetComponentInChildren<FlashEffect_Sprite>();
        }
        
    }

    private void FixedUpdate() {
        if (isActive) {
            foreach (InsightObject insight in insightList) {
                //if region has the number or more workers allocated
                if (regionContainers[(int)insight.region].GetWorkerCount() >= insight.workerRequired) {
                    flashEffects[(int)insight.region].Deactivate();
                } else {
                    flashEffects[(int)insight.region].Activate();
                }
            }
        }
    }

    public void Activate() {
        Debug.Log("ACTIVATED INSIGHT");
        isActive = true;
    }

    public void Deactivate() {
        isActive = false;
        foreach (FlashEffect_Sprite flash in flashEffects) {
            flash.Deactivate();
        }
    }

    public void loadInsightList(List<InsightObject> insightList) {
        foreach (FlashEffect_Sprite flashEffect in flashEffects) {
            flashEffect.Deactivate();
        }
        this.insightList = insightList;
    }

    //yay more OOP
    public class InsightObject{
        public BrainRegion region;
        public int workerRequired;

        public InsightObject(BrainRegion region, int workerRequired) {
            this.region = region;
            this.workerRequired = workerRequired;
        }
    }
}
