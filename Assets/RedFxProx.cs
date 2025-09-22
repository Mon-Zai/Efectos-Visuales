using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFxProx : MonoBehaviour
{
   
    public Transform player;        // El player
    public Transform target;        // El objeto que activa el efecto
    public Renderer quadRenderer;   // Renderer del quad
    public float maxDistance = 5f;  // Distancia máxima (a partir de aquí es invisible)
    public float maxAlpha = 1f;     // Alpha máximo cuando está cerca

    private Material quadMat;

    void Start()
    {
        quadMat = quadRenderer.material;

        // Arranca transparente
        Color c = quadMat.color;
        c.a = 0f;
        quadMat.color = c;
    }

    void Update()
    {
        // Distancia entre el player y el objeto target
        float dist = Vector3.Distance(player.position, target.position);

        // Normalizamos (cuando dist >= maxDistance ? 0, cuando dist = 0 ? 1)
        float t = Mathf.InverseLerp(maxDistance, 0f, dist);

        // Calcular alpha (0 lejos, maxAlpha cerca)
        float newAlpha = Mathf.Lerp(0f, maxAlpha, t);

        // Aplicar alpha al quad
        Color c = quadMat.color;
        c.a = newAlpha;
        quadMat.color = c;
    }
}



