﻿using UnityEngine;
using System.Collections.Generic;

public class ControlPanel : MonoBehaviour
{
    private static ControlPanel _instance = null;
    public static ControlPanel Instance { get { return _instance; } }

    public Color Intact;
    public Color Minor;
    public Color Severe;
    public Color Broken;

    public MeshRenderer PitchLight;
    public MeshRenderer RollLeftLight;
    public MeshRenderer RollRightLight;
    public MeshRenderer YawLeftLight;
    public MeshRenderer YawRightLight;
    public MeshRenderer BreathLight;
    public MeshRenderer OilLight;
    public MeshRenderer GasLight;

    public List<GameObject> TurnLeftBreakableParts;
    public List<GameObject> TurnLeftSpawnableParts;

    public List<GameObject> TurnRightBreakableParts;
    public List<GameObject> TurnRightSpawnableParts;

    public List<GameObject> PitchBreakableParts;
    public List<GameObject> PitchSpawnableParts;

    public List<GameObject> RollLeftBreakableParts;
    public List<GameObject> RollLeftSpawnableParts;

    public List<GameObject> RollRightBreakableParts;
    public List<GameObject> RollRightSpawnableParts;

    public List<GameObject> ShootFireBreakableParts;
    public List<GameObject> ShootFireSpawnableParts;

    public List<GameObject> DropOilBreakableParts;
    public List<GameObject> DropOilSpawnableParts;

    public List<GameObject> SpeedThrottleBreakableParts;
    public List<GameObject> SpeedThrottleSpawnableParts;

    private Dictionary<string, List<GameObject>> m_breakableObjects;
    private Dictionary<string, List<GameObject>> m_spawnableObjects;

    // Use this for initialization
    void Start ()
    {
        _instance = this;

        m_breakableObjects = new Dictionary<string, List<GameObject>>();
        m_breakableObjects.Add(BreakingPoints.Turn_Left, TurnLeftBreakableParts);
        m_breakableObjects.Add(BreakingPoints.Turn_Right, TurnRightBreakableParts);
        m_breakableObjects.Add(BreakingPoints.Turn_Up, PitchBreakableParts);
        m_breakableObjects.Add(BreakingPoints.Rotate_Left, RollLeftBreakableParts);
        m_breakableObjects.Add(BreakingPoints.Rotate_Right, RollRightBreakableParts);
        m_breakableObjects.Add(BreakingPoints.Shoot_Fire, ShootFireBreakableParts);
        m_breakableObjects.Add(BreakingPoints.Drop_Oil, DropOilBreakableParts);
        m_breakableObjects.Add(BreakingPoints.Speed_Adjust, SpeedThrottleBreakableParts);

        m_spawnableObjects = new Dictionary<string, List<GameObject>>();
        m_spawnableObjects.Add(BreakingPoints.Turn_Left,    TurnLeftSpawnableParts);
        m_spawnableObjects.Add(BreakingPoints.Turn_Right,   TurnRightSpawnableParts);
        m_spawnableObjects.Add(BreakingPoints.Turn_Up,      PitchSpawnableParts);
        m_spawnableObjects.Add(BreakingPoints.Rotate_Left,  RollLeftSpawnableParts);
        m_spawnableObjects.Add(BreakingPoints.Rotate_Right, RollRightSpawnableParts);
        m_spawnableObjects.Add(BreakingPoints.Shoot_Fire,   ShootFireSpawnableParts);
        m_spawnableObjects.Add(BreakingPoints.Drop_Oil,     DropOilSpawnableParts);
        m_spawnableObjects.Add(BreakingPoints.Speed_Adjust, SpeedThrottleSpawnableParts);
    }

    public void UpdateColorForLight(string _key, BreakTimerManager.Condition _condition)
    {
        BreakDragonApart(_key);

        switch (_key)
        {
            case BreakingPoints.Rotate_Left:
                RollLeftLight.material.color = GetColorForCondition(_condition);
                break;
            case BreakingPoints.Rotate_Right:
                RollRightLight.material.color = GetColorForCondition(_condition);
                break;
            case BreakingPoints.Turn_Left:
                YawLeftLight.material.color = GetColorForCondition(_condition);
                break;
            case BreakingPoints.Turn_Right:
                YawRightLight.material.color = GetColorForCondition(_condition);
                break;
            case BreakingPoints.Turn_Up:
                PitchLight.material.color = GetColorForCondition(_condition);
                break;
            case BreakingPoints.Shoot_Fire:
                BreathLight.material.color = GetColorForCondition(_condition);
                break;
            case BreakingPoints.Drop_Oil:
                OilLight.material.color = GetColorForCondition(_condition);
                break;
            case BreakingPoints.Speed_Adjust:
                GasLight.material.color = GetColorForCondition(_condition);
                break;
        }
    }

    private Color GetColorForCondition(BreakTimerManager.Condition _condition)
    {
        Color color = new Color();

        switch(_condition)
        {
            case BreakTimerManager.Condition.Intact:
                color = Intact;
                break;
            case BreakTimerManager.Condition.Minor:
                color = Minor;
                break;
            case BreakTimerManager.Condition.Severe:
                color = Severe;
                break;
            case BreakTimerManager.Condition.Broke:
                color = Broken;
                break;
        }

        return color;
    }

    private void BreakDragonApart(string _key)
    {
        List<GameObject> _breakables = m_breakableObjects[_key];
        List<GameObject> _spawnables = m_spawnableObjects[_key];

        if (_spawnables.Count == 1)
        {
            Instantiate(_spawnables[0], _breakables[0].transform);
        }
        else
        {
            for(int i = 0; i < _spawnables.Count / 2; i++)
            {
                Instantiate(_spawnables[i], _breakables[i].transform);
            }
        }

        if (_breakables.Count == 1)
        {
            _breakables[0].SetActive(false);
        }
        else
        {
            for(int i = 0; i < _breakables.Count / 2; i++)
            {
                _breakables[i].SetActive(false);
                m_breakableObjects[_key].RemoveAt(i);
            }
        }
    }
}