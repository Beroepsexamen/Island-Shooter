using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HostageCollection : MonoBehaviour
{
    [SerializeField] private Transform collectionPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hostage"))
        {
            StartCoroutine(CollectHostage(other.transform));
        }
    }

    private IEnumerator CollectHostage(Transform hostage)
    {
        NavMeshAgent agent = hostage.GetComponent<NavMeshAgent>();
        Animator animator = hostage.GetComponent<Animator>();

        hostage.GetComponent<HostageController>().enabled = false;
        agent.SetDestination(collectionPoint.position);
        agent.stoppingDistance = 0.1f;

        yield return new WaitUntil(() => agent.velocity.magnitude <= 0.1f);

        agent.isStopped = true;

        animator.SetFloat("xVelocity", 0);
        animator.SetFloat("yVelocity", 0);
    }
}
