using UnityEngine;

public class AimTarget : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;

    private void Update()
    {
        Ray Ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Ray, out RaycastHit HitInfo))
        {
            transform.position = HitInfo.point;
        }
    }
}
