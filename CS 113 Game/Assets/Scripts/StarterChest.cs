using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterChest : MonoBehaviour
{
    public Player player;
    public Weapon weapon;
    public bool opened = false;
    public bool inWindow = false;
    public Sprite openedChestSprite;

    // UI
    [SerializeField] private GameObject starterPanel;
    [SerializeField] private GameObject knockBackPanel;
    [SerializeField] private GameObject heartsPanel;
    [SerializeField] private GameObject bowPanel;
    [SerializeField] private Sprite bowSprite;
    private GameObject currentWindow;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon = player.GetComponentInChildren<Weapon>();
        currentWindow = starterPanel;
    }

    void Update()
    {
        if (opened && gameObject.GetComponent<SpriteRenderer>().sprite != openedChestSprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openedChestSprite;
        }
    }

    void CashOut()
    {
        starterPanel.SetActive(true);
        currentWindow = starterPanel;
    }

    public void CloseWindow()
    {
        currentWindow.SetActive(false);
        inWindow = false;
    }

    public void SwitchWindowStarter()
    {
        currentWindow.SetActive(false);
        currentWindow = starterPanel;
        currentWindow.SetActive(true);
    }

    public void SwitchWindowKnockBack()
    {
        currentWindow.SetActive(false);
        currentWindow = knockBackPanel;
        currentWindow.SetActive(true);
    }

    public void SwitchWindowHearts()
    {
        currentWindow.SetActive(false);
        currentWindow = heartsPanel;
        currentWindow.SetActive(true);
    }

    public void SwitchWindowBow()
    {
        currentWindow.SetActive(false);
        currentWindow = bowPanel;
        currentWindow.SetActive(true);
    }

    public void ConfirmKnockBack()
    {
        player.hasKnockBack = true;
        opened = true;
        CloseWindow();
    }

    public void ConfirmHearts()
    {
        ZeldaHealthBar.instance.AddContainer();
        ZeldaHealthBar.instance.AddContainer();
        opened = true;
        CloseWindow();
    }

    public void ConfirmBow()
    {
        weapon.GetComponent<SpriteRenderer>().sprite = bowSprite;
        weapon.weaponType = "bow";
        weapon.attackIcon.sprite = weapon.bowSprite;
        weapon.animator.SetBool("Bow", true);
        opened = true;
        CloseWindow();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!opened && collider.tag == "Player" && Input.GetKeyDown(player.interactKey) && !inWindow)
        {
            inWindow = true;
            CashOut();
        }
        else if (!opened && collider.tag == "Player" && Input.GetKeyDown(player.interactKey) && inWindow)
        {
            inWindow = false;
            CloseWindow();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (!opened && collider.tag == "Player")
        {
            CloseWindow();
        }
    }
}
