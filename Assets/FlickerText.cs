using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerText : MonoBehaviour {


    [SerializeField]
    float flickerMin = .1f;

    [SerializeField]
    float flickerMax = 2f;

    Text t;
    
    // Use this for initialization
    void Start()
    {
        t = GetComponent<Text>();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            t.enabled = !t.enabled;
            yield return new WaitForSeconds(Random.Range(flickerMin, flickerMax));
        }
    }
}
