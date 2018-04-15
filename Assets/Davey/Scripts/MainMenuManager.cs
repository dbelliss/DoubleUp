using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    public void ChangeToMainMenu() {
        GameManager.instance.ChangeToMainMenu ();
    }

    public void ChangeToCharacterSelect() {
        Debug.Log("Changing to character sekect");
        GameManager.instance.ChangeToCharacterSelect ();
    }

    public void ChangeToFight() {
        GameManager.instance.ChangeToFight ();
    }
}
