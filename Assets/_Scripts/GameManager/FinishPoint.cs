using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter called with: " + collision.name);
        if (collision.CompareTag("Player"))
        {
            SceneController.instance.NextScene();
            Debug.Log("Finish Point Reached");
        }
    }
}
