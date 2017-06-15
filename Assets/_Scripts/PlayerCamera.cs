using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    //Camera cam;
    Vector3 currentVelocity;
    public float speed = 10;
    // Use this for initialization
    void Start()
    {
        //cam = GetComponent<Camera>();

    }

    void FollowPlayer()
    {
        var playerPos = GameplayManager.Instance.playerVehicle.transform.position;
        var pos = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref currentVelocity, Time.deltaTime * speed);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }
}
