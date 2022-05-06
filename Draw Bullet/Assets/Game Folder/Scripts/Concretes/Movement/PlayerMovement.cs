
using UnityEngine;
using PathCreation;
public class PlayerMovement : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;

    private float _distanceTravelled;


    private void Update()
    {
        _distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled);
        // transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled);
    }
}
