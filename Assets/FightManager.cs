using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour {


    [SerializeField]
    ScoreKeeper player1ScoreKeeper;

    [SerializeField]
    ScoreKeeper player2ScoreKeeper;

    [SerializeField]
    int numRoundsToWin = 2;

    float minRangeToDoubleTime = 5;
    float maxRangeToDoubleTime = 10;

    float nextDoubleTime; // Time of the next double time
    float endOfDoubleTime; // End of current double time

    [SerializeField]
    Text doubleTimeText;

    [SerializeField]
    Text readyText;

    public bool isDoubleTime {
        get;
        private set;
    } // True if in double time


	// Use this for initialization
	void Start () {
        
        SetNextDoubleTime ();
        isDoubleTime = false;
        BeginRound ();
	}

    void BeginRound() {
        // Reset player positions

        // Show ready text
        readyText.gameObject.SetActive (true);
        readyText.text = "Ready";
        StartCoroutine (RoundStart ());
    }

    void ResetPlayers() {
        // Move players back to original positions
        //TODO
        // Lock movement
    }
        

    // Wait a short time before allowing players to move
    IEnumerator RoundStart() {
        yield return new WaitForSeconds (2f);
        readyText.text = "Fight!";
        // Allow players to move
        //TODO
        StartCoroutine (FadeText()); // Begin to fade Fight text away
    }

    IEnumerator FadeText() {
        Color startColor = readyText.color;
        while (readyText.color.a >= 0) {
            readyText.color = new Color (startColor.r, startColor.g, startColor.b, readyText.color.a - .005f);
            yield return new WaitForEndOfFrame ();
        }
        readyText.gameObject.SetActive (false);
        readyText.color = startColor;
    }
	

    void SetNextDoubleTime() {
        nextDoubleTime = Time.time + Random.Range (minRangeToDoubleTime, maxRangeToDoubleTime); // Generate next double time
    }


    void SetEndDoubleTime() {
        endOfDoubleTime = Time.time + Random.Range (minRangeToDoubleTime, maxRangeToDoubleTime); // Generate next double time
    }


	// Update is called once per frame
	void FixedUpdate () {
        if (!isDoubleTime) {
            float timeLeft = nextDoubleTime - Time.time;

            doubleTimeText.text = "Next double time: " + (int)timeLeft;
            if (Time.time >= nextDoubleTime) {
                Time.timeScale = 2;
                isDoubleTime = true;
                SetEndDoubleTime ();
            }
        }
        else {
            // In double time
            float timeLeft = endOfDoubleTime - Time.time;
            if (timeLeft <= 0) {
                // Double time has ended, generate next double time
                Time.timeScale = 1;
                isDoubleTime = false;
                SetNextDoubleTime ();
                doubleTimeText.text = "Next double time: " + (int)nextDoubleTime;
            }
            else {
                // Still in double time
                doubleTimeText.text = "Double timer: " + (int)timeLeft;
            }
        }

	}

    public void WinRound(int winner) {
        if (winner == 0) {
            // Player 1 won
            player1ScoreKeeper.WinRound();
        }
        else if (winner == 1) {
            // Player 2 wins
            player2ScoreKeeper.WinRound ();
        }

        if (!CheckForVictory ()) {
            BeginRound (); // Being next round if no one has won
        }
    }

    // Checks if either player has won
    // returns true if a player has won
    private bool CheckForVictory() {
        if (player1ScoreKeeper.numRoundsWon >= numRoundsToWin) {
            Win (0);
            return true;
        }
        else if (player2ScoreKeeper.numRoundsWon >= numRoundsToWin) {
            Win (1);
            return true;
        }
        else {
            return false;
        }
    }

    private void Win(int playerNum) {
        
    }
}
