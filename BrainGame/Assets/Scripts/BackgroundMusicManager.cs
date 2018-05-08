using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour {
    public AudioEntry[] audioEntries;

    //crossfade parameters
    public float fadeTime = 2;

    public string initialMusicKey = "";

    private Dictionary<string, AudioClip> audioDict;
    private DoubleAudioSource doubleAudioSource;

    void Start () {
        audioDict = new Dictionary<string, AudioClip>();
        foreach (AudioEntry entry in audioEntries) {
            audioDict.Add(entry.key, entry.clip);
        }
        //I'm betting lots on this code to work
        doubleAudioSource = gameObject.GetComponent<DoubleAudioSource>();

        if (initialMusicKey != "") {
            PlayClip(initialMusicKey);
        }
	}

    public void PlayClip(string key) {
        if (audioDict.ContainsKey(key)) {
            doubleAudioSource.CrossFade(audioDict[key], 1, fadeTime);
        } else {
            Debug.LogWarning("Key " + key + " doesn't exist in audioEntries");
        }
    }

    [System.Serializable]
    public struct AudioEntry {
        public AudioClip clip;
        public string key;
    }
}
