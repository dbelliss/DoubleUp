using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerImage : MonoBehaviour {

    [SerializeField]
    float flickerMin = .1f;

    [SerializeField]
    float flickerMax = 2f;

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
            im.enabled = !im.enabled;
            yield return new WaitForSeconds(Random.Range(flickerMin, flickerMax));
        }
    }

}
