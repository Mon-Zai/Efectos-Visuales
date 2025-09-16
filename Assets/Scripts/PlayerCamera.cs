using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private float _xOffset = 0f, _yOffset = 0f, _zOffset = 0f;
    [SerializeField] private float _yMouseSensitivity;
    [SerializeField] private float vMaxLimit = 90, vMinLimit = -90;
    private float verRotation;

    [SerializeField] private float _xMouseSensitivity;

    [Header("Recoil Values")] 

    public float recoilStrenght = 2f;

    private float _mouseY, _mouseX;
    private Vector3 _position;

    private void LateUpdate()
    {
        CameraRotation();

        _position = new Vector3
                (_player.position.x - _xOffset, _player.position.y - _yOffset, _player.position.z - _zOffset);

        transform.position = _position;

        transform.rotation = Quaternion.Euler(verRotation, _player.eulerAngles.y, 0f);

    }

    private void CameraRotation()
    {
        _mouseY = Input.GetAxis("Mouse Y") * _yMouseSensitivity + recoilStrenght;
        verRotation -= _mouseY;
        verRotation = Mathf.Clamp(verRotation, vMinLimit, vMaxLimit);

        transform.localEulerAngles = new Vector3
        (verRotation, transform.localEulerAngles.y, 0f);

        //=====

        if (Input.GetAxisRaw("Mouse X") != 0)
        {
            _mouseX = Input.GetAxisRaw("Mouse X") * _xMouseSensitivity;
            _player.Rotate(Vector3.up * _mouseX);
        }
    }
}
