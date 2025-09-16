using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void Update()
    {
        if(Vector3.Distance(transform.position, _player.position) < 4.5)
        {
            GameManager.Instance.SetCheckpoint();
            Destroy(gameObject, 0.15f);
        }
    }
}
