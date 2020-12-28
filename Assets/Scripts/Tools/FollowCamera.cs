using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed = 2;

    private void Start()
    {
        target = GameManager.PlayerController.Target.transform;
    }

    void LateUpdate()
    {
        var cameraPosition = transform.position;
        var position = target.position;
        var targetPosition = position + target.TransformDirection(offset);

        transform.position = Vector3.Slerp(cameraPosition, targetPosition, moveSpeed * Time.fixedDeltaTime);
        
    }
}
