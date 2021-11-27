using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Resources
    [SerializeField] private List<Sprite> weaponSprites;
    [SerializeField] private Player player;
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private Image musicSymbol;
    [SerializeField] private Sprite musicOn, musicOff;
    public int roomsCleared = 0;
    public bool disableInputs = false;
    public bool isLoading = true;
    public bool musicPaused = false;

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
        if (disableInputs) return;

        if (pausePanelAnim.GetBool("show"))
        {
            Time.timeScale = 1;
            pausePanelAnim.SetBool("show", false);
            if (!musicPaused) gameMusic.UnPause();
        }
        else
        {
            Time.timeScale = 0;
            pausePanelAnim.SetBool("show", true);
            gameMusic.Pause();
        }
    }

    public void PauseMusic()
    {
        if (!musicPaused)
        {
            musicSymbol.sprite = musicOff;
            gameMusic.Pause();
            musicPaused = true;
        }
        else
        {
            musicSymbol.sprite = musicOn;
            gameMusic.UnPause();
            musicPaused = false;
        }
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneAsync("TitleScene"));
    }

    public void EndScreen()
    {
        gameMusic.Pause();
        gameOverPanel.SetActive(true);
    }

    public void VictoryScreen()
    {
        disableInputs = true;
        gameMusic.Pause();
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
        // if (score == roomsCleared - 1)
        // {
        //     Time.timeScale = 0;
        //     VictoryScreen();
        // }
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
