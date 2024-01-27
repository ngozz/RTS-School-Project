using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonSupport : MonoBehaviour
{
    [SerializeField] private GameObject fire;

    private void Start()
    {
        ActivateFire(true);
    }

    public void ActivateFire(bool state)
    {
        fire.SetActive(!state);
    }
}