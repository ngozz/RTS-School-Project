using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject pausedMenu;
    
    void Update (){
        HandleEscape();
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPausedMenu();
        }
    }

    public void ShowPausedMenu()
    {
        pausedMenu.SetActive(!pausedMenu.activeSelf);
        if (!pausedMenu.activeSelf)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    
    public void OpenScene5() {
        SceneManager.LoadScene("SelectMap");
    }

}