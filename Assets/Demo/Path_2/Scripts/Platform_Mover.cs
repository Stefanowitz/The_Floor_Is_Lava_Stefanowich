using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Mover : MonoBehaviour
{
    public Transform[] points;

    public float moveSpeed = 1f;

    private Transform target;
    private int wayPointIndex = 0;

    public PlayerMoving player;
    Vector3 dir;

    void Start()
    {
        target = points[0];
    }

    void Update()
    {
        dir = target.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextPoint();
        }
        
    }

    void GetNextPoint()
    {
        if (wayPointIndex == 1)
        {
            wayPointIndex--;
        }
        else
        {
            wayPointIndex++;
        }
        target = points[wayPointIndex];
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerMoving>();
            player.controller.Move(dir.normalized * moveSpeed * Time.deltaTime);

            //player.transform.parent = transform;
            //player.gameObject.transform.SetParent(transform);
            //player.onMovingPlatform = true;
            
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player.transform.parent = null;
            //player.gameObject.transform.SetParent(null);
            //player.onMovingPlatform = false;
            player = null;

            Debug.Log("Exit");

        }
    }
}
