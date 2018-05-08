using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFuckupper : MonoBehaviour {
    public audioSetting[] settings;

    private WorkerContainer temporalContainer;
    private AudioDistortionFilter distortFilter;
    private AudioEchoFilter echoFilter;

	void Start () {
        temporalContainer = GameObject.Find("TemporalLobe").GetComponent<WorkerContainer>();
        distortFilter = gameObject.GetComponent<AudioDistortionFilter>();
        echoFilter = gameObject.GetComponent<AudioEchoFilter>();
    }

    private void FixedUpdate() {
        int workerCount = temporalContainer.GetWorkerCount();

        //if there is a matching index in settings array for workerCount
        if (workerCount >= 0 && workerCount < settings.Length) {
            LoadAudioSetting(settings[workerCount]);
        }
    }

    private void LoadAudioSetting(audioSetting setting) {
        distortFilter.distortionLevel = setting.distortion;
        echoFilter.delay = setting.echoDelay;
    }

    [System.Serializable]
    public struct audioSetting {
        [Range(0.0f, 1.0f)]
        public float distortion;
        public int echoDelay;
    }
}
