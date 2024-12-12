using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0, 4, -5);
    public float followSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the camera starts at the correct offset
        if (offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired position using the target's position and offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Keep the camera looking at the target
        transform.LookAt(target);

    }
}
