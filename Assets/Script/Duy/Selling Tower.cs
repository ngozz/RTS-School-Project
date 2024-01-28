using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingTower : MonoBehaviour
{
    [SerializeField] private GameObject towerSpot;
    [SerializeField] private int price;
    void OnMouseDown()
    {
        LevelManager.main.currency += price;
        Instantiate(towerSpot, transform.parent.position, Quaternion.identity);
        Destroy(transform.parent.parent.gameObject);
    }
}
