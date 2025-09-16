using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTubeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject attachTo;


    [SerializeField] private float damage;

    [SerializeField] private Transform _player;

    private bool _picked = false;

    [SerializeField] private Transform _axeModel;

    public void Interact() //Should create a world model to avoid possible errors and mantain single responsability
    {
        transform.SetParent(attachTo.transform);
        transform.position = attachTo.transform.position;
        transform.rotation = attachTo.transform.rotation;

        _axeModel.localPosition = new Vector3(0.04f, 0.48f, 0.05f);
        _axeModel.localEulerAngles = new Vector3(-101.294f, 134.7f, -267.85f);

        _picked = true;
    }

    private void Update()
    {
        if(Vector3.Distance(_player.position, transform.position) < 1.5f && !_picked)
        {
            Interact();
            _picked = true;
        }

    }
}
