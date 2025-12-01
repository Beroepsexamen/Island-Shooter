using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera fpsCam;
    public Transform FirePoint;
    public GameObject Fire;
    public GameObject HitPoint;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();

        }
    }        

    public void Shooting() 
    {
     RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 100f))
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.yellow);


            GameObject a = Instantiate(Fire, FirePoint.position, Quaternion.identity);
            GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);

            Destroy(a, 1f);
            Destroy(b, 1f);

        }


    }



}
