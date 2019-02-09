using UnityEngine;
using System.Collections;
 
public class LookAtCameraYOnly : MonoBehaviour
{
    [SerializeField] private Camera cameraToLookAt;

    void Start()
    {
        if (cameraToLookAt == null)
        {
            cameraToLookAt = Camera.main;
        }
    }
    
    void Update() 
    {
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v); 
    }
}