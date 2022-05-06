
using System;
using UnityEngine;
using PathCreation;
public class PlayerMovement : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;

    private float distanceTravelled;


    private void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
}
