using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class ShowPathLine : MonoBehaviour
{
    public Transform target;
    public float offsetHeight = 0.5f;
    public float updateTime = 0.25f;

    private LineRenderer pathRenderer;
    private float time;

    private void Awake()
    {
        pathRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (time < updateTime)
        {
            time += Time.deltaTime;
            return;
        }
        if (target != null)
        {
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
            {
                pathRenderer.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                {
                    pathRenderer.SetPosition(i, path.corners[i] + Vector3.up * offsetHeight);
                }
            }
            time = 0.0f;
        }
        else
        {
            pathRenderer.positionCount = 0;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetNoTarget()
    {
        target = null;
    }
}
