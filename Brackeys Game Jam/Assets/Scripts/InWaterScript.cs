using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWaterScript : MonoBehaviour
{
[Header("References")]
public MeshCollider col;
public Rigidbody rb;

[Header("Variables")]
float currentStrength;
public float initialZRotation;
public bool adjustRotationRunning;
public bool isColliding;

    void Start()
    {
        //Set current strenght
        currentStrength = GameController.instance.currentStrength;
        initialZRotation = transform.rotation.eulerAngles.z;
    }

    void FixedUpdate() 
    {
        //Apply the force of the current!
        rb.AddForce (new Vector3(-currentStrength, 0, 0));

        if (transform.rotation.eulerAngles.z != initialZRotation && !adjustRotationRunning && !isColliding) 
        {
            StartCoroutine(AdjustRotation(initialZRotation));
        }
    }

    void OnCollisionEnter() 
    {
        isColliding = true;
    }
    void OnCollisionExit() 
    {
        isColliding = false;
    }
    public IEnumerator AdjustRotation(float targetZRotation) 
    {
        adjustRotationRunning = true;
        Debug.Log("AdjustRot of " + gameObject.name);

        float t = 0;
        Quaternion startRot = transform.rotation;
        Quaternion currentRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler (0, 0, targetZRotation);
        while (t <= 4) 
        {
            t += Time.deltaTime;

            currentRot = Quaternion.Lerp(startRot, targetRot, t/4);
            transform.rotation = currentRot;
            yield return null;
        }
        adjustRotationRunning = false;
        transform.rotation =  targetRot;
        rb.angularVelocity = Vector3.zero;
    }
}
