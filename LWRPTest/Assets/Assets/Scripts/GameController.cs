using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

[Header("References")]
public GameObject player;
public GameObject dam;
public GameObject damHoles;

public GameObject[] twigPrefabs;
public GameObject[] trashPrefabs;

[Header("Properties")]
public float currentStrengthMin;
public float currentStrengthMax;
public float spawnTime;
public float spawnRange;
[Range(0,1)]
public float trashProbability;

[Header("Variables")]
public float currentStrength;
public Vector3 currentSpawnPos;
float timer;
int holeCountTot;
[HideInInspector]
public int holeCountCur;
public float holeBlend;

public delegate void DamBreak();
public static DamBreak OnDamChange;

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
    void Start() 
    {
        foreach (Transform child in damHoles.transform)
            holeCountTot++;
        
        OnDamChange += _OnDamChange;
        _OnDamChange();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime) 
        {
            timer = 0;
            Vector3 damPos = dam.transform.position;
            currentSpawnPos = new Vector3 (damPos.x, damPos.y, damPos.z + Random.Range(-spawnRange, spawnRange));

            if (Random.Range(0f,1f) < trashProbability) 
            {
                int randomIndex = Random.Range(0, trashPrefabs.Length);
                Instantiate(trashPrefabs[randomIndex], currentSpawnPos, trashPrefabs[randomIndex].transform.rotation, gameObject.transform);
            }

            else 
            {
                int randomIndex = Random.Range(0, twigPrefabs.Length);
                GameObject twig = Instantiate(twigPrefabs[randomIndex], currentSpawnPos, twigPrefabs[randomIndex].transform.rotation, gameObject.transform);
                if (player.GetComponent<PlayerController>().pickedUpTwig) 
                    twig.GetComponent<MeshCollider>().isTrigger = false;

                else 
                    twig.GetComponent<MeshCollider>().isTrigger = true;

            }

        }
    }
    void _OnDamChange() 
    {
        holeBlend = (float)holeCountCur/holeCountTot;
        currentStrength = Mathf.Lerp(currentStrengthMin, currentStrengthMax, holeBlend );
    }
}
