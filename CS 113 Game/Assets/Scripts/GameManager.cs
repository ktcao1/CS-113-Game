using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Resources
    [SerializeField] private List<Sprite> weaponSprites;
    [SerializeField] private Player player;

    // Pause
    [SerializeField] private GameObject pausePanel;
    private bool paused;

    // UI
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        paused = false;
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // TODO: Change into a full menu in the future
    public void PauseGame()
    {
        if (paused)
        {
            paused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            paused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
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
