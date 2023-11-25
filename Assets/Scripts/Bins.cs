using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bins : MonoBehaviour
{
    [Header("Amount this bin can hold")]
    [SerializeField]
    private int binCapacity;

    [SerializeField]
    private bool isBinFull;

    void Start()
    {
        isBinFull = false;
    }


    void Update()
    {
        
    }
}
