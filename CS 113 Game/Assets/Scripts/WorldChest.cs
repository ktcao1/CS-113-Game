using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldChest : MonoBehaviour
{
    public Player player;
    public Weapon weapon;
    public bool opened = false;
    public bool inWindow = false;
    public Sprite openedChestSprite;

    // UI
    private GameObject chestPanel;
    private GameObject confirmPanel;
    private GameObject option1, option2;
    private int option1IND, option2IND;
    private GameObject currentWindow;

    // Rewards
    [SerializeField] private List<Sprite> rewardSprites = new List<Sprite>();
    [SerializeField] private List<string> rewardTitles = new List<string>();
    [SerializeField] private List<string> rewardDescriptions = new List<string>();

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon = player.GetComponentInChildren<Weapon>();
        chestPanel = GameObject.FindGameObjectWithTag("WorldChestPanel").transform.Find("ChestPanel").gameObject;
        confirmPanel = GameObject.FindGameObjectWithTag("WorldChestPanel").transform.Find("ConfirmPanel").gameObject;
        option1 = chestPanel.transform.Find("Option1").gameObject;
        option2 = chestPanel.transform.Find("Option2").gameObject;

        // Set Buttons
        option1.GetComponent<Button>().onClick.AddListener(delegate{SetupConfirm("option1");});
        option2.GetComponent<Button>().onClick.AddListener(delegate{SetupConfirm("option2");});

        // Randomized reward
        option1IND = Random.Range(0, 3);
        while (true)
        {
            option2IND = Random.Range(0, 3);
            if (option2IND != option1IND) break;
        }

        // Sprite
        option1.GetComponent<Image>().sprite = rewardSprites[option1IND];
        option2.GetComponent<Image>().sprite = rewardSprites[option2IND];

        // Title
        option1.GetComponentInChildren<TMP_Text>().text = rewardTitles[option1IND];
        option2.GetComponentInChildren<TMP_Text>().text = rewardTitles[option2IND];

        currentWindow = chestPanel;
    }
    
    void Update()
    {
        if (opened && gameObject.GetComponent<SpriteRenderer>().sprite != openedChestSprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openedChestSprite;
        }
    }

    public void SwitchWindowStarter()
    {
        currentWindow.SetActive(false);
        currentWindow = chestPanel;
        currentWindow.SetActive(true);
    }

    public void SetupConfirm(string option)
    {
        confirmPanel.transform.Find("GoBack").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        confirmPanel.transform.Find("Confirm").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

        if (option == "option1")
        {
            confirmPanel.transform.Find("ChoiceText").gameObject.GetComponent<TMP_Text>().text = rewardDescriptions[option1IND];
            confirmPanel.transform.Find("Image").gameObject.GetComponent<Image>().sprite = rewardSprites[option1IND];
            confirmPanel.transform.Find("GoBack").gameObject.GetComponent<Button>().onClick.AddListener(SwitchWindowStarter);
            confirmPanel.transform.Find("Confirm").gameObject.GetComponent<Button>().onClick.AddListener(delegate{ConfirmSelection(option1IND);});
        }
        else
        {
            confirmPanel.transform.Find("ChoiceText").gameObject.GetComponent<TMP_Text>().text = rewardDescriptions[option2IND];
            confirmPanel.transform.Find("Image").gameObject.GetComponent<Image>().sprite = rewardSprites[option2IND];
            confirmPanel.transform.Find("GoBack").gameObject.GetComponent<Button>().onClick.AddListener(SwitchWindowStarter);
            confirmPanel.transform.Find("Confirm").gameObject.GetComponent<Button>().onClick.AddListener(delegate{ConfirmSelection(option2IND);});
        }
        SwitchWindowConfirm();
    }

    public void SwitchWindowConfirm()
    {
        currentWindow.SetActive(false);
        currentWindow = confirmPanel;
        currentWindow.SetActive(true);
    }

    public void ConfirmSelection(int option)
    {
        if (rewardTitles[option] == "+1 Heart")
        {
            ZeldaHealthBar.instance.AddContainer();
        }
        else if (rewardTitles[option] == "Heal +2")
        {
            ZeldaHealthBar.instance.AddHearts(2);
        }
        else if (rewardTitles[option] == "Heal +3")
        {
            ZeldaHealthBar.instance.AddHearts(3);
        }
        opened = true;
        CloseWindow();
    }

    void CashOut()
    {
        chestPanel.SetActive(true);
        currentWindow = chestPanel;
    }

    public void CloseWindow()
    {
        currentWindow.SetActive(false);
        inWindow = false;
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
