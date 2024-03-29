﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float viewPortFactor;
    [SerializeField] private float followDuration;
    [SerializeField] private float maximumFollowSpeed;
    [SerializeField] private Transform playerTransform;

    Vector2 viewPortSize;
    Camera mainCamera;

    Vector3 targetPosition;
    Vector3 currentVelocity;

    Vector2 distance;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        targetPosition = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        viewPortSize = (mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - mainCamera.ScreenToWorldPoint(Vector2.zero)) * viewPortFactor;

        distance = playerTransform.position - transform.position;
        if (Mathf.Abs(distance.x) > viewPortSize.x / 2)
        {
            targetPosition.x = playerTransform.position.x - (viewPortSize.x / 2 * Mathf.Sign(distance.x));
        }

        if (Mathf.Abs(distance.y) > viewPortSize.y / 2)
        {
            targetPosition.y = playerTransform.position.y - (viewPortSize.y / 2 * Mathf.Sign(distance.y));
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition - new Vector3(0, 0, 10), ref currentVelocity, followDuration, maximumFollowSpeed);
    }

    private void OnDrawGizmos()
    {
        Color c = Color.red;
        c.a = 0.3f;
        Gizmos.color = c;

        Gizmos.DrawCube(transform.position, viewPortSize);
    }
}
