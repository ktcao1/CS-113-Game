using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DynamicRoomHandler : MonoBehaviour
{
    public Player player;
    public GameObject doors, frontDoors;
    public GameObject goblin;
    public bool roomCleared = false;
    public bool enemiesSpawned = false;
    public bool doorsActive = false;
    public string roomType;
    public List<GameObject> enemiesInRoom = new List<GameObject>();
    public GameObject enemies;

    public EnemyReferences enemyReferences;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyReferences = GameObject.FindGameObjectWithTag("EnemyReferences").GetComponent<EnemyReferences>();
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
        goblin = enemyReferences.enemyDict["goblin"];
    }

    void Update()
    {
        if (roomCleared) return;
        if (player.currentRoom == gameObject && gameObject.name != "TBLR" && !doorsActive)
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
        }
        else if (player.currentRoom == gameObject && gameObject.name != "TBLR" && doorsActive && !enemiesSpawned)
        {
            int randGoblins = Random.Range(1, 4);
            Vector3 roomCenter = this.gameObject.transform.position;
            for (int i = 0; i < randGoblins; i++)
            {
                float randX = Random.Range(roomCenter.x-3f, roomCenter.x+3f);
                float randY = Random.Range(roomCenter.y-3f, roomCenter.y+3f);
                Vector3 randLoc = new Vector3(randX, randY, roomCenter.z);
                GameObject go = Instantiate(goblin, randLoc, Quaternion.identity);
                go.transform.parent = enemies.transform;
            }
            enemiesSpawned = true;
        }
    }
}
