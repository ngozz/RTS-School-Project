using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower_Manager : MonoBehaviour
{
    public GameObject buildButton;
    private GameObject selectedBuildPoint;

    /*// Start is called before the first frame update
    void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit != false)
                {
                    selectedBuildPoint = hit.transform.gameObject;
                    if (selectedBuildPoint.tag == "BuildPoint")
                    {
                        buildButton.SetActive(true);
                        buildButton.transform.position = Camera.main.WorldToScreenPoint(selectedBuildPoint.transform.position);
                    }
                }
                else if (buildButton.activeInHierarchy == true)
                {
                    buildButton.SetActive(false);
                }
            }
        }
    }
}