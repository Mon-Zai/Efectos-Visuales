using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject attachTo;
    bool picked = false;
    public bool flashLightPicked { get; private set; }

    public Material material;
    private float shaderIntensity = 0f;

    [SerializeField] private Light source;
    private bool isOn = false;
    public void Interact() //Should create a world model to avoid possible errors and mantain single responsability
    {
        transform.SetParent(attachTo.transform);
        transform.position = attachTo.transform.position;
        transform.rotation = attachTo.transform.rotation;
        picked = true;

        transform.GetChild(0).gameObject.layer =
        LayerMask.NameToLayer("Default");



        flashLightPicked = picked; //IGAL AGREGÓ ESTO
    }
    void Update()
    {
        if (picked && Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
            {
                source.range = 0f;
                isOn = false;
            }
            else
            {
                source.range = 10f;
                isOn = true;
            }

        }

    }
}
