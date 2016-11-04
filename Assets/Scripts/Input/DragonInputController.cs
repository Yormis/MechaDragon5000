using UnityEngine;
using System.Collections.Generic;

public class DragonInputController
{
    static DragonInputController _instance = null;
    public static DragonInputController Instance { get { return _instance; } }

    private float m_pitch, m_roll, m_yaw;

    private BreakTimerManager m_breakTimeManager = null;
    private DragonValues m_dragonValues = null;

	// Use this for initialization
	void Start ()
    {
        _instance = this;

        if (m_breakTimeManager == null)
            m_breakTimeManager = BreakTimerManager.Instance;

        if (m_dragonValues == null)
            m_dragonValues = DragonValues.Instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float _pitch = GetModifiedPitch();
        float _roll = GetModifiedRoll();
        float _yaw = GetModifiedYaw();

        FartFuel();
        BurpFire();
	}

    #region Private PRY Functions

    private float GetModifiedPitch()
    {
        float _modifiedPitch = Input.GetAxis("Pitch");
        float _multiplier = 1.0f;

        switch(m_breakTimeManager.m_conditions["pitch"])
        {
            case BreakTimerManager.Condition.Intact:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Minor:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Severe:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Broke:
                _multiplier = 0.0f;
                break;
        }


        return _modifiedPitch * _multiplier;
    }

    private float GetModifiedRoll()
    {
        float _modifiedRoll = Input.GetAxis("Roll");
        float _multiplier = 1.0f;

        switch (m_breakTimeManager.m_conditions["roll"])
        {
            case BreakTimerManager.Condition.Intact:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Minor:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Severe:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Broke:
                _multiplier = 0.0f;
                break;
        }

        return _modifiedRoll * _multiplier;
    }

    private float GetModifiedYaw()
    {
        float _modifiedYaw = Input.GetAxis("Yaw");
        float _multiplier = 1.0f;

        switch (m_breakTimeManager.m_conditions["yaw"])
        {
            case BreakTimerManager.Condition.Intact:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Minor:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Severe:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Broke:
                _multiplier = 0.0f;
                break;
        }

        return _modifiedYaw * _multiplier;
    }

    #endregion

    #region Public Input Functions

    private void FartFuel()
    {
        
    }

    private void BurpFire()
    {

    }

    #endregion
}
