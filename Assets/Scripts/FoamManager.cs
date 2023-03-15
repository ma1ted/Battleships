using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoamManager : MonoBehaviour
{
    [SerializeField] private Material material;


    private bool foam = true;

    private void Start()
    {
        material.SetFloat("_Foam_Opacity", 1f);
    }

    public void SetFoamOpacity()
    {
        foam = !foam;
        material.SetFloat("_Foam_Opacity", foam ? 1f : 0f);
    }
}
