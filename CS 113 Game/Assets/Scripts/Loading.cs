using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    private float waitTime = 4f;

    void Update()
    {
        if (waitTime <= 0)
        {
            GameManager.instance.isLoading = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
