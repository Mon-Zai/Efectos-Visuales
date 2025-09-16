using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EKeyIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _EKeyUI;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("22");

        if (other.gameObject.layer == 7 || other.gameObject.CompareTag("Player"))
        {
            _EKeyUI.SetActive(true);

            Debug.Log("11");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("22");

        if (other.gameObject.layer == 7 || other.gameObject.CompareTag("Player"))
        {
            _EKeyUI.SetActive(false);

            Debug.Log("11");
        }
    }
}
