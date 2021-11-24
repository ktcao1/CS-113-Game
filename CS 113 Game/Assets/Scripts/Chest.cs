using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Player player;
    public bool opened = false;
    public Sprite openedChestSprite;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void CashOut()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!opened && collider.tag == "Player" && Input.GetKeyDown(player.interactKey))
        {
            opened = true;
            CashOut();
            gameObject.GetComponent<SpriteRenderer>().sprite = openedChestSprite;
            Debug.Log("opened!");
        }
    }
}
