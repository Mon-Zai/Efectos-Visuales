using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb = null;
    
    [Header("Move Properties")]
    [SerializeField] private float _walkSpeed = 6f;

    [HideInInspector] public float horizontal = 0, vertical = 0;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 movement = ((transform.forward * vertical) + (transform.right * horizontal)).normalized;

        _rb.velocity = new Vector3
            (movement.x * _walkSpeed, _rb.velocity.y, movement.z * _walkSpeed);

    }

}
