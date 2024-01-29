using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void OpenScene5() {
        SceneManager.LoadScene("SelectMap");
    }

    public void Quit() {
        Application.Quit();
    }

    public void OpenScene6() {
        SceneManager.LoadScene("EndMenu");
    }
}
