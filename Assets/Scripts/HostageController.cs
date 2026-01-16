using UnityEngine;
using UnityEngine.AI;

public class HostageController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float followRadius = 15f;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    private int xVelHash;
    private int yVelHash;

    private bool followingPlayer = false;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        xVelHash = Animator.StringToHash("xVelocity");
        yVelHash = Animator.StringToHash("yVelocity");
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius) followingPlayer = true;

        if (followingPlayer)
        {
            if (distance <= followRadius) agent.SetDestination(target.position);
            if (distance > followRadius) agent.ResetPath();
            if (distance <= agent.stoppingDistance) FaceTarget();
        }

        // Determine local velocity
        Vector3 flatVel = new Vector3(agent.velocity.x, 0, agent.velocity.z);
        Vector3 localVel = transform.InverseTransformDirection(flatVel);

        // Set the animation based on local velocity
        animator.SetFloat(xVelHash, localVel.x);
        animator.SetFloat(yVelHash, localVel.z);
    }

    private void FaceTarget() // Face target when in stopping distance
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
