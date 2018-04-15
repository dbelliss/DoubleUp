using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectModel : MonoBehaviour {

    [SerializeField]
    int playerNum = 1; // Which player this model represents

    public void SetPlayerModel(int characterNum) {
        if (characterNum == 1) {
            // Display Main player
        }
        else if (characterNum == -1) {
            // Display Random symbol
        }
    }
}
