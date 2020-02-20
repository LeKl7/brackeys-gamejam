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
public float turningSpeed;

[Header("Variables")]
public Vector2 movement;
public bool hasMovementInput;
public Vector2 oldMovement = new Vector2 (1, 0);
public bool movementChanged;
public bool inMidRotation;
public bool isColliding;
public bool pickedUpTwig;
Coroutine adjustRotationCorout;

    void FixedUpdate() 
    {
        movement = new Vector2 (Input.GetAxisRaw("Vertical"), -(Input.GetAxisRaw("Horizontal")));
        rb.AddForce(new Vector3 (movement.x * movementSpeed, 0,  movement.y * movementSpeed));

        hasMovementInput = (movement.magnitude != 0) ? true : false;
       
        if (!hasMovementInput && adjustRotationCorout != null) 
        {
            inMidRotation = true;
            StopCoroutine(adjustRotationCorout);
        }

        if (hasMovementInput && oldMovement != movement) 
        {
            movementChanged = true;
            oldMovement = movement;
        }
 
        if ((movementChanged && !isColliding ) || (hasMovementInput && inMidRotation)) 
        {
            float newRotation = -Vector2.SignedAngle(Vector2.right, movement);            
            
            if (adjustRotationCorout != null)
                StopCoroutine(adjustRotationCorout);
            adjustRotationCorout = StartCoroutine(AdjustRotation(newRotation, turningSpeed));
            
            movementChanged = false;
        }
    }

    void OnTriggerEnter(Collider col) 
    { 
        if (col.gameObject.CompareTag("Twig") && !pickedUpTwig)
        {            
            Debug.Log("You picked up a twig.");

            pickedUpTwig = true;
            Destroy(col.gameObject);
        }
    }

    void OnCollisionEnter(Collision col) 
    {
        isColliding = true;
        Debug.Log("You hit trash.");
        rb.AddForce(-col.gameObject.GetComponent<InWaterScript>().mass, 0, 0);
    }

    void OnCollisionExit(Collision col) 
    {
        isColliding = false;
        if (hasMovementInput) 
        {
            float newRotation = -Vector2.SignedAngle(Vector2.right, movement);            
            
            if (adjustRotationCorout != null)
                StopCoroutine(adjustRotationCorout);
            adjustRotationCorout = StartCoroutine(AdjustRotation(newRotation, turningSpeed));
        }
    }

     public IEnumerator AdjustRotation (float targetRotation, float speed) 
    {
        Quaternion startRot = transform.rotation;
        Quaternion currentRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler (0, targetRotation, 0);

        while (transform.rotation != targetRot) 
        {
            currentRot = Quaternion.RotateTowards(transform.rotation, targetRot, speed * Time.deltaTime);

            transform.rotation = currentRot;
            yield return null;
        }

        inMidRotation = false;

        transform.rotation =  targetRot;
        if (hasMovementInput)
            rb.angularVelocity = Vector3.zero;
    }
}
