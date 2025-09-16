using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleDoor : MonoBehaviour
{
    public bool isLocked = true;

    [SerializeField] private GameObject _padlock = null;

    [SerializeField] private DoorOpening _doorOpeningScript = null;

    private void Update()
    {
        if(_padlock.activeSelf == false)
        {
            isLocked = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7) //CAMBIAR A LA LAYER QUE TENGA EL ARMA
        {
            _padlock.SetActive(false);

            if(_doorOpeningScript != null) _doorOpeningScript.canOpen = true;
        }
    }
}
