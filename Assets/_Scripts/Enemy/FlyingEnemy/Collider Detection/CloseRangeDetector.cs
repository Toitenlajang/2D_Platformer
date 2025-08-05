using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeDetector : MonoBehaviour
{
    private FlyingEnemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<FlyingEnemy>();

        if (enemy == null)
        {
            Debug.Log("No FlyingEnemy found in parent objects!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.OnCloseRangeEnter();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.OnCloseRangeExit();
        }
    }

}
