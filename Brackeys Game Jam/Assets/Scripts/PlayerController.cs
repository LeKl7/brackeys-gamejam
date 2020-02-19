using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public MeshCollider _collider; 

    [Header("Properties")]
    public float movementSpeed;
    float currentStrength;

    [Header("Variables")]
    public Vector3 movement;
    public bool pickedUpTwig;

    void Start() 
    {
        currentStrength = GameController.instance.currentStrength;
    }

    void FixedUpdate() 
    {
        movement = new Vector3 (Input.GetAxisRaw("Vertical") * movementSpeed, 0, -(Input.GetAxisRaw("Horizontal") * movementSpeed));
        rb.AddForce(movement);
    }

    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.CompareTag("Trash")) 
        {
            Debug.Log("You hit trash.");
            rb.AddForce(-2, 0, 0);
        }

        else if (col.gameObject.CompareTag("Twig") && !pickedUpTwig)
        {            
            Debug.Log("You picked up a twig.");

            pickedUpTwig = true;
            Destroy(col.gameObject);
        }
    }
}
