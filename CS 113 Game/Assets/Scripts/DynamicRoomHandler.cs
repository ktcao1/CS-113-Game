using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DynamicRoomHandler : MonoBehaviour
{
    public Player player;
    public GameObject doors, frontDoors;
    public GameObject goblin, obstacle, waypoint, worldChest;
    public bool roomCleared = false;
    public bool enemiesSpawned = false;
    public bool doorsActive = false;
    public string roomType;
    public List<GameObject> enemiesInRoom = new List<GameObject>();
    public Transform spawnables;
    public GameObject enemies;

    public References references;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawnables = transform.Find("Spawnables");
        references = GameObject.FindGameObjectWithTag("References").GetComponent<References>();
        // Set room type
        roomType = "Enemies";

        // Create holder for enemies
        enemies = new GameObject();
        enemies.name = "Enemies";
        enemies.transform.parent = this.gameObject.transform;

        // Doors closed by default
        doors = transform.Find("Doors").gameObject;
        frontDoors = transform.Find("FrontDoors").gameObject;
        doors.SetActive(false);
        frontDoors.SetActive(false);

        // Spawn random amount of enemies
        goblin = references.enemyDict["goblin"];
        obstacle = references.objectDict["obstacle"];
        waypoint = references.objectDict["waypoint"];
        worldChest = references.objectDict["world_chest"];
    }

    void Update()
    {
        if (roomCleared) return;
        if (gameObject.name == "TBLR") 
        {
            roomCleared = true;
            GameManager.instance.roomsCleared++;
        }
        else if (player.currentRoom == gameObject && !doorsActive)
        {
            GameObject roomPortals = player.currentRoom.transform.Find("Portals").gameObject;
            roomPortals.SetActive(false);
            doors.SetActive(true);
            frontDoors.SetActive(true);
            doorsActive = true;
        }
        else if (enemies.transform.childCount == 0 && enemiesSpawned)
        {
            GameObject roomPortals = player.currentRoom.transform.Find("Portals").gameObject;
            roomPortals.SetActive(true);
            doors.SetActive(false);
            frontDoors.SetActive(false);
            roomCleared = true;
            GameManager.instance.roomsCleared++;
            if (GameManager.instance.roomsCleared % 2 == 0) Instantiate(worldChest, transform.position, Quaternion.identity);
        }
        else if (player.currentRoom == gameObject && doorsActive && !enemiesSpawned)
        {
            // Invoke("SpawnEnemies", 0.5f);
            SpawnEnemiesAndObstacles();
        }
    }

    void SpawnEnemiesAndObstacles()
    {
        List<Vector3> spawnedLocs = new List<Vector3>();

        int randGoblins = Random.Range(1, 4);
        Vector3 roomCenter = this.gameObject.transform.position;
        for (int i = 0; i < randGoblins; i++)
        {
            while (true)
            {
                bool safe = true;
                float randX = Random.Range(roomCenter.x-2f, roomCenter.x+2f);
                float randY = Random.Range(roomCenter.y-2f, roomCenter.y+2f);
                Vector3 randLoc = new Vector3(randX, randY, roomCenter.z);
                foreach (Vector3 loc in spawnedLocs)
                {
                    if (Vector3.Distance(randLoc, loc) < 1.5f)
                    {
                        safe = false;
                        break;
                    }
                }
                if (!safe) continue;
                GameObject go = Instantiate(goblin, randLoc, Quaternion.identity);
                go.transform.parent = enemies.transform;
                spawnedLocs.Add(randLoc);
                break;
            }
        }

        int randObstacles = Random.Range(1, 5);
        for (int i = 0; i < randObstacles; i++)
        {
            while (true)
            {
                bool safe = true;
                float obX = Random.Range(roomCenter.x-3f, roomCenter.x+3f);
                float obY = Random.Range(roomCenter.y-3f, roomCenter.y+3f);
                Vector3 randObjLoc = new Vector3(obX, obY, roomCenter.z);
                foreach (Vector3 loc in spawnedLocs)
                {
                    if (Vector3.Distance(randObjLoc, loc) < 1.5f || Vector3.Distance(randObjLoc, roomCenter) < 2f)
                    {
                        safe = false;
                        break;
                    }
                }
                if (!safe) continue;
                GameObject goObj = Instantiate(obstacle, randObjLoc, Quaternion.identity);
                goObj.transform.parent = spawnables.transform;
                spawnedLocs.Add(randObjLoc);
                break;
            }
        }

        enemiesSpawned = true;
    }
}
