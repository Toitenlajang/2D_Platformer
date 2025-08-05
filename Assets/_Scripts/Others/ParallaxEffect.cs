using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    //Startig position for the parallax game object
    Vector2 startPosition;

    //Start Z vsalue of the object
    float startZ;

    //the distances the camera has move from the start position of the parallax object
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    //The further the object from the player, the faster the parallaxEffect object will move. Drag its Z value closer to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
    private void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }
    private void Update()
    {
        //When the target moves, move the paralllax object the same distance times a multiplier
        Vector2 newPosition = startPosition + camMoveSinceStart * parallaxFactor;

        //The X,Y position channges based on target travell speedtimes parallax factor, but the Z stats consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
    }
}

