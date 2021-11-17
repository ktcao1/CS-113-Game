using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Player player;
    public int openingDirection;
    public GameObject portalExit;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            float x = portalExit.GetComponentInParent<AddRoom>().room.transform.position.x;
            float y = portalExit.GetComponentInParent<AddRoom>().room.transform.position.y;
            
            player.currentRoom = portalExit.GetComponentInParent<AddRoom>().room;
            
            player.transform.position = portalExit.transform.position;
            float saveZ = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(x, y, saveZ);
        }
    }
}
