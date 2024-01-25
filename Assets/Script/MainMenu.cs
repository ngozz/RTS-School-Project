using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject SettingScene;
    [SerializeField] private GameObject SelectMap;

    private void Start()
    {
        ActivateMainMenu(true);
    }


    public void ActivateMainMenu(bool state)
    {
        mainMenu.SetActive(state);
        SettingScene.SetActive(!state);
    }

    public void ActivateSelectMap(bool state)
    {
        mainMenu.SetActive(state);
        SelectMap.SetActive(!state);
    }
}
