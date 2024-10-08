using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class ShowPathLine : MonoBehaviour
{
    [OnValueChanged("RefreshDrawPath")] public Transform target;
    public float offsetHeight = 0.5f;
    public float updateSpeed = 0.25f;

    private LineRenderer pathRenderer;
    private Coroutine currentDraw;

    private void Awake()
    {
        currentDraw = StartCoroutine(DrawPath());
    }

    public void RefreshDrawPath()
    {
        if (currentDraw != null)
        {
            StopAllCoroutines();
        }
        currentDraw = StartCoroutine(DrawPath());
    }

    private IEnumerator DrawPath()
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        NavMeshPath path = new NavMeshPath();

        while (target != null)
        {
            if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
            {
                pathRenderer.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                {
                    pathRenderer.SetPosition(i, path.corners[i] + Vector3.up * offsetHeight);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate path on NavMesh between {transform.position} and {target.position}!");
            }
            yield return wait;
        }
    }
}
