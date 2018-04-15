using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour {

    public void ChangeIcon(int characterNum) {
        for (int i = 0; i < transform.childCount; i++) {
            if (i == characterNum) {
                transform.GetChild (i).gameObject.SetActive (true);
            }
            else {
                transform.GetChild (i).gameObject.SetActive (false);
            }
        }
    }
}
