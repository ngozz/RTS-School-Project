using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenScene() {
        SceneManager.LoadScene("DuyScene");
    }

    public void OpenScene1() {
        SceneManager.LoadScene("HoaScene-2");
    }

    public void OpenScene2() {
        SceneManager.LoadScene("HieuScene 2");
    }
}
