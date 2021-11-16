using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DynamicRoomHandler : MonoBehaviour
{
    public GameObject doors, frontDoors;
    public GameObject goblin;
    public bool roomCleared = false;
    public string roomType;
    public List<GameObject> enemiesInRoom = new List<GameObject>();
    public GameObject enemies;

    public EnemyReferences enemyReferences;

    void Start()
    {
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
        if (goblin != null && gameObject.name != "TBLR")
        {
            GameObject go = Instantiate(goblin, this.gameObject.transform.position, Quaternion.identity);
            go.transform.parent = enemies.transform;
        }
    }

    void Update()
    {
        
    }
}
