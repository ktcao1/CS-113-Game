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

    // Interactable UI
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

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

    // TODO: Change into a full menu in the future
    public void PauseGame()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void EndScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");

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
