using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExitToDynamic : MonoBehaviour
{
    public List<XRGrabInteractable> targetObjList = new List<XRGrabInteractable>();

    private void OnTriggerEnter(Collider other)
    {
        XRGrabInteractable obj = targetObjList.Find(x => x.gameObject == other.gameObject);
        if (obj)
        {
            obj.movementType = XRBaseInteractable.MovementType.Kinematic;

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            Debug.Log("Enter Spill Kit");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        XRGrabInteractable obj = targetObjList.Find(x => x.gameObject == other.gameObject);
        if (obj)
        {
            obj.movementType = XRBaseInteractable.MovementType.VelocityTracking;

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;

            Debug.Log("Exit Spill Kit");
        }
    }
}
