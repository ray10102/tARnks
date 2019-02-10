using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class BuildingCreator : MonoBehaviour
{
    private static BuildingCreator instance;

    [SerializeField] private Transform buildingParent;

    [SerializeField] private GameObject[] buildings;

    [SerializeField] private GameObject[] roofs;

    [SerializeField] private GameObject[] window;

    [SerializeField] private float roofHeightThreshhold = 6;

    [SerializeField] private float roofProbability = .4f;

    [SerializeField] private float windowProbability = .3f;

    [SerializeField] private float addAllWindowsProbability = .2f;

    [SerializeField] private Color[] frameColors, innerColors, buildingColors;

    private static int maxRoofHeight = 10;
    private static int minRoofHeight = 3;
    private static float tinyWindowOffset = .0001f;

    private static float unit = .125f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void MakeBuilding(Vector3 position, Vector2 dimensions)
    {
        Vector3 dimensionsWithRoof =
            new Vector3(dimensions.x, Random.Range(minRoofHeight, maxRoofHeight), dimensions.y);
        instance._MakeBuilding(position, dimensionsWithRoof);
    }

    public static void MakeBuilding(Vector3 position, Vector3 dimensions)
    {
        instance._MakeBuilding(position, dimensions);
    }

    private void _MakeBuilding(Vector3 position, Vector3 dimensions)
    {
        Color buildingColor = buildingColors[Random.Range(0, buildingColors.Length)];
        int buildingIndex = MakeBase(position, dimensions, buildingColor);

        MakeRoof(position, dimensions, buildingIndex, buildingColor);

        MakeWindows(position, dimensions);
    }

    #region make helpers
    
    private int MakeBase(Vector3 position, Vector3 dimensions, Color buildingColor)
    {
        int buildingIndex = Random.Range(0, buildings.Length);
        GameObject obj = Instantiate(buildings[buildingIndex], buildingParent) as GameObject;
        obj.transform.localPosition = position * unit;
        obj.transform.localScale = dimensions;
        SetColor(obj, buildingColor);
        return buildingIndex;
    }

    private void MakeRoof(Vector3 position, Vector3 dimensions, int buildingIndex, Color buildingColor)
    {
        if (dimensions.y > roofHeightThreshhold && buildingIndex == 2 && Random.Range(0f, 1f) < roofProbability)
        {
            GameObject roof = Instantiate(roofs[Random.Range(0, roofs.Length)], buildingParent);
            roof.transform.localPosition = new Vector3(position.x, dimensions.y, position.z) * unit;
            dimensions.y = Mathf.Min(dimensions.x, dimensions.z);
            roof.transform.localScale = dimensions;
            SetColor(roof, buildingColor);
        }
    }

    private void MakeWindows(Vector3 position, Vector3 dimensions)
    {
        bool addAllWindows = Random.Range(0, 1f) < addAllWindowsProbability;
        if (addAllWindows || Random.Range(0, 1f) < windowProbability)
        {
            Color windowFrame = frameColors[Random.Range(0, frameColors.Length)];
            Color innerColor = innerColors[Random.Range(0, innerColors.Length)];

            for (int y = 0; y < dimensions.y; y++)
            {
                MakeWindowRow(position, dimensions, windowFrame, innerColor, y, addAllWindows);
            }
        }
    }

    private void MakeWindowRow(Vector3 position, Vector3 dimensions, Color windowFrame, Color innerColor, int y, bool addAllWindows)
    {
        for (int x = 0; x < dimensions.x; x++)
        {
            if (addAllWindows || Random.Range(0, 1f) < windowProbability)
            {
                GameObject w1 = Instantiate(window[Random.Range(0, window.Length)], buildingParent) as GameObject;
                w1.transform.localPosition =
                    new Vector3((unit / 2 * (dimensions.x - 1) - unit * x) + (position.x * unit), unit / 2 + y * unit,
                        (unit / 2 * dimensions.z) - (unit * dimensions.z) + (position.z * unit) - tinyWindowOffset);
                w1.transform.eulerAngles = new Vector3(0, 90, 0);
                SetWindowColor(w1, windowFrame, innerColor);
            }

            if (addAllWindows || Random.Range(0, 1f) < windowProbability)
            {
                GameObject w2 = Instantiate(window[Random.Range(0, window.Length)], buildingParent) as GameObject;
                w2.transform.localPosition =
                    new Vector3((unit / 2 * (dimensions.x - 1) - unit * x) + (position.x * unit), unit / 2 + y * unit,
                        (unit / 2 * dimensions.z) + (position.z * unit) + tinyWindowOffset);
                w2.transform.eulerAngles = new Vector3(0, -90, 0);
                SetWindowColor(w2, windowFrame, innerColor);
            }
        }

        for (int z = 0; z < dimensions.z; z++)
        {
            if (addAllWindows || Random.Range(0, 1f) < windowProbability)
            {
                GameObject w1 = Instantiate(window[Random.Range(0, window.Length)], buildingParent) as GameObject;
                w1.transform.localPosition =
                    new Vector3(((unit / 2 * dimensions.x) - (unit * dimensions.x)) + (position.x * unit) - tinyWindowOffset,
                        unit / 2 + y * unit, (unit / 2 * (dimensions.z - 1) - unit * z) + (position.z * unit));
                w1.transform.eulerAngles = new Vector3(0, 180, 0);
                SetWindowColor(w1, windowFrame, innerColor);
            }

            if (addAllWindows || Random.Range(0, 1f) < windowProbability)
            {
                GameObject w2 = Instantiate(window[Random.Range(0, window.Length)], buildingParent) as GameObject;
                w2.transform.localPosition = new Vector3((unit / 2 * dimensions.x) + (position.x * unit) + tinyWindowOffset,
                    unit / 2 + y * unit, (unit / 2 * (dimensions.z - 1) - unit * z) + (position.z * unit));
                w2.transform.eulerAngles = new Vector3(0, 0, 0);
                SetWindowColor(w2, windowFrame, innerColor);
            }
        }
    }

    private void SetColor(GameObject obj, Color color)
    {
        obj.GetComponent<MeshRenderer>().material.color = color;
    }

    private void SetWindowColor(GameObject obj, Color color, Color innerColor)
    {
        obj.GetComponent<MeshRenderer>().material.color = color;
    }
    
    #endregion
}