using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinRangeDetector : MonoBehaviour
{
    private FlyingEnemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<FlyingEnemy>();

        if (enemy == null)
        {
            Debug.LogError("[CloseRangeDetector] No FlyingEnemy found in parent objects!");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.OnMinRangeEnter();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.OnMinRangeExit();
        }
    }
}
