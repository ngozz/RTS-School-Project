using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    //[SerializeField] private GameObject upgradeUI;
    //[SerializeField] private Button upgradeButton;

    [SerializeField] private GameObject towerSelectionPrefab;
    private GameObject towerSelectionInstance;
    private bool isClicked = false;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 200f;
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

        RotateTowardTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        } else {
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
        RaycastHit2D[] hits = Physics2D.CircleCastAll(turretRotationPoint.position, targetingRange, (Vector2)turretRotationPoint.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, turretRotationPoint.position) <= targetingRange;
    }

    private void RotateTowardTarget()
    {
        float angle = Mathf.Atan2(target.position.y - turretRotationPoint.position.y, target.position.x - turretRotationPoint.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //public void OpenUpgradeUI()
    //{
    //    upgradeUI.SetActive(true);
    //}

    //public void CloseUpgrade()
    //{
    //    upgradeUI.SetActive(false);
    //}
    private void OnDrawGizmosSelected()
    {
        //Draw 2D circle
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(turretRotationPoint.position, turretRotationPoint.forward, targetingRange);

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
