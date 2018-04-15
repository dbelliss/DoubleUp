using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour {

    public void SetPlayer1Character(int cType) {
        GameManager.instance.SetPlayer1Character (cType);
    }

    public void SetPlayer2Character(int cType) {
        GameManager.instance.SetPlayer2Character (cType);
    }
}
