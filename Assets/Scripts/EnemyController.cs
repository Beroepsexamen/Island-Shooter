using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 15f;
    public int health = 100;
    public int dropRatePercent = 10;

    public ShooterData shooterData;
    public GameObject gunPickup;
    public Transform firePoint;
    public AudioSource gunAudio;
    public GameObject gunPickupUI;

    private float nextFireTime;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    private int xVelHash;
    private int yVelHash;

    private bool isDead = false;

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
        if (isDead) return;
        
        // Get the distance from agent to target
        float distance = Vector3.Distance(target.position, transform.position);

        // Set destination if within look radius
        if (distance <= lookRadius) agent.SetDestination(target.position);
        if (distance > lookRadius) agent.ResetPath();

        // Makes the enemy crouch when close to the player
        if (distance <= agent.stoppingDistance)
        {
            animator.SetBool("IsCrouched", true);
            FaceTarget();
            Shooting();
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

    private void Shooting()
    {
        if (Time.time < nextFireTime)
            return;
            
        Fire();
        nextFireTime = Time.time + Random.Range(0.5f, 1.5f);
    }

    private void Fire()
    {
        Debug.Log("Firing");

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, target.position - firePoint.position, out hit, shooterData.range))
        {
            gunAudio.PlayOneShot(shooterData.shootSound);

            GameObject fire = Instantiate(
                shooterData.fireEffect,
                firePoint.position,
                firePoint.rotation,
                firePoint
            );

            GameObject hitFX = Instantiate(
                shooterData.hitEffect,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );

            Destroy(fire, shooterData.effectLifetime);
            Destroy(hitFX, shooterData.effectLifetime);

            if (hit.transform.CompareTag("Player"))
            {
                PlayerHealth.instance.TakeDamageP(1);
            }
            else
            {
                Debug.Log("Player hit");
                Debug.Log(hit.transform.name);
                Debug.Log("no Dam");
            }
        }
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

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(2f);
        GetComponent<CapsuleCollider>().enabled = false;

        // Drop item based on drop rate
        int roll = Random.Range(1, 101);

        if (roll <= dropRatePercent)
        {
            GameObject pickupGun = Instantiate(
                gunPickup,
                transform.position + Vector3.up,
                Quaternion.identity
            );

            pickupGun.GetComponent<GunPickup>().pickupTextUI = gunPickupUI;
        }
    }
}
