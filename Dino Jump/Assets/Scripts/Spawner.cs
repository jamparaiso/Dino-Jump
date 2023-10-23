using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject spawnObject;
        [Range(0f, 1f)] public float spawnChance;
    }

    public SpawnableObject[] objects;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        //random spawn rate
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        //randon spawn chance
        float spawnChance = Random.value;

        //iterate to each object until it spawns or reached until the end of the list
        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            { 
                GameObject obstacle = Instantiate(obj.spawnObject);
                obstacle.transform.position += transform.position;
                break; // breaks the loop if it spawns something
            }
            // didn't meet the condition above
            // deduct the spawn chance and move to the next index in the list
            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
