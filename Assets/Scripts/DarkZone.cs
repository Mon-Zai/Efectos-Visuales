using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkZone : MonoBehaviour
{
    [SerializeField] private Material _darkZoneShader;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _distanceToApplyDarkness = 7;

    [SerializeField] private Vector3 _darknessRange;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _darknessRange);
    }

    private void Start()
    {
        _darkZoneShader.SetFloat("_ApplyEffect", 0);
    }

    private void Update()
    {
        Bounds bounds = new Bounds(transform.position, _darknessRange);

        if(bounds.Contains(_playerTransform.position))
        {
            _darkZoneShader.SetFloat("_ApplyEffect", 1);
        }
        else
        {
            _darkZoneShader.SetFloat("_ApplyEffect", 0);
        }
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            _darkZoneShader.SetFloat("_ApplyEffect", 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _darkZoneShader.SetFloat("_ApplyEffect", 0);
        }
    }*/
}
