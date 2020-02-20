using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWaterScript : MonoBehaviour
{
[Header("References")]
public MeshCollider col;
public Rigidbody rb;

[Header("Properties")]
public float mass;

[Header("Variables")]
public float currentStrength;

    void FixedUpdate() 
    {
        //Apply the force of the current!
        rb.AddForce (new Vector3(-GameController.instance.currentStrength, 0, 0));
    }
}
