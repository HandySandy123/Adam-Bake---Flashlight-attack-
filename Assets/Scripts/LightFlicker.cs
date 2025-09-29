using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light myLight;

    private float flickerPeriod, pausePeriod, time = 0;
    private int flickerAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myLight = GetComponent<Light>();
        flickerPeriod = Random.Range(0.1f, 0.5f);
        pausePeriod = Random.Range(5f, 9f);
        flickerAmount = Random.Range(1, 8);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > pausePeriod)
        {
            StartCoroutine(nameof(flickerLights));
        }
    }

    IEnumerator flickerLights()
    {
        time = 0;
        while (time < flickerAmount)
        {
            time += flickerPeriod;
            myLight.enabled = !myLight.enabled;
            yield return new WaitForSeconds(flickerPeriod);
        }
        flickerPeriod = Random.Range(0.1f, 0.5f);
        time = 0;
        pausePeriod = Random.Range(5f, 9f);
    }
}
