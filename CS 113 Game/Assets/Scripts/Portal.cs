using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Transform portal;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            player.transform.position = portal.transform.position;
            Camera.main.transform.position += new Vector3(-22, 0, 0);
        }
    }
}
