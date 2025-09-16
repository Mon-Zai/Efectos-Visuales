using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStats : MonoBehaviour
{
    private float currentSpeed;

    [Header("Basic Movement")]
    public float normalSpeed;
    public float maxNormalSpeed;

    [Header("Sprint")]
    public float sprintSpeed;
    public float maxSprintSpeed;
    public float sprintMultiplier;

    [Header("Jump")]
    public float jumpForce;
    public float fallForce;

    [Header("Crouch")]
    public float crouchSpeed;
    public float maxCrouchSpeed;
    public float crouchDivide;

    void Awake()
    {
        UpdateSpeeds();
    }

    void Update()
    {
        if (currentSpeed != normalSpeed)
        {
            UpdateSpeeds();
        }
        currentSpeed = normalSpeed;
    }


    public void UpdateSpeeds()
    {
        currentSpeed = normalSpeed;
        crouchSpeed = normalSpeed / crouchDivide;
        sprintSpeed = normalSpeed * sprintMultiplier;

        maxSprintSpeed = maxNormalSpeed * sprintMultiplier;
        maxCrouchSpeed = maxNormalSpeed / (crouchDivide * crouchDivide);
    }
}