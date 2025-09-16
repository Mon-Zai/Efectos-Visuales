using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewCamera : MonoBehaviour
{
    [SerializeField] private GameObject attachTo;

    [Header("Sensibility")]
    public float sensX = 100f;
    public float sensY = 100f;

    [Header("Clamping")]
    [SerializeField] private float xRotMax = 90f;
    [SerializeField] private float xRotMin = 90f;

    [Header("Tilt (Z rotation)")]
    [SerializeField] private float zRotMax = 5f;
    [SerializeField] private float tiltSmoothTime = 0.1f;
    [SerializeField] private float tiltThreshold = 0.5f;
    [SerializeField] private float tiltDecay = 2f;

    private float xRotation;
    private float yRotation;
    private Transform cameraPos;
    private float currentZRot = 0f;
    private float zVelocity = 0f;

    private float accumulatedMouseX = 0f;

    private PlayerStateHandler playerStateHandler;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraPos = attachTo.transform.Find("Camera Position");

        playerStateHandler = attachTo.GetComponent<PlayerStateHandler>();
    }

    private void LateUpdate()
    {
        if (playerStateHandler.GetState() == PlayerStates.CROUCHING)
        {
            Vector3 crouchPos = cameraPos.position;
            crouchPos.y = attachTo.transform.position.y;
            transform.position = crouchPos;
        }
        else
        {
            transform.position = cameraPos.position;
        }
        MoveCamera();
    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xRotMax, xRotMin);

        accumulatedMouseX += mouseX;
        accumulatedMouseX = Mathf.Lerp(accumulatedMouseX, 0, Time.deltaTime * tiltDecay);

        float targetZ = 0f;

        if (Mathf.Abs(accumulatedMouseX) > tiltThreshold)
        {
            float tiltAmount = Mathf.Clamp(-accumulatedMouseX, -zRotMax, zRotMax);
            targetZ = tiltAmount;
        }

        currentZRot = Mathf.SmoothDamp(currentZRot, targetZ, ref zVelocity, tiltSmoothTime);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, currentZRot);
        attachTo.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}