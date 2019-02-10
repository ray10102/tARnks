﻿using System.Collections;
using System.Collections.Generic;
using Parabox.CSG;
using UnityEngine;

public class SplodeyRadius : MonoBehaviour
{
    [SerializeField] private MeshFilter splosion;

    public static SplodeyRadius instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("There is are two SplodeyRadii in the scene");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        splosion = GetComponent<MeshFilter>();
        if (splosion == null)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            splosion = sphere.GetComponent<MeshFilter>();
            sphere.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col);
        if (col.GetComponent<Explodable>()) // All explodables have mesh filters
        {
            Explode(transform.position, .1f, col.GetComponent<MeshFilter>());
        }
    }

    public static void Explode(Vector3 position, float radius)
    {
        instance.SetLocation();
    }

    private void SetLocation()
    {
        
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
