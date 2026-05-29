using System;
using UnityEngine;

public class DynamicsManager : MonoBehaviour
{
    public static DynamicsManager Instance;

    [Header("Environment Settings")]
    [Tooltip("kg/m^3 (Earth average is 1.225)")]
    public float airDensity = 1.225f; 
    public bool isVacuum = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ToggleVacuum(bool status)
    {
        isVacuum = status;
        Debug.Log($"Physics Mode: {(isVacuum ? "Vacuum" : "Atmosphere")}");
    }

    internal void Initialize()
    {
        throw new NotImplementedException();
    }
}
