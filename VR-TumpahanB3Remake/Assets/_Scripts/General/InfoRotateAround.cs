using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoRotateAround : MonoBehaviour
{
    public bool inversRotation = false;
    public Transform targetObject;
    public float rotationThreshold = 30f;
    public float rotationSpeed = 5f;

    void Update()
    {
        Vector3 directionToPlayer = targetObject.position - transform.position;
        directionToPlayer.y = 0;

        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle > rotationThreshold)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inversRotation ? -directionToPlayer : directionToPlayer);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
