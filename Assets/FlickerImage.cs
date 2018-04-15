using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerImage : MonoBehaviour {

    [SerializeField]
    float flickerMin = .1f;

    [SerializeField]
    float flickerMax = 2f;

    bool shouldFlicker = true;

    Image im;
	// Use this for initialization
	void Start () {
        im = GetComponent<Image>();
        StartCoroutine(Flicker());
	}
	
    IEnumerator Flicker()
    {
        while (true)
        {
            if (shouldFlicker) {
                im.enabled = !im.enabled;
            }
            else {
                im.enabled = false;
            }
            yield return new WaitForSeconds(Random.Range(flickerMin, flickerMax));
        }
    }

    public void Enable() {
        shouldFlicker = true;
    }

    public void Disable() {
        shouldFlicker = false;
    }
}
