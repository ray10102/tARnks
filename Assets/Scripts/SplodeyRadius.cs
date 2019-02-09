using System.Collections;
using System.Collections.Generic;
using Parabox.CSG;
using UnityEngine;

public class SplodeyRadius : MonoBehaviour
{
    [SerializeField] private MeshFilter splosion;
    // Start is called before the first frame update
    void Start()
    {
        if (splosion == null)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            splosion = sphere.GetComponent<MeshFilter>();
            sphere.SetActive(false);
        }
    }

    public void Explode(Vector3 position, float radius, MeshFilter toSplode)
    {
        splosion.gameObject.SetActive(true);
        splosion.transform.position = position;
        splosion.transform.localScale = new Vector3(radius, radius, radius);
        toSplode.mesh = CSG.Subtract(splosion.gameObject, toSplode.gameObject);
        splosion.gameObject.SetActive(false);
    }
}
