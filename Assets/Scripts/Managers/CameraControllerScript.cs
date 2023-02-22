using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public Vector3 offset = Vector3.up; // Camera offset away from the targer
    public Transform target; // The player target

    public float[] bordersX = new float[2];
    public float[] bordersZ = new float[2];
    
    void Update()
    {
        Vector3 targetPosition = target.position;
        //Clamp: Limits the min (Index[0]) and the max (Index[1]) values for not getting outside of the borders
        float posX = Mathf.Clamp(targetPosition.x, bordersX[0], bordersX[1]); 
        float posZ = Mathf.Clamp(targetPosition.z, bordersZ[0], bordersZ[1]);
        //Lerp: Smooth the movement from point A to point B within a time delay.
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, 0, posZ) + offset, cameraSpeed * Time.deltaTime);
    }
}
