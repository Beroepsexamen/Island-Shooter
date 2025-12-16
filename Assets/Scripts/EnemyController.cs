using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    private int xVelHash;
    private int yVelHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        xVelHash = Animator.StringToHash("XVelocity");
        yVelHash = Animator.StringToHash("YVelocity");
    }

    // Update is called once per frame
    private void Update()
    {
        // Get the distance from agent to target
        float distance = Vector3.Distance(target.position, transform.position);

        // Set destination if within look radius
        if (distance <= lookRadius) agent.SetDestination(target.position);

        // Makes the enemy crouch when close to the player
        if (distance <= agent.stoppingDistance)
        {
            animator.SetBool("IsCrouched", true);
            FaceTarget();
        }
        else
        {
            animator.SetBool("IsCrouched", false);
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

    // Draw the look radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
