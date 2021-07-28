using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool spawnOnStart = true;
    public GameObject spawnedObject;

    private void Start()
    {
        if (spawnOnStart)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        Vector3 position = transform.position;
        GameObject gameObject = Instantiate(spawnedObject, position, Quaternion.identity);

        SpawnedObject spawn = gameObject.AddComponent<SpawnedObject>();
        spawn.SetSpawner(this);
    }
}
