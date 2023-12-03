using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behaviour : MonoBehaviour
{
    private int currentWayPoint;
    private GameObject[] wayPoints;
    public float speed;

    private Vector2 target;


    // Start is called before the first frame update
    void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        target = wayPoints[currentWayPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WayPoint")
        {
            if (currentWayPoint >= wayPoints.Length - 1)
            {
                Destroy(gameObject);
            }
            else
            {
                currentWayPoint++;
                target = wayPoints[currentWayPoint].transform.position;
            }
        }
    }
}
