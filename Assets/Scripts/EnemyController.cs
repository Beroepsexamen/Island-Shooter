using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float LookRadius = 10f;
    
    Transform Target;
    NavMeshAgent Agent;
    Animator Animator;

    private int YVelHash;

    private bool IsWalking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Target = PlayerManager.instance.Player.transform;
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        YVelHash = Animator.StringToHash("YVelocity");
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(Target.position, transform.position);

        if (Distance <= LookRadius) 
        {
            Agent.SetDestination(Target.position);
            IsWalking = true;
        }

        if (Distance <= Agent.stoppingDistance)
        {
            FaceTarget();
            IsWalking = false;
        }

        if (IsWalking) Animator.SetFloat(YVelHash, 1f);
        else Animator.SetFloat(YVelHash, 0f);
    }

    private void FaceTarget()
    {
        Vector3 Direction = (Target.position - transform.position).normalized;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(Direction.x, 0, Direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
}
