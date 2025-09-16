using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyZombie : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _player;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _damage = 1;

    [SerializeField] private float _distanceToFollowPlayer, _distanceToAttack;

    //[SerializeField] private Transform _hand;
    [SerializeField] private float _rayLenght = 1f;
    [SerializeField] private LayerMask _mask;

    [SerializeField] private Transform _transform;

    private bool _canCancellAttack = true;

    private Ray ray;
    private RaycastHit hit;

    [SerializeField] private int _life = 7;
    [SerializeField] private FinalDoor _finalDoor;

    [SerializeField] private ParticleSystem _bloodParticles;

    public int TakeDamage(int damage)
    {
        _life -= damage;
        ShowParticles();

        if(_life <= 0)
        {
            _finalDoor.Open();
            gameObject.SetActive(false);
        }
        return _life;
    }

    private void ShowParticles()
    {

        _bloodParticles.Play();
    }

    private void Awake()
    {
        if(_anim == null) _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, _player.position) < _distanceToFollowPlayer &&
            Vector3.Distance(transform.position, _player.position) >= _distanceToAttack)
        {
            Walk();
        }
        else if (Vector3.Distance(transform.position, _player.position) >=
            _distanceToFollowPlayer)
        {
            Idle();
        }
        if(Vector3.Distance(transform.position, _player.position) < _distanceToAttack)
        {          
            Vector3 dir = _player.position - transform.position;
            dir.y = 0f;
            transform.forward = dir.normalized;

            AttackAnimation();
        }
        else
        {
            if(_canCancellAttack)StartCoroutine(CancelAttackAnim());
        }

        
    }

    public void DamagePlayer()
    {
        //Ray ray = new Ray(_transform.position, _transform.forward * _rayLenght);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);

        Debug.Log("yu");

        if(Physics.Raycast(_transform.position, _transform.forward, out hit, _rayLenght, _mask))
        {
            Debug.Log("yy");
            if (hit.collider.gameObject.TryGetComponent<PlayerLife>(out PlayerLife player))
            {
                player.TakeDamage(_damage);
            }
        }
    }

    private void Idle()
    {
        _anim.SetBool("IsIdle", true);

        transform.position = transform.position;
    }
    private void Walk()
    {
        _anim.SetBool("IsIdle", false);

        Vector3 dir = _player.position - transform.position;
        dir.y = 0f;
        transform.forward = dir.normalized;

        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }

    private void AttackAnimation()
    {
        _anim.SetBool("CanAttack", true);
    }
    private IEnumerator CancelAttackAnim()
    {
        _canCancellAttack = false;
        yield return new WaitForSeconds(1f);
        _anim.SetBool("CanAttack", false);
        _canCancellAttack = true;
    }
    
}
