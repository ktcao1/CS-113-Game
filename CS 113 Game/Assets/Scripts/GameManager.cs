using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Resources
    [SerializeField] private List<Sprite> weaponSprites;
    
    [SerializeField] private Player player;

    // TODO
    public void SaveState()
    {

    }

    // TODO
    public void LoadState()
    {

    }
}
