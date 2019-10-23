using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendingColors : MonoBehaviour
{
    public Material palette;
    public Material blending;
    public Material result;

    void Start()
    {
        StartCoroutine(Blending());
    }

    IEnumerator Blending()
    {
        this.GetComponent<Renderer>().material = palette;
        yield return new WaitForSeconds(10);
        this.GetComponent<Renderer>().material = blending;
        yield return new WaitForSeconds(2);
        this.GetComponent<Renderer>().material = result;
        yield return new WaitForSeconds(2);
        this.GetComponent<Renderer>().material = palette;
        yield return new WaitForSeconds(2);
    }
}
