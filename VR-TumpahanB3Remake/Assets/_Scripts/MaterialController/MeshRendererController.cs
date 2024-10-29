using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererController : MonoBehaviour
{
    public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    public void SetColors(Color color)
    {
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.color = color;
        }
    }
}
