using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoLightZoneLock : MonoBehaviour
{
    [SerializeField] private GameObject _canvasMessage = null;

    [SerializeField] private GameObject _leftHand;
    [SerializeField] private Transform _player;

    [SerializeField] private GameObject _finalEnemy;

    // -------------------------------
    // 🔥 CONTROL DEL SHADER FULLSCREEN
    // -------------------------------
    [Header("Fullscreen Shader")]
    public Material fullscreenMaterial;   // Material de tu RenderFeature
    public float activationDistance = 5f; // A qué distancia empieza el efecto

    private float shaderIntensity = 0f;
    // -------------------------------


    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Flashlight flashlight = _leftHand.GetComponentInChildren<Flashlight>();
            if (flashlight != null && flashlight.flashLightPicked)
            {
               // _canvasMessage.SetActive(false);
                DestroyBarrier();
            }
            else
            {
               // _canvasMessage.SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
           // _canvasMessage.SetActive(false);
        }
    }
    #endregion


    #region Trigger + Shader
    private void Update()
    {
        // -------------------------------
        // 🔥 CONTROL DE EFECTO POR DISTANCIA
        // -------------------------------
        float distance = Vector3.Distance(_player.position, transform.position);

        
        float targetIntensity = Mathf.Clamp01(1 - (distance / activationDistance));

      
        shaderIntensity = Mathf.Lerp(shaderIntensity, targetIntensity, Time.deltaTime * 5f);

       
        if (fullscreenMaterial != null)
            fullscreenMaterial.SetFloat("_fxIntensity", shaderIntensity);
        


       
        if (distance < 2)
        {
            NewFlashLight flashlight = _leftHand.GetComponentInChildren<NewFlashLight>();

            if (flashlight != null && flashlight.flashLightPicked)
            {
                //_canvasMessage.SetActive(false);
                DestroyBarrier();
            }
            else
            {
                //_canvasMessage.SetActive(true);
            }
        }
        else
        {
            //_canvasMessage.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Flashlight flashlight = _leftHand.GetComponentInChildren<Flashlight>();

            if (flashlight != null && flashlight.flashLightPicked)
            {
               // _canvasMessage.SetActive(false);
                DestroyBarrier();
            }
            else
            {
                //_canvasMessage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            //_canvasMessage.SetActive(false);
        }
    }
    #endregion


    public void DestroyBarrier()
    {
        if (!_finalEnemy.activeSelf)
            _finalEnemy.SetActive(true);

        _canvasMessage.SetActive(false);
        Destroy(gameObject);
    }
}
