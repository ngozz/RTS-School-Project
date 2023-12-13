using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOption : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private int price;

    void OnMouseDown()
    {
        if (price <= LevelManager.main.currency)
        {
            LevelManager.main.SpendCurrency(price);
            Instantiate(towerPrefab, transform.parent.position, Quaternion.identity);
            Destroy(transform.parent.parent.gameObject);
        }
        else
        {
            Debug.Log("You do not have enough to purchase this item");
            Destroy(transform.parent.gameObject);
        }
    }

}
