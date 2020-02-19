using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{

[Header("References")]
GameObject player;

[Header("Variables")]
public bool isActive;

void Start() 
{
   player = GameObject.FindWithTag("Player");
}
void OnTriggerEnter(Collider collision) 
{
    if(collision.gameObject.CompareTag("Player") && player.GetComponent<PlayerController>().pickedUpTwig) 
    {
        Debug.Log("You have repaired " + gameObject.name);
        player.GetComponent<PlayerController>().pickedUpTwig = false;
        isActive = false;
    }
}


}
