using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float LookRadius = 10f;
    
    Transform Target;
    NavMeshAgent Agent;
    Animator Animator;

    private int XVelHash;
    private int YVelHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Target = PlayerManager.instance.Player.transform;
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        XVelHash = Animator.StringToHash("XVelocity");
        YVelHash = Animator.StringToHash("YVelocity");
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(Target.position, transform.position);

        if (Distance <= LookRadius) Agent.SetDestination(Target.position);

        if (Distance <= Agent.stoppingDistance) FaceTarget();

        Vector3 FlatVel = new Vector3(Agent.velocity.x, 0, Agent.velocity.z);
        Vector3 LocalVel = transform.InverseTransformDirection(FlatVel);

        Animator.SetFloat(XVelHash, LocalVel.x);
        Animator.SetFloat(YVelHash, LocalVel.z);
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
