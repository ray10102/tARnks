﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generate grid with the layout of where buildings and streets will be placed
public class MapGenerator : MonoBehaviour
{

    [SerializeField] private int mapWidth, mapLength, minStreetWidth, maxStreetWidth, minBuildingWidth, maxBuildingWidth;

    private int[] lengths;

    private int[] widths;

    [HideInInspector]
    public int totalLength;
    [HideInInspector]
    public int totalWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");

        widths = new int[mapWidth];
        lengths = new int[mapLength];
        
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
                totalWidth = w;
            }
        }
        
        totalLength = l;
    }
}