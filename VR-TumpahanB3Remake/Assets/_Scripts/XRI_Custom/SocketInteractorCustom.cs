using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SocketInteractorCustom : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<GameObject> allowedObjects = new List<GameObject>();
    [SerializeField] private GameObject attach;
    private bool followSocketPos;
    private Rigidbody objRb;
    private GameObject currentObj;
    private bool allowSocket;

    [Header("Events")]
    public UnityEvent onEnterSnap;
    public UnityEvent onExitSnap;

    private void Start()
    {
        followSocketPos = false;
        allowSocket = true;

        if (!attach)
        {
            attach = transform.gameObject;
        }
    }

    private void Update()
    {
        if (followSocketPos)
        {
            if (attach == transform.gameObject)
            {
                currentObj.transform.position = transform.position;
                currentObj.transform.rotation = transform.rotation;
            }
            else
            {
                currentObj.transform.position = transform.position + attach.transform.localPosition;
                currentObj.transform.rotation = transform.rotation * attach.transform.localRotation;
            }
        }
    }

    private void FixedUpdate()
    {
        if (followSocketPos)
        {
            objRb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (allowedObjects.Contains(other.gameObject) && allowSocket)
        {
            currentObj = other.gameObject;
            try
            {
                objRb = currentObj.GetComponent<Rigidbody>();
            }
            catch
            {
                Debug.Log("No Rigidbody was found here");
            }

            followSocketPos = true;
            allowSocket = false;
            onEnterSnap?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentObj == other.gameObject)
        {
            followSocketPos = false;
            allowSocket = true;

            currentObj = null;
            onExitSnap?.Invoke();
        }
    }
}