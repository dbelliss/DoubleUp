using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour {

    public static FightManager instance;

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
    Image[] doubleUpImages;

    [SerializeField]
    Text doubleTimeText;

    [SerializeField]
    Text readyText;

    [SerializeField]
    Text winText;

    public bool isDoubleTime {
        get;
        private set;
    } // True if in double time

    [SerializeField]
    bool testWinRound1; // For testing, as if player 1 wins

    [SerializeField]
    bool testWinRound2; // For testing, as in player 2 wins

    [SerializeField]
    GameObject player1; 

    [SerializeField]
    GameObject player2;

    [SerializeField]
    private Vector3 player1Start;

    [SerializeField]
    private Vector3 player2Start;

    bool hasRoundEnded = false;

    [SerializeField]
    GameObject[] playerTypes;


	// Use this for initialization
	void Start () {
        if (instance != null) {
            Debug.LogError ("Error: Two FIghtManagers were created");
            return;
        }
            
        instance = this;
        CreatePlayers ();
        if (player1 == null || player2 == null) {
            Debug.LogError ("Error: Both players were not set");
            return;
        }

        foreach(Image im in doubleUpImages) {
            im.enabled = false;
            FlickerImage flicker = im.GetComponent<FlickerImage> ();
            if (flicker != null) {
                flicker.Disable ();
            }
        }

        SetNextDoubleTime ();
        isDoubleTime = false;
        BeginRound ();
	}


    private void CreatePlayers() {

        Quaternion player1Quat = Quaternion.identity;
        player1Quat.eulerAngles = new Vector3 (0, 135, 0);

        player1 = Instantiate (playerTypes [(int)GameManager.instance.player1CharacterType], player1Start, player1Quat);
        player1.GetComponent<CubeController> ().playerNum = 0;

        Quaternion player2Quat = Quaternion.identity;
        player2Quat.eulerAngles = new Vector3 (0, -135, 0);

        player2 = Instantiate (playerTypes [(int)GameManager.instance.player2CharacterType], player2Start, player2Quat);

        player2.GetComponent<CubeController> ().playerNum = 1;
    }

    public CubeController GetPlayer1() {
        return player1.GetComponent<CubeController> ();
    }

    public CubeController GetPlayer2() {
        return player2.GetComponent<CubeController> ();
    }


    void BeginRound() {
        
        readyText.gameObject.SetActive (false);
        StopCoroutine (FadeText ()); // Stop fading if still fading
        // Reset player positions
        ResetPlayers();
        // Show ready text
        hasRoundEnded = false;
        readyText.gameObject.SetActive (true);
        readyText.text = "Ready";
        StartCoroutine (RoundStart ());

    }

    void ResetPlayers() {
        // Move players back to original positions
        player1.transform.position = player1Start;
        player2.transform.position = player2Start;

        // Lock movement
        CubeController player1Controller = player1.GetComponent<CubeController>();
        CubeController player2Controller = player2.GetComponent<CubeController>();

        if (player1Controller == null || player2Controller == null) {
            Debug.LogError ("Error: Both players do not have a cube controller");
        }


        player1Controller.enabled = false;
        player2Controller.enabled = false;
    }
        

    // Wait a short time before allowing players to move
    IEnumerator RoundStart() {
        yield return new WaitForSeconds (2f);
        readyText.text = "Fight!";
        // Allow players to move
        // Lock movement
        // Lock movement
        CubeController player1Controller = player1.GetComponent<CubeController>();
        CubeController player2Controller = player2.GetComponent<CubeController>();

        if (player1Controller == null || player2Controller == null) {
            Debug.LogError ("Error: Both players do not have a cube controller");
        }




        player1Controller.enabled = true;
        player2Controller.enabled = true;
    
        StartCoroutine (FadeText()); // Begin to fade Fight text away
    }

    IEnumerator FadeText() {
        Color startColor = readyText.color;
        while (readyText.color.a >= 0) {
            readyText.color = new Color (startColor.r, startColor.g, startColor.b, readyText.color.a - .01f);
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
        if (hasRoundEnded) {
            doubleTimeText.text = "";
            return;
        }

        if (!isDoubleTime) {
            float timeLeft = nextDoubleTime - Time.time;

            doubleTimeText.text = "2X in: " + (int)timeLeft;
            if (Time.time >= nextDoubleTime) {
                // Begin double time
                foreach(Image im in doubleUpImages) {
                    im.enabled = true;
                    FlickerImage flicker = im.GetComponent<FlickerImage> ();
                    if (flicker != null) {
                        flicker.Enable ();
                    }
                }

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
                foreach(Image im in doubleUpImages) {
                    im.enabled = false;
                    FlickerImage flicker = im.GetComponent<FlickerImage> ();
                    if (flicker != null) {
                        flicker.Disable ();
                    }
                }

                Time.timeScale = 1;
                isDoubleTime = false;
                SetNextDoubleTime ();
                doubleTimeText.text =  ((int)nextDoubleTime).ToString();
            }
            else {
                // Still in double time
                doubleTimeText.text = ((int)timeLeft).ToString();
            }
        }

        if (testWinRound1) {
            testWinRound1 = false;
            WinRound (0);
        }
        else if (testWinRound2) {
            testWinRound2 = false;
            WinRound (1);
        }
	}

    public void WinRound(int winner) {
        readyText.gameObject.SetActive (true);
        hasRoundEnded = true;
        if (winner == 0) {
            // Player 1 won
            player1ScoreKeeper.WinRound();

        }
        else if (winner == 1) {
            // Player 2 wins
            player2ScoreKeeper.WinRound ();
        }
        else {
            Debug.LogError ("Player " + winner.ToString () + " cannot win the round");
            return;
        }
        readyText.text = "Player " + (winner+1).ToString() + " wins the round";

        isDoubleTime = false;

        Time.timeScale = .5f;
        StartCoroutine (FinishRound ());

    }

    // Go slow motion for a bit before restarting round
    IEnumerator FinishRound() {
        yield return new WaitForSeconds (2f);
        Time.timeScale = 1;
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
        readyText.gameObject.SetActive (false);
        winText.gameObject.SetActive (true);
        winText.text = "Player " + (playerNum+1).ToString() + " wins!";
        StartCoroutine (BackToCharacterSelect ());
    }

    IEnumerator BackToCharacterSelect() {
        yield return new WaitForSeconds (3f);
        GameManager.instance.ChangeToCharacterSelect ();
    }
        
}
