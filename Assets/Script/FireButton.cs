using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    [SerializeField] private int fire_price;
    [SerializeField] private LevelManager lm;
    public void OnMouseDown()
    {

        Debug.Log("hi");
        lm.SpendCurrency(fire_price);
    }


}
