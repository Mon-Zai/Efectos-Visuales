using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{

    public float viewRadius = 10f;
    [Range(0, 360)]
    [SerializeField] private float viewAngle = 120f;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    private bool canSeePlayer;
    public bool CanSeePlayer
    {
        set { canSeePlayer = value; }
        get { return canSeePlayer; }
    }
    private bool playerIsInrange;
    public bool PlayerIsInrange
    {
        set { playerIsInrange = value; }
        get { return playerIsInrange; }
    }
    private GameObject player;
    public GameObject Player
    {
        get { return player; }
    }

    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfView();
        }
    }

    void FieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            playerIsInrange = true;
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            player = target.gameObject;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
        else
        {
            playerIsInrange = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 fovLine1 = DirectionFromAngle(transform.eulerAngles.y, -viewAngle / 2);
        Vector3 fovLine2 = DirectionFromAngle(transform.eulerAngles.y, viewAngle / 2);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1 * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2 * viewRadius);

        if (canSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

