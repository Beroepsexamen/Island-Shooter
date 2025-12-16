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
        // Get the distance from agent to target
        float Distance = Vector3.Distance(Target.position, transform.position);

        // Set destination if within look radius
        if (Distance <= LookRadius) Agent.SetDestination(Target.position);

        // Makes the enemy crouch when close to the player
        if (Distance <= Agent.stoppingDistance)
        {
            Animator.SetBool("IsCrouched", true);
            FaceTarget();
        }
        else
        {
            Animator.SetBool("IsCrouched", false);
        }

        // Determine local velocity
        Vector3 FlatVel = new Vector3(Agent.velocity.x, 0, Agent.velocity.z);
        Vector3 LocalVel = transform.InverseTransformDirection(FlatVel);

        // Set the animation based on local velocity
        Animator.SetFloat(XVelHash, LocalVel.x);
        Animator.SetFloat(YVelHash, LocalVel.z);
    }

    private void FaceTarget() // Face target when in stopping distance
    {
        Vector3 Direction = (Target.position - transform.position).normalized;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(Direction.x, 0, Direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, Time.deltaTime * 5f);
    }

    // Draw the look radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
}
