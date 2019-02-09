using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<MeshFilter>() == null)
        {
            Debug.LogWarning("This Explodable does not have a mesh filter, disabling");
            this.enabled = false;
        }
    }
}
