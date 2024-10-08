using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class ShowPathLine : MonoBehaviour
{
    public Transform target;
    public float offsetHeight = 0.5f;
    public float updateSpeed = 0.25f;

    private LineRenderer pathRenderer;
    private NavMeshTriangulation navMeshTriangulation;
    private Coroutine currentDraw;

    private void Awake()
    {
        navMeshTriangulation = NavMesh.CalculateTriangulation();
    }

    private void OnEnable()
    {
        if (currentDraw != null)
        {
            StopCoroutine(currentDraw);
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
