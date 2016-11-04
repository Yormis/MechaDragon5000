using UnityEngine;
using System.Collections;

public class DragonValues : MonoBehaviour
{
    private static DragonValues _instance = null;
    public static DragonValues Instance { get { return _instance; } }

    [Range(0.0f, 100.0f)]
    public float FuelAmount;

    [Range(0.0f, 1.0f)]
    public float FuelConsumption;

    void Start()
    {
        _instance = this;
    }
}
