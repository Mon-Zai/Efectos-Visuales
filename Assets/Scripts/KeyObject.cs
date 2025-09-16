using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [SerializeField] private DoorsManagement _doorsManager;
    [SerializeField] private Key _key;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.CompareTag("Player"))  //LAYER PLAYER
        {
            _doorsManager.AddKey(_key);

            Destroy(gameObject, 0.25f);
        }
        
    }
}
