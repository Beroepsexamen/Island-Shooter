using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HostageCollection : MonoBehaviour
{
    [SerializeField] private Transform collectionPoint;
    [SerializeField] private GameObject hostageParent;

    private int hostageCount;
    private int hostagesCollected;

    private bool hostagesAtCollectionPoint = false;

    private void Start()
    {
        hostageCount = hostageParent.transform.childCount;
        hostagesCollected = 0;
    }

    private void Update()
    {
        if (hostagesCollected >= hostageCount && hostagesAtCollectionPoint)
        {
            SceneManager.LoadScene("Victory");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Victory");
        }
    } 

    // When a hostage enters the trigger, start the collection process
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hostage"))
        {
            StartCoroutine(CollectHostage(other.transform));
        }
    }
    
    // Collects the hostage to set location
    private IEnumerator CollectHostage(Transform hostage)
    {
        NavMeshAgent agent = hostage.GetComponent<NavMeshAgent>();
        Animator animator = hostage.GetComponent<Animator>();

        hostage.GetComponent<HostageController>().enabled = false;
        agent.SetDestination(collectionPoint.position);
        agent.stoppingDistance = 0.1f;

        hostagesCollected++;

        yield return new WaitUntil(() => agent.velocity.magnitude <= 0.1f);

        hostagesAtCollectionPoint = true;
        agent.isStopped = true;

        animator.SetFloat("xVelocity", 0);
        animator.SetFloat("yVelocity", 0);
    }
}
