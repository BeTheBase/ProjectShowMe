using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flikker : MonoBehaviour
{
    [SerializeField] private List<Material> flikkerMaterials;
    [SerializeField] private float flikkerTweakTime = 0.5f;

    private MeshRenderer MeshRenderer { get; set; }

    private void Start()
    {
        if (!MeshRenderer)
            MeshRenderer = GetComponent<MeshRenderer>();

        StartCoroutine(Change());
    }
   

    private IEnumerator Change()
    {
        yield return new WaitForSeconds(flikkerTweakTime);
        MeshRenderer.material = flikkerMaterials[1];
        StartCoroutine(ChangeBack());
    }

    private IEnumerator ChangeBack()
    {
        yield return new WaitForSeconds(flikkerTweakTime);
        MeshRenderer.material = flikkerMaterials[0];
        StartCoroutine(Change());
    }
}
