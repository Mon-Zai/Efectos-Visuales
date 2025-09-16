using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TubeWeapon : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject attachTo;


    [SerializeField] private float damage;

    public void Interact() //Should create a world model to avoid possible errors and mantain single responsability
    {
        transform.SetParent(attachTo.transform);
        transform.position = attachTo.transform.position;
        transform.rotation = attachTo.transform.rotation;
    }
}
