using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 >> need bottom
    // 2 >> need top
    // 3 >> need left
    // 4 >> need right

    private RoomTemplates templates;
    private DungeonGenerator dungeonGenerator;
    public int rand;
    public bool spawned = false;
    public List<string> openings;

    void Start()
    {
        Destroy(gameObject, 5f);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        dungeonGenerator = GameObject.FindGameObjectWithTag("DungeonGenerator").GetComponent<DungeonGenerator>();
        dungeonGenerator.spawnPointers.Enqueue(this);
        // Invoke("Spawn", 0.1f);
    }

    public void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                GameObject go = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                go.GetComponent<AddRoom>().op = 'B';
                if (rand == 0) openings = new List<string>{"B"};
                if (rand == 1) openings = new List<string>{"B", "R"};
                if (rand == 2) openings = new List<string>{"B", "T"};
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                GameObject go = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                go.GetComponent<AddRoom>().op = 'T';
                if (rand == 0) openings = new List<string>{"T"};
                if (rand == 1) openings = new List<string>{"T", "B"};
                if (rand == 2) openings = new List<string>{"T", "L"};
                if (rand == 3) openings = new List<string>{"T", "R"};
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                GameObject go = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                go.GetComponent<AddRoom>().op = 'L';
                if (rand == 0) openings = new List<string>{"L"};
                if (rand == 1) openings = new List<string>{"L", "R"};
                if (rand == 2) openings = new List<string>{"L", "T"};
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                GameObject go = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                go.GetComponent<AddRoom>().op = 'R';
                if (rand == 0) openings = new List<string>{"R"};
                if (rand == 1) openings = new List<string>{"R", "B"};
                if (rand == 2) openings = new List<string>{"R", "L"};
                if (rand == 3) openings = new List<string>{"R", "T"};
            }
            spawned = true;
        }
    }

    public void SpawnBeginning()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                rand = Random.Range(1, templates.bottomRooms.Length);
                GameObject go = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                go.GetComponent<AddRoom>().op = 'B';
                if (rand == 1) openings = new List<string>{"B", "R"};
                if (rand == 2) openings = new List<string>{"B", "T"};
            }
            else if (openingDirection == 2)
            {
                rand = Random.Range(1, templates.topRooms.Length);
                GameObject go = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                go.GetComponent<AddRoom>().op = 'T';
                if (rand == 1) openings = new List<string>{"T", "B"};
                if (rand == 2) openings = new List<string>{"T", "L"};
                if (rand == 3) openings = new List<string>{"T", "R"};
            }
            else if (openingDirection == 3)
            {
                rand = Random.Range(1, templates.leftRooms.Length);
                GameObject go = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                go.GetComponent<AddRoom>().op = 'L';
                if (rand == 1) openings = new List<string>{"L", "R"};
                if (rand == 2) openings = new List<string>{"T", "L"};
            }
            else if (openingDirection == 4)
            {
                rand = Random.Range(1, templates.rightRooms.Length);
                GameObject go = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                go.GetComponent<AddRoom>().op = 'R';
                if (rand == 1) openings = new List<string>{"R", "B"};
                if (rand == 2) openings = new List<string>{"R", "L"};
                if (rand == 3) openings = new List<string>{"T", "R"};
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpawnPoint")
        {
            RoomSpawner otherSpawner = other.GetComponent<RoomSpawner>();

            if (!otherSpawner.spawned && !spawned)
            {
                if ((openingDirection == 1 && otherSpawner.openingDirection == 3) || (openingDirection == 3 && otherSpawner.openingDirection == 1))
                {
                    Instantiate(templates.closedRooms[0], transform.position, templates.closedRooms[0].transform.rotation);
                    openings = new List<string>{"B", "L"};
                }
                else if ((openingDirection == 1 && otherSpawner.openingDirection == 4) || (openingDirection == 4 && otherSpawner.openingDirection == 1))
                {
                    Instantiate(templates.closedRooms[1], transform.position, templates.closedRooms[1].transform.rotation);
                    openings = new List<string>{"R", "B"};
                }
                else if ((openingDirection == 2 && otherSpawner.openingDirection == 3) || (openingDirection == 3 && otherSpawner.openingDirection == 2))
                {
                    Instantiate(templates.closedRooms[2], transform.position, templates.closedRooms[2].transform.rotation);
                    openings = new List<string>{"T", "L"};
                }
                else if ((openingDirection == 2 && otherSpawner.openingDirection == 4) || (openingDirection == 4 && otherSpawner.openingDirection == 2))
                {
                    Instantiate(templates.closedRooms[3], transform.position, templates.closedRooms[3].transform.rotation);
                    openings = new List<string>{"R", "T"};
                }
            }
            if (otherSpawner.spawned && !spawned)
            {
                if (openingDirection == 1)
                {
                    if (!otherSpawner.openings.Contains("B"))
                    {
                        templates.finalRooms.Add(this.gameObject.GetComponentInParent<AddRoom>().room);
                    }
                }
                else if (openingDirection == 2)
                {
                    if (!otherSpawner.openings.Contains("T"))
                    {
                        templates.finalRooms.Add(this.gameObject.GetComponentInParent<AddRoom>().room);
                    }
                }
                else if (openingDirection == 3)
                {
                    if (!otherSpawner.openings.Contains("L"))
                    {
                        templates.finalRooms.Add(this.gameObject.GetComponentInParent<AddRoom>().room);
                    }
                }
                else if (openingDirection == 4)
                {
                    if (!otherSpawner.openings.Contains("R"))
                    {
                        templates.finalRooms.Add(this.gameObject.GetComponentInParent<AddRoom>().room);
                    }
                }
            }
            spawned = true;
            otherSpawner.spawned = true;
        }
    }
}
