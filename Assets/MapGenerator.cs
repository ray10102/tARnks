using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

//Generate grid with the layout of where buildings and streets will be placed
public class MapGenerator : MonoBehaviour
{

    [SerializeField] private int mapWidth, mapLength, minStreetWidth, maxStreetWidth, minBuildingWidth, maxBuildingWidth;

    [SerializeField] private bool makeMapOnStart;
    
    private int[] lengths;

    private int[] widths;
    
    [SerializeField] private GameObject Spawn1;
    [SerializeField] private GameObject Spawn2;

    [HideInInspector]
    public int totalLength;
    [HideInInspector]
    public int totalWidth;

    private bool madeMap;

    private BuildingCreator buildingCreator;

    [SerializeField] private bool disableMap;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
        if (makeMapOnStart)
        {
            MakeMap();
        }


        buildingCreator = GetComponent<BuildingCreator>();
    }

    public void MakeMap()
    {
        if (madeMap)
        {
            return;
        }
        widths = new int[mapWidth];
        lengths = new int[mapLength];

        Vector2 s1 = new Vector2(Mathf.Ceil(mapWidth / 2f) - 2, Mathf.Ceil(mapLength / 2f));
        Vector2 s2 = new Vector2(Mathf.Ceil(mapWidth / 2f), Mathf.Ceil(mapLength / 2f));

        for (int x = 0; x < mapWidth; x++)
        {
            if (x % 2 == 0)
            {
                widths[x] = Random.Range(minBuildingWidth, maxBuildingWidth);
            }
            else
            {
                widths[x] = Random.Range(minStreetWidth, maxStreetWidth);
            }
        }

        for (int x = 0; x < mapLength; x++)
        {
            if (x % 2 == 0)
            {
                lengths[x] = Random.Range(minBuildingWidth, maxBuildingWidth);
            }
            else
            {
                lengths[x] = Random.Range(minStreetWidth, maxStreetWidth);
            }
        }

        int l = 0;
        for (int i = 0; i < mapLength; i++)
        {
            l += lengths[i];
            int w = 0; // accumulating a width to calculate the next building's position.
            for (int j = 0; j < mapWidth; j++)
            {
                w += widths[j];
                if (i % 2 == 0 && j % 2 == 0)
                {   
                    BuildingCreator.MakeBuilding(new Vector3(w - (widths[j] / 2f), 0, l - (lengths[i] / 2)),
                        new Vector2(widths[j], lengths[i]));
                }
                else
                {
                    if (s1.x == j && s1.y == i)
                    {
                        Spawn1.transform.localPosition =
                            new Vector3(w - (widths[j] / 2f), 0, l - (lengths[i] / 2)) * BuildingCreator.unit;
                        Debug.Log("Adding Spawn Point");
                    }

                    if (s2.x == j && s2.y == i)
                    {
                        Spawn2.transform.localPosition =
                            new Vector3(w - (widths[j] / 2f), 0, l - (lengths[i] / 2)) * BuildingCreator.unit;
                        Debug.Log("Adding Spawn Point");
                    }
                }
                totalWidth = w;
            }
        }
        totalLength = l;
        madeMap = true;

        if (!buildingCreator)
        {
            buildingCreator = GetComponent<BuildingCreator>();
        }
        Debug.Log(buildingCreator);
        buildingCreator.transform.localPosition = buildingCreator.buildingParent.transform.localPosition - new Vector3(
                                                      (totalWidth * BuildingCreator.unit) / 2, 0,
                                                      (totalLength * BuildingCreator.unit) / 2);
        buildingCreator.buildingParent.gameObject.SetActive(!disableMap);
    }

    public void TurnOnMap()
    {
        buildingCreator.buildingParent.gameObject.SetActive(true);
    }
}