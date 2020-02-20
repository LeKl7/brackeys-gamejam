using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
[Header("References")]
public Material materialWater;
public Material materialWater_Foam;

[Header("Properties")]
public float speedMin;
public float speedMax;

[Header("Variables")]
public float currentSpeed;
public float desiredSpeed;

    void Start()
    {
        currentSpeed = speedMin;
        materialWater.SetVector("FlowSpeed", new Vector4 (0, currentSpeed, 0, 0));
        materialWater_Foam.SetVector("FlowSpeed", new Vector4 (0, currentSpeed, 0, 0));

        GameController.OnDamChange += OnDamChange;
    }
     
    void OnDamChange() 
    {
        desiredSpeed = Mathf.Lerp(speedMin, speedMax, GameController.instance.holeBlend);
        StartCoroutine(FadeToSpeed(desiredSpeed, materialWater));
        StartCoroutine(FadeToSpeed(desiredSpeed, materialWater_Foam));
    }

    IEnumerator FadeToSpeed(float endSpeed, Material material) 
    {
        float t = 0;
        float startSpeed = currentSpeed;

        while (t <= 2) 
        {
            t += Time.deltaTime;

            currentSpeed = Mathf.Lerp(startSpeed, endSpeed, t/2);
            
            material.SetVector("FlowSpeed", new Vector4 (0, currentSpeed, 0, 0));
            yield return null;
        }

        currentSpeed = endSpeed;
        material.SetVector("FlowSpeed", new Vector4 (0, currentSpeed, 0, 0));
    }
}
