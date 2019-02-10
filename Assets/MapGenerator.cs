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
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
        if (makeMapOnStart)
        {
            MakeMap();
        }


        buildingCreator = FindObjectOfType<BuildingCreator>();
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

        Debug.Log("s1 is " + s1);
        Debug.Log("s2 is " + s2);
        
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
                    if ((int)s1.x == j & (int)s1.y == i)
                    {
                        Spawn1.transform.localPosition =
                            new Vector3(w - (widths[j] / 2f), 0, l - (lengths[i] / 2)) * BuildingCreator.unit;
                    }
                    if ((int)s2.x == j && (int)s2.y == i)
                    {
                        Spawn2.transform.localPosition =
                            new Vector3(w - (widths[j] / 2f), 0, l - (lengths[i] / 2)) * BuildingCreator.unit;
                    }
                }
                totalWidth = w;
            }
        }
        totalLength = l;
        madeMap = true;

        GameManager gm = FindObjectOfType<GameManager>();
        gm.SpawnAllTanks();

        buildingCreator.transform.localPosition = buildingCreator.buildingParent.transform.localPosition - new Vector3(
                                                      (totalWidth * BuildingCreator.unit) / 2, 0,
                                                      (totalLength * BuildingCreator.unit) / 2);
    }
}