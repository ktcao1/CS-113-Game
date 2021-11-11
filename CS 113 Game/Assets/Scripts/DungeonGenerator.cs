using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public Queue<RoomSpawner> spawnPointers = new Queue<RoomSpawner>();
    public int count = 0;
    public float lastSpawned = -1f;
    public float cooldown = 1f;

    void Update()
    {
        if (spawnPointers.Count > 0)
        {
            RoomSpawner rs = spawnPointers.Dequeue();

            if (count < 4)
            {
                rs.SpawnBeginning();
            }
            else
            {
                rs.Spawn();
            }
            count++;
        }
    }
}
