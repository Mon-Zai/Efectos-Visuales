using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeDamager : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private Transform _player;

    [SerializeField] private float _hitDistance;
    [SerializeField] private LayerMask _enemyMask;

    [SerializeField] private NewTubeWeapon _tube;

    public void Hit()
    {
        _tube = GetComponentInChildren<NewTubeWeapon>();

        if(_tube == null) return;

        Ray ray = new Ray(_player.position, _player.forward);

        Debug.DrawRay(ray.origin, ray.direction * _hitDistance, Color.blue);

        RaycastHit hit;

        Debug.Log("qq");

        if (Physics.Raycast(ray.origin, ray.direction, out hit, _hitDistance, _enemyMask))
        {
            Debug.Log("ee");
            hit.collider.TryGetComponent<EnemyZombie>(out EnemyZombie enemy);
            if(enemy != null)
            {
                enemy.TakeDamage(_damage);
                Debug.Log("rr");
            }
        }
    }
}
