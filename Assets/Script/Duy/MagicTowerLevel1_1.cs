using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MagicTowerLevel1_1 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private Transform TowerBase;

    [SerializeField] private GameObject towerSelectionPrefab;
    private GameObject towerSelectionInstance;
    private bool isClicked = false;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float bps = 1f;//bullet per second

    private Transform target;
    private float timeUntilFire;

    void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
            towerSelectionInstance = Instantiate(towerSelectionPrefab, transform.position, Quaternion.identity, transform);
            OnDrawGizmosSelected();
        }
    }

    private void Update()
    {
        if (isClicked)
        {
            StartCoroutine(CheckForClicksOutside());
        }

        if (target == null)
        {
            FindTarget();
            return;
        }

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(TowerBase.position, targetingRange, (Vector2)TowerBase.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, TowerBase.position) <= targetingRange;
    }

    private void OnDrawGizmosSelected()
    {
        //Draw 2D circle
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(TowerBase.position, TowerBase.forward, targetingRange);

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
