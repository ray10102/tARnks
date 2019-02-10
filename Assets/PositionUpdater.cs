using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdater : MonoBehaviour
{
    public GameObject gameObjectToBeMoved;

    public void Move()
    {
        gameObjectToBeMoved.transform.position = transform.position;
    }
}
