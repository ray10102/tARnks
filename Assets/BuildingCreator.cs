using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] buildings;

    [SerializeField] private GameObject[] roofs;

    [SerializeField] private GameObject[] window;

    [SerializeField] private float roofHeightThreshhold = 6;

    [SerializeField] private float roofProbability = .4f;

    [SerializeField] private float windowProbability = .7f;
    
    // Start is called before the first frame update
    void Start()
    {
        MakeBuilding(Vector3.zero, new Vector3(Random.Range(3, 8), Random.Range(3, 8), Random.Range(3, 8)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeBuilding(Vector3 position, Vector3 dimensions)
    {
        int buildingIndex = Random.Range(0, buildings.Length);
        GameObject obj = Instantiate(buildings[buildingIndex], transform) as GameObject;
        obj.transform.localPosition = position;
        obj.transform.localScale = dimensions;
        if (dimensions.y > roofHeightThreshhold && buildingIndex == 2 && Random.Range(0f, 1f) > roofProbability)
        {
            GameObject roof = Instantiate(roofs[Random.Range(0, roofs.Length)], obj.transform);
            roof.transform.localPosition = new Vector3(0, dimensions.y * 3, 0);
            dimensions.y = 3;
            roof.transform.localScale = dimensions;
        }

        if (Random.Range(0, 1f) > windowProbability)
         {
             
            
            for (int x = 0; x < dimensions.x; x++)
            {
                for (int y = 0; y < dimensions.y; y++)
                {
                    for (int z = 0; z < dimensions.z; z++)
                    {
                        GameObject w = Instantiate(window[Random.Range(0, window.Length)], obj.transform) as GameObject;
                        w.transform.localPosition = new Vector3( 1.5f * (dimensions.x - 1) - 3 * x, 1.5f + y * 3f, );
                    }
                }
            }
            
            Debug.Log("windows");
        }
    }
}
