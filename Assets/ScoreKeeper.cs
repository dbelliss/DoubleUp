using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    public int numRoundsWon {
        get;
        private set;

    }

    void Start() {
        numRoundsWon = 0;
    }

    // Shows an additional icon
    public void WinRound() {
        numRoundsWon++;

        for (int i = 0; i < numRoundsWon; i++) {
            transform.GetChild (i).gameObject.SetActive (true);
        }
    }
}
