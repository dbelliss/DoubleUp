using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public static GameManager instance;


    public enum CharacterType {
        Sean,
        Mystery1,
        Mystery2,
        Mystery3,
        Random,
    }

    private CharacterType player1CharacterType;
    private CharacterType player2CharacterType;

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	// Use this for initialization
	void Start () {
        if (instance == null && instance != this) {
            instance = this;
            DontDestroyOnLoad (this.gameObject);
        }
        else {
            DestroyImmediate (this.gameObject);
            return;
        }
	}
        
    // called Call onSceneLoads 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }

    public int GetSceneNum() {
        return SceneManager.GetActiveScene ().buildIndex;
    }

    public void ChangeToMainMenu() {
        SceneManager.LoadScene (0);
    }

    public void ChangeToCharacterSelect() {
        SceneManager.LoadScene (1);
    }

    public void ChangeToFight() {
        SceneManager.LoadScene (2);
    }

    public void SetPlayer1Character(int cType) {
        player1CharacterType = (CharacterType)cType;
        Debug.Log ("Set player one to " + player1CharacterType.ToString ());
    }

    public void SetPlayer2Character(int cType) {
        player2CharacterType = (CharacterType)cType;
        Debug.Log ("Set player two to " + player2CharacterType.ToString ());
    }
}
