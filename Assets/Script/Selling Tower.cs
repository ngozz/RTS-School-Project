using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingTower : MonoBehaviour
{
    [SerializeField] private int price;
    void OnMouseDown()
    {
        LevelManager.main.currency += price;
        
    }
}
