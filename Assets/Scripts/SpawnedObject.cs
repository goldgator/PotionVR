using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    private Spawner parentSpawner;

    private void OnDestroy()
    {
        parentSpawner.Spawn();
    }

    public void SetSpawner(Spawner parent)
    {
        parentSpawner = parent;
    }
}
