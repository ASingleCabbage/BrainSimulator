using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using System;

public class CameraEffectsController : MonoBehaviour {
    public PostProcessKeyValPair[] ppProfiles;
    private Dictionary<string, PostProcessProfile> profiles;    //Workaround as Unity inspector doesn't like dictionaries
    private Dictionary<string, PostProcessVolume> activeVolumes;

    private List<EffectDurationTracker> timedEffects;

    void Start () {
        profiles = new Dictionary<string, PostProcessProfile>();
        foreach (PostProcessKeyValPair pair in ppProfiles) {
            try {
                profiles.Add(pair.key, pair.profile);
            } catch (ArgumentException) {
                Debug.Log("Error loading in post processing profiles, duplicate keys detected");
            }
        }
        activeVolumes = new Dictionary<string, PostProcessVolume>();
        timedEffects = new List<EffectDurationTracker>();
	}

    public void SpawnPostProcessVolume(string key) {
        if (profiles.ContainsKey(key)) {
            if (activeVolumes.ContainsKey(key)) {
                Debug.Log("profile " + key + " is already applied");
                return;
            }
            activeVolumes.Add(key, PostProcessManager.instance.QuickVolume(gameObject.layer, 0.0f, profiles[key].settings.ToArray()));
        } else {
            Debug.Log(key + " is not a valid key in profiles");
        }
    }

    public void SpawnPostProcessVolume(string key, float duration) {
        Debug.Log("Added post process volume " + key + " that goes away in " + duration);
        timedEffects.Add(new EffectDurationTracker(key, duration));
        SpawnPostProcessVolume(key);
    }

    public void DestroyPostProcessVolume(string key) {
        if (activeVolumes.ContainsKey(key)) {
            RuntimeUtilities.DestroyVolume(activeVolumes[key], false);
            activeVolumes.Remove(key);
        } else {
            Debug.Log(key + " is not a valid key in activeVolumes");
        }
    }

	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < timedEffects.Count; i++) {
            if (timedEffects[i].EffectDurationMet()) {
                DestroyPostProcessVolume(timedEffects[i].GetEffectKey());
                timedEffects.RemoveAt(i);
                i--;
            }
        }
	}

    [System.Serializable]
    public class PostProcessKeyValPair {
        public PostProcessProfile profile = null;
        public string key = "";
    }

    class EffectDurationTracker {
        private string key;
        private float targetDuration;
        private float activeDuration;

        public EffectDurationTracker(string key, float targetDuration) {
            this.key = key;
            this.targetDuration = targetDuration;
            activeDuration = 0.0f;
        }

        public bool EffectDurationMet() {
            activeDuration += Time.deltaTime;
            if (targetDuration <= activeDuration) {
                return true;
            }
            return false;
        }

        public string GetEffectKey() {
            return key;
        }

    }
}
