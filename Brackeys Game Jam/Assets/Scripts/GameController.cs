using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

[Header("References")]
public GameObject player;
public GameObject dam;

public GameObject twigPrefab;
public GameObject trashPrefab;

[Header("Properties")]
public float currentStrength;
public float spawnTime;
public float spawnRange;
[Range(0,1)]
public float trashProbability;

[Header("Variables")]
public float time;
public Vector3 currentSpawnPos;

#region Singleton
public static GameController instance;
void Awake() 
{
    if (instance == null) 
        instance = this;
    
    if (instance != this)
        Destroy (gameObject);
}
#endregion

    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnTime) 
        {
            time = 0;
            Debug.Log("Spawning...");
            Vector3 damPos = dam.transform.position;
            currentSpawnPos = new Vector3 (damPos.x, damPos.y, damPos.z + Random.Range(-spawnRange, spawnRange));

            if (Random.Range(0f,1f) < trashProbability)
                Instantiate(trashPrefab, currentSpawnPos, trashPrefab.transform.rotation, gameObject.transform);

            else 
            Instantiate(twigPrefab, currentSpawnPos, twigPrefab.transform.rotation, gameObject.transform);

        }
    }
}
