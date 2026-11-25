using System;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float value;

    public float maxValue;

    [Range(0, 1)]
    public float increaseRate;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        value = Mathf.Lerp(value, maxValue, increaseRate * Time.deltaTime);
    }
}
