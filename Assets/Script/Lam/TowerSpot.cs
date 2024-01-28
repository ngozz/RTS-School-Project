using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    [SerializeField]
    private GameObject towerSelectionPrefab;
    private GameObject towerSelectionInstance;
    private bool isClicked = false;

    void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
            towerSelectionInstance = Instantiate(towerSelectionPrefab, transform.position, Quaternion.identity, transform);
        }
    }

    void Update()
    {
        if (isClicked)
        {
            StartCoroutine(CheckForClicksOutside());
        }
    }

    IEnumerator CheckForClicksOutside()
    {
        // Wait for a short moment before checking for the click
        yield return new WaitForSeconds(0.1f);

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool isClickedOnOption = false;
            foreach (Transform option in towerSelectionInstance.transform)
            {
                if (option.GetComponent<Collider2D>().OverlapPoint(mousePos))
                {
                    isClickedOnOption = true;
                    break;
                }
            }
            if (!isClickedOnOption)
            {
                Destroy(towerSelectionInstance);
                isClicked = false;
                Debug.Log("Destroy");
            }
        }
    }

}
