using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject _text1, _text2;
    [SerializeField] private float _timeBetweenText = 2f;

    private void Start()
    {
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        yield return new WaitForSeconds(_timeBetweenText);
        _text1.SetActive(false);
        _text2.SetActive(true);
        yield return new WaitForSeconds(_timeBetweenText);
        _text2.SetActive(false);
    }
}
