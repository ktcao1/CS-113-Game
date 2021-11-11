using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float waitTime = 5f;
    private bool removed = false;

    void Update()
    {
        if (waitTime <= 0 && !removed)
        {
            finalRooms.RemoveAt(0);
            finalRooms.RemoveAt(0);
            Queue<GameObject> destroyedRooms = new Queue<GameObject>();

            for (int i = 0; i < finalRooms.Count; i++)
            {
                char op = finalRooms[i].GetComponent<AddRoom>().op;
                if (op != finalRooms[i].name[0])
                {
                    if (op == 'B') Instantiate(bottom, finalRooms[i].transform.position, Quaternion.identity);
                    if (op == 'T') Instantiate(top, finalRooms[i].transform.position, Quaternion.identity);
                    if (op == 'L') Instantiate(left, finalRooms[i].transform.position, Quaternion.identity);
                    if (op == 'R') Instantiate(right, finalRooms[i].transform.position, Quaternion.identity);
                }
                else if (op != finalRooms[i].name[1])
                {
                    if (op == 'B') Instantiate(bottom, finalRooms[i].transform.position, Quaternion.identity);
                    if (op == 'T') Instantiate(top, finalRooms[i].transform.position, Quaternion.identity);
                    if (op == 'L') Instantiate(left, finalRooms[i].transform.position, Quaternion.identity);
                    if (op == 'R') Instantiate(right, finalRooms[i].transform.position, Quaternion.identity);
                }
                destroyedRooms.Enqueue(finalRooms[i]);
            }
            removed = true;

            while (destroyedRooms.Count > 0)
            {
                GameObject destroyedRoom = destroyedRooms.Dequeue();
                Destroy(destroyedRoom);
            }

            finalRooms.Clear();
        }
        else if (removed)
        {
            return;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
