using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Fire;
    public GameObject HitPoint;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Shooting();

        }
            
    }

    public void Shooting() 
    {
     RaycastHit hit;

        if(Physics.Raycast(FirePoint.position, transform.TransformDirection(Vector3.forward), out hit, 100f))
        {
            Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);


            GameObject a = Instantiate(Fire, FirePoint.position, Quaternion.identity);
            GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);

            Destroy(a, 1f);
            Destroy(b, 1f);

        }


    }



}
