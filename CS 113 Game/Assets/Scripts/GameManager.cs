using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Resources
    [SerializeField] private List<Sprite> weaponSprites;
    [SerializeField] private Player player;
    private RoomTemplates roomTemplates;
    public bool isLoading = true;

    // Interactable UI
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject loadingPanel;
    private Animator pausePanelAnim;

    // UI
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        pausePanelAnim = pausePanel.GetComponent<Animator>();
        loadingPanel.SetActive(true);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus && !isLoading && !pausePanelAnim.GetBool("show"))
        {
            PauseGame();
        }
    }

    // TODO: Change into a full menu in the future
    public void PauseGame()
    {
        if (pausePanelAnim.GetBool("show"))
        {
            Time.timeScale = 1;
            pausePanelAnim.SetBool("show", false);
        }
        else
        {
            Time.timeScale = 0;
            pausePanelAnim.SetBool("show", true);
        }
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneAsync("TitleScene"));
    }

    public void EndScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void VictoryScreen()
    {
        victoryPanel.SetActive(true);
    }

    public void RestartScene()
    {
        StartCoroutine(LoadSceneAsync("SampleScene"));
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // TODO: Change into different UI in the future
    public void IncrementScore()
    {
        int score = Int32.Parse(scoreText.text.Substring(16)) + 1;
        scoreText.text = "Goblins Killed: " + score;
        if (score == roomTemplates.rooms.Count - 1)
        {
            Time.timeScale = 0;
            VictoryScreen();
        }
    }

    // TODO
    public void SaveState()
    {

    }

    // TODO
    public void LoadState()
    {

    }
}
