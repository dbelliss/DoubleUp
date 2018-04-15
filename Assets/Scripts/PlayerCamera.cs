using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private GameObject _curPlayer;
    [SerializeField]
    private Vector3 baseOffset;

    [SerializeField]
    float rightCameraMax;

    [SerializeField]
    float leftCameraMax;

    public bool isFollowing = true; // True if camera should follow player

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_curPlayer == null)
        {
            _curPlayer = GameObject.Find("SeanLegacy");
        }
        if (_curPlayer && isFollowing)
        {
            this.gameObject.transform.position = _curPlayer.transform.position + baseOffset; // Move to a position tha maintains the same offset

            // Check if camera is within bounds
            if (transform.position.x > rightCameraMax) {
                // Set to right max position
                transform.position = new Vector3(rightCameraMax, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < leftCameraMax)
            {
                // Set to max left position
                transform.position = new Vector3(leftCameraMax, transform.position.y, transform.position.z);
            }
        }
        if (_curPlayer && this.GetComponent<AudioListener>().enabled == true)
        {
            // Move audio listener onto the player
            //            _curPlayer.AddComponent<AudioListener> ();
            //            this.GetComponent<AudioListener> ().enabled = false;
        }

    }

}
