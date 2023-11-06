using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerToFollow = null;
    Vector3 playerOffset;

    private void Awake()
    {
        //creates the offset between the camera and the players position
        playerOffset = this.transform.position - playerToFollow.position;
    }
    //Happens after Update. Camera should always move last
    private void LateUpdate()
    {
        // Apply the offset every frame, to reposition this object
        this.transform.position = playerToFollow.position + playerOffset;
    }
}
