using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PositionStart : MonoBehaviour
{
    public XROrigin player; 
    public Transform target;
    private void Start()
    {
        player.transform.position = target.position;
        player.MoveCameraToWorldLocation(target.position);
    }
}
