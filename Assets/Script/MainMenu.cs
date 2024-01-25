using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject SettingScene;

    private void Start()
    {
        ActivateMainMenu(true);
    }


    public void ActivateMainMenu(bool state)
    {
        mainMenu.SetActive(state);
        SettingScene.SetActive(!state);
    }

    public void OpenScene3() {
        SceneManager.LoadScene("SelectMap");
    }
}
