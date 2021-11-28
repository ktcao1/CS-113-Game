using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject bottom;
    public GameObject top;
    public GameObject left;
    public GameObject right;

    public GameObject[] closedRooms;
    public List<GameObject> rooms;
    public List<GameObject> finalRooms;

    private float waitTime = 2f;
    private bool removed = false;

    void Update()
    {
        if (waitTime <= 0 && !removed)
        {
            Debug.Log(rooms[rooms.Count-1].name);
            finalRooms.RemoveAt(0);
            finalRooms.RemoveAt(0);
            Queue<GameObject> destroyedRooms = new Queue<GameObject>();

            for (int i = 0; i < finalRooms.Count; i++)
            {
                char op = finalRooms[i].GetComponent<AddRoom>().op;
                if (op != finalRooms[i].name[0] || op != finalRooms[i].name[1])
                {
                    if (op == 'B') // openingDirection == 1
                    {
                        GameObject go = Instantiate(bottom, finalRooms[i].transform.position, Quaternion.identity);
                        go.GetComponent<AddRoom>().mmSquare.transform.position = RoomSpawner.FindMMSpawnPoint(finalRooms[i].GetComponent<AddRoom>().prevRoom.GetComponent<AddRoom>().mmSquare, 1).transform.position;
                        go.GetComponent<AddRoom>().prevRoom = finalRooms[i].GetComponent<AddRoom>().prevRoom;
                        RoomSpawner.FindPortal(go, 2).GetComponent<Portal>().portalExit = RoomSpawner.FindPortalExit(go.GetComponent<AddRoom>().prevRoom, 1); 
                        RoomSpawner.FindPortal(go.GetComponent<AddRoom>().prevRoom, 1).GetComponent<Portal>().portalExit = RoomSpawner.FindPortalExit(go, 2); 
                    } 
                    if (op == 'T') // openingDirection == 2
                    {
                        GameObject go = Instantiate(top, finalRooms[i].transform.position, Quaternion.identity);
                        go.GetComponent<AddRoom>().mmSquare.transform.position = RoomSpawner.FindMMSpawnPoint(finalRooms[i].GetComponent<AddRoom>().prevRoom.GetComponent<AddRoom>().mmSquare, 2).transform.position;
                        go.GetComponent<AddRoom>().prevRoom = finalRooms[i].GetComponent<AddRoom>().prevRoom;
                        RoomSpawner.FindPortal(go, 1).GetComponent<Portal>().portalExit = RoomSpawner.FindPortalExit(go.GetComponent<AddRoom>().prevRoom, 2); 
                        RoomSpawner.FindPortal(go.GetComponent<AddRoom>().prevRoom, 2).GetComponent<Portal>().portalExit = RoomSpawner.FindPortalExit(go, 1); 
                    } 
                    if (op == 'L') // openingDirection == 3
                    {
                        GameObject go = Instantiate(left, finalRooms[i].transform.position, Quaternion.identity);
                        go.GetComponent<AddRoom>().mmSquare.transform.position = RoomSpawner.FindMMSpawnPoint(finalRooms[i].GetComponent<AddRoom>().prevRoom.GetComponent<AddRoom>().mmSquare, 3).transform.position;
                        go.GetComponent<AddRoom>().prevRoom = finalRooms[i].GetComponent<AddRoom>().prevRoom;
                        RoomSpawner.FindPortal(go, 4).GetComponent<Portal>().portalExit = RoomSpawner.FindPortalExit(go.GetComponent<AddRoom>().prevRoom, 3);
                        RoomSpawner.FindPortal(go.GetComponent<AddRoom>().prevRoom, 3).GetComponent<Portal>().portalExit = RoomSpawner.FindPortalExit(go, 4); 
                    } 
                    if (op == 'R') // openingDirection == 4
                    {
                        GameObject go = Instantiate(right, finalRooms[i].transform.position, Quaternion.identity);
                        go.GetComponent<AddRoom>().mmSquare.transform.position = RoomSpawner.FindMMSpawnPoint(finalRooms[i].GetComponent<AddRoom>().prevRoom.GetComponent<AddRoom>().mmSquare, 4).transform.position;
                        go.GetComponent<AddRoom>().prevRoom = finalRooms[i].GetComponent<AddRoom>().prevRoom;
                        RoomSpawner.FindPortal(go, 3).GetComponent<Portal>().portalExit = RoomSpawner.FindPortalExit(go.GetComponent<AddRoom>().prevRoom, 4);
                        RoomSpawner.FindPortal(go.GetComponent<AddRoom>().prevRoom, 4).GetComponent<Portal>().portalExit = RoomSpawner.FindPortalExit(go, 3); 
                    } 
                }
                destroyedRooms.Enqueue(finalRooms[i]);
            }
            removed = true;

            while (destroyedRooms.Count > 0)
            {
                GameObject destroyedRoom = destroyedRooms.Dequeue();
                rooms.Remove(destroyedRoom);
                Destroy(destroyedRoom);
            }

            // roomTiles.PlaceTiles();
            finalRooms.Clear();
            // surface2D.BuildNavMesh();
        }
        else if (removed)
        {
            if (rooms.Count == GameManager.instance.roomsCleared)
            {
                GameManager.instance.VictoryScreen();
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
