using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public enum MoveDirection
    {
        Horizontal,
        Vertical
    }

    public MoveDirection direction = MoveDirection.Horizontal; // Direction of movement
    public float distance = 5f; // Distance to move from the original position
    public float speed = 2f; // Speed of the platform

    private Vector3 _startPosition;
    private bool _movingForward = true;

    void Start()
    {
        // Save the original position
        _startPosition = transform.position;
    }

    void Update()
    {
        // Determine the axis of movement
        float move = speed * Time.deltaTime * (_movingForward ? 1 : -1);
        Vector3 offset = direction == MoveDirection.Horizontal ? new Vector3(move, 0, 0) : new Vector3(0, move, 0);

        // Move the platform
        transform.position += offset;

        // Check if platform has reached the limit and reverse direction
        float traveledDistance = direction == MoveDirection.Horizontal
            ? transform.position.x - _startPosition.x
            : transform.position.y - _startPosition.y;

        if (Mathf.Abs(traveledDistance) >= distance)
        {
            _movingForward = !_movingForward;
            // Clamp position to ensure it doesn't overshoot
            if (direction == MoveDirection.Horizontal)
                transform.position = new Vector3(_startPosition.x + Mathf.Sign(traveledDistance) * distance, _startPosition.y, _startPosition.z);
            else
                transform.position = new Vector3(_startPosition.x, _startPosition.y + Mathf.Sign(traveledDistance) * distance, _startPosition.z);
        }
    }
}