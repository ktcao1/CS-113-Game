using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Inputs
    public KeyCode upKey, downKey, leftKey, rightKey;
    public KeyCode fireKey;

    // Stats
    public int healthPoints;
    public int maxHealthPoints;

    private void Start()
    {
        upKey = KeyCode.W;
        downKey = KeyCode.S;
        leftKey = KeyCode.A;
        rightKey = KeyCode.D;
        fireKey = KeyCode.F;

        maxHealthPoints = 5;
        healthPoints = maxHealthPoints;
    }

    private void Update()
    {
        
    }
}
