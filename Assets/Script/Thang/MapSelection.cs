using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelection : MonoBehaviour
{

    public void OpenScene() {
        SceneManager.LoadScene("DuyScene");
        Time.timeScale = 1f;
    }

    public void OpenScene1() {
        SceneManager.LoadScene("HoaScene-2");
        Time.timeScale = 1f;
    }

    public void OpenScene2() {
        SceneManager.LoadScene("HieuScene 2");
        Time.timeScale = 1f;
    }

    public void OpenScene4() {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }
}
