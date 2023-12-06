using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOption : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;

    void OnMouseDown()
    {
        Instantiate(towerPrefab, transform.parent.position, Quaternion.identity);
        Destroy(transform.parent.parent.gameObject);
    }

}
