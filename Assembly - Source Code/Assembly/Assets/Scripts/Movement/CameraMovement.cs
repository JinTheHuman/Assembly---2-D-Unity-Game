using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // public varibales are imported from Unity
    
    public Transform target; // what gets followed, in our case it is the character
    
    public float smoothing; // how smoothly it follows the object

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position) // if the position of the camera doesn't match the position of the character
        {
            //the variable targetPosition is set to a new vector3 which holds the x y position of the character
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            // move camera to target
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }

    }
}
