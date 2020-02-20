using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{

[Header("References")]
GameObject player;
public GameObject waterfallPrefab;
GameObject waterfall;

[Header("Properties")]
public float breakChance;

[Header("Variables")]
public bool isActive = false;
float time;

    void Start() 
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1) 
        {
            time = 0;

            if (Random.Range(0f, 1f) < breakChance && !isActive) 
            {
                Debug.Log(gameObject.name + " has broken.");
                isActive = true;
                waterfall = Instantiate(waterfallPrefab, gameObject.transform, false);
                GameController.instance.holeCountCur++;
                GameController.OnDamChange();
            }
        } 
    }

    void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.CompareTag("Player") && player.GetComponent<PlayerController>().pickedUpTwig && isActive) 
        {
            Debug.Log("You have repaired " + gameObject.name);
            player.GetComponent<PlayerController>().pickedUpTwig = false;
            isActive = false;
            Destroy(waterfall);
            GameController.instance.holeCountCur--;
        }
    }
}
