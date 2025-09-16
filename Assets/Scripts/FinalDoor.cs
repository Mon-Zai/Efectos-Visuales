using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoor : MonoBehaviour
{
    [SerializeField] private Transform _doorLeft;
    [SerializeField] private Transform _doorRight; //PUERTA PRINCIPAL QUE USO PARA CUANDO NO SON DOBLES
    [SerializeField] private bool _isOpen = false;

    private Vector3 _leftDoorOgRot, _rightDoorOgRot;

    private bool _canOpen = true;

    [SerializeField] private Transform _player;

    [SerializeField] private bool _canWin = false;

    private void Start()
    {
        if (_doorLeft != null) _leftDoorOgRot = _doorLeft.localEulerAngles;
        _rightDoorOgRot = _doorRight.localEulerAngles;
    }

    public void Open()
    {
        if(_canOpen) StartCoroutine(OpenDoor());
        _canWin = true;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position,_player.position)< 3f && _canWin)
        {
            SceneManager.LoadScene("WinScene");   
        }
    }

    private IEnumerator OpenDoor()
    {
        _canOpen = false;

        Vector3 doorRightOpen = new Vector3(0f, -90f, 0f);
        Vector3 doorLeftOpen = new Vector3(0f, 90f, 0f);

        float cd = 0f;
        float maxCD = 1f;
        while (cd < maxCD)
        {
            cd += Time.deltaTime;

            _doorRight.localEulerAngles = Vector3.Lerp(_rightDoorOgRot, doorRightOpen, cd);
            _doorLeft.localEulerAngles = Vector3.Lerp(_leftDoorOgRot, doorLeftOpen, cd);

            //enabled = false;
            yield return null;
        }

        //_canOpen = true;
    }
}
