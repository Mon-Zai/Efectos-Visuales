using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour //TAMBIEN LA USO PARA PUERTAS QUE NO SON DOBLES
{
    [SerializeField] private Transform _doorLeft;
    [SerializeField] private Transform _doorRight; //PUERTA PRINCIPAL QUE USO PARA CUANDO NO SON DOBLES
    [SerializeField] private bool _isOpen = false;
    [SerializeField] private Transform _player;

    [SerializeField] private bool _isDouble = false;

    private Vector3 _leftDoorOgRot, _rightDoorOgRot;

    private Vector3 _velocity = new Vector3(0f,90f,0f);

    private bool _canOpen = true;

    private void Start()
    {
        if(_doorLeft != null)_leftDoorOgRot = _doorLeft.localEulerAngles;
        _rightDoorOgRot = _doorRight.localEulerAngles;  
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, _player.position) < 2)
        {
            if (_canOpen) StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        _canOpen = false;

        Vector3 doorRightOpen = new Vector3(0f, -90f, 0f);
        Vector3 doorLeftOpen = new Vector3(0f, 90f, 0f);

        float cd = 0f;
        float maxCD = 1f;
        while(cd < maxCD)
        {
            cd += Time.deltaTime;
            if(_isDouble)
            {
                _doorRight.localEulerAngles = Vector3.Lerp(_rightDoorOgRot, doorRightOpen, cd);
                if (_doorLeft != null) _doorLeft.localEulerAngles = Vector3.Lerp(_leftDoorOgRot, doorLeftOpen, cd);
            }
            else
            {
                _doorRight.localEulerAngles = Vector3.Lerp(_rightDoorOgRot, doorRightOpen, cd);
            }

            enabled = false;
            yield return null;
        }

        //_canOpen = true;
    }
}
