using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class NewFlashLight : MonoBehaviour
{
    [SerializeField] private GameObject attachTo;
    bool picked = false;
    public bool flashLightPicked { get; private set; }

    [SerializeField] private Light source;
    private bool isOn = false;

    [SerializeField] private Transform _player;

    [SerializeField] private GameObject _UIText;

    [SerializeField] private float _lightIntensity = 30f;

    public void Interact() 
    {
        transform.SetParent(attachTo.transform);
        transform.position = attachTo.transform.position;
        transform.rotation = attachTo.transform.rotation;
        picked = true;

        flashLightPicked = picked; 
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, _player.position) < 1.5f && !picked)
        {
            _UIText.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        else if (Vector3.Distance(transform.position, _player.position) >= 1.5f && !picked )
        {
            _UIText.SetActive(false);
        }
        else if(picked)
        {
            _UIText.SetActive(false);

            transform.position = attachTo.transform.position;
            transform.rotation = attachTo.transform.rotation;
        }

        if (picked && Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
            {
                source.range = 0f;
                isOn = false;
            }
            else
            {
                source.range = _lightIntensity;
                isOn = true;
            }

        }

    }
}
