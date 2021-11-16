using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReferences : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public Dictionary<string, GameObject> enemyDict = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemyDict.Add(enemy.name, enemy);
        }
    }
}
