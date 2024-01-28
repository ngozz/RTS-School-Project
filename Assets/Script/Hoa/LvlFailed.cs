using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LvlFailed : MonoBehaviour
{
    private bool gameEnded = false;
    [SerializeField] public GameObject gameOverUI;
    [SerializeField] private LevelManager lm;
    private void Update()
    {
        if(gameEnded) return;

        if(lm.LifeLeft <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        
        gameOverUI.SetActive(true);

        Time.timeScale = 0f;
    }
}
