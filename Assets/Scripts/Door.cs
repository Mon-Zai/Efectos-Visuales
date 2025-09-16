using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorsManagement _doorsManager;
    [SerializeField] private Transform _player;

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7 && _doorsManager.keys.ContainsKey(Key.FlashlightDoor))   //LAYER PLAYER
        {
            gameObject.SetActive(false);
        }
    }*/

    private void Update()
    {
        if(Vector3.Distance(transform.position, _player.position)< 2f)
        {
            if(_doorsManager.keys.ContainsKey(Key.FlashlightDoor)) 
                gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _doorsManager.keys.ContainsKey(Key.FlashlightDoor))   //LAYER PLAYER
        {
            gameObject.SetActive(false);
        }
    }
}
