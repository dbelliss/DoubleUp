using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance; // Public singleton accessor

    AudioSource songSource; // Source of all songs

    [SerializeField]
    AudioClip[] MenuSongs; // Songs to play in main menu

    [SerializeField]
    AudioClip[] CharacterSelectSongs; // Songs to play in character select

    [SerializeField]
    AudioClip[] FightSongs; // Songs to play when fighting
	
    // Use this for initialization
	bool Initialize () {
        if (instance == null && instance != this) {
            instance = this; // Set singleton
            DontDestroyOnLoad (this.gameObject); // Keep this object between scenes
        }
        else {
            DestroyImmediate (this);
            return false;
        }
        songSource = GetComponent<AudioSource> ();
        return true;
	}

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

        
    // Plays song based on what level is loaded
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (instance == null) {
            if (!Initialize ()) {
                return;
            } // If songSource has not been set, set it
        }
        if (instance != this) {
            DestroyImmediate (this);
            return;
        }
        int sceneNum = scene.buildIndex;
        if (sceneNum == 0) {
            if (MenuSongs.Length == 0) {
                Debug.LogError ("Error: No menu songs");
                return;
            }
            if (songSource.clip == MenuSongs[0])
            {
                // Already playing menu song
                return;
            }
            int songNum = Random.Range (0, MenuSongs.Length);
            songSource.clip = MenuSongs [songNum];
        }
        else if (sceneNum == 1) {
            if (songSource.clip.name == "Smoke and Gas") {
                // Came from main menu, do not change song
                return;
            }
            if (CharacterSelectSongs.Length == 0) {
                Debug.LogError ("Error: No character select songs");
                return;
            }
            int songNum = Random.Range (0, CharacterSelectSongs.Length);
            songSource.clip = CharacterSelectSongs [songNum];
        }
        else if (sceneNum == 2) {
            if (FightSongs.Length == 0) {
                Debug.LogError ("Error: No fight songs");
                return;
            }
            int songNum = Random.Range (0, FightSongs.Length);
            songSource.clip = FightSongs [songNum];
        }
        songSource.Play (); // Play the song
    }

    // Stops all songs being played
    public void StopAllSongs() {
        songSource.Stop ();
    }
}
