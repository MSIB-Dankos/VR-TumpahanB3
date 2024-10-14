using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleport : MonoBehaviour
{
    public LayerMask triggerLayer;
    public Transform teleportPlace;
    public Transform objectTeleported;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerLayer == (triggerLayer | (1 << other.gameObject.layer)))
        {
            objectTeleported.position = teleportPlace.position;
        }
    }
}
