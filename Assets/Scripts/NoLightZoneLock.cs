using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoLightZoneLock : MonoBehaviour
{
    [SerializeField] private GameObject _canvasMessage = null;

    //[SerializeField] private Flashlight _flashLight;
    [SerializeField] private GameObject _leftHand;

    [SerializeField] private Transform _player;

    [SerializeField] private GameObject _finalEnemy;

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("Ahh... Mi cabeza... No puedo pasar por aquí.");

            Flashlight flashlight = _leftHand.GetComponentInChildren<Flashlight>(); 

            if (flashlight != null)
            {
                Debug.Log("1");
                if (flashlight.flashLightPicked)
                {
                    Debug.Log("2");
                    _canvasMessage.SetActive(false);
                    DestroyBarrier();
                }

            }
            else
            {
                _canvasMessage.SetActive(true);
            }
        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            _canvasMessage.SetActive(false);
        }
    }
    #endregion
    #region Trigger

    private void Update()
    {
        if(Vector3.Distance(_player.position, transform.position) < 2)
        {
            NewFlashLight flashlight = _leftHand.GetComponentInChildren<NewFlashLight>();

            if (flashlight != null)
            {
                Debug.Log("1");
                if (flashlight.flashLightPicked)
                {
                    Debug.Log("2");
                    _canvasMessage.SetActive(false);
                    DestroyBarrier();
                }

            }
            else
            {
                _canvasMessage.SetActive(true);
            }
        }
        else
        {
            _canvasMessage.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log("Ahh... Mi cabeza... No puedo pasar por aquí.");

            Flashlight flashlight = _leftHand.GetComponentInChildren<Flashlight>();

            if (flashlight != null)
            {
                Debug.Log("1");
                if (flashlight.flashLightPicked)
                {
                    Debug.Log("2");
                    _canvasMessage.SetActive(false);
                    DestroyBarrier();
                }

            }
            else
            {
                _canvasMessage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _canvasMessage.SetActive(false);
        }
    }
    #endregion
    public void DestroyBarrier() //LLAMAR A ESTO CUANDO YA TENGA LA LINTERNA Y PUEDA PASAR
    {
        if(!_finalEnemy.activeSelf)_finalEnemy.SetActive(true);
        _canvasMessage.SetActive(false);
        Destroy(gameObject);
    }
}
