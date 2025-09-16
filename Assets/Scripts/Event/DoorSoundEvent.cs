using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundEvent : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    bool active = true;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            audioSource.time = 24;
            audioSource.Play();
            active = false;
        }
    }



}
