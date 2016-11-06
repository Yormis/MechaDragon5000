using UnityEngine;

public class DragonInputController : MonoBehaviour
{
    static DragonInputController _instance = null;
    public static DragonInputController Instance { get { return _instance; } }

    private float m_pitch, m_roll, m_yaw;

    private BreakTimerManager m_breakTimeManager = null; // Has to be on scene (singleton).
    private DragonValues m_dragonValues = null; // Has to be on Dragon. (singleton)

	// Use this for initialization
	void Start ()
    {
        _instance = this;

        m_breakTimeManager = BreakTimerManager.Instance;

        m_dragonValues = DragonValues.Instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //float _pitch = GetModifiedPitch();
        //float _roll = GetModifiedRoll();
        //float _yaw = GetModifiedYaw();

        //AdjustSpeed();
        //FartFuel();
        //BurpFire();
        //ResetCamera();
	}

    #region Adjusting sixaxis movements with condition

    private float GetModifiedPitch()
    {
        float _modifiedPitch = Input.GetAxis("Pitch");
        float _multiplier = 1.0f;

        if(m_breakTimeManager == null)
        {
            m_breakTimeManager = BreakTimerManager.Instance;
        }

        if (m_breakTimeManager.m_conditions == null)
        {
            Debug.LogError("Conditions Dictionary is not set!");
            return 0.0f;
        }

        switch (m_breakTimeManager.m_conditions[BreakingPoints.Turn_Up])
        {
            case BreakTimerManager.Condition.Intact:
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

        if (m_breakTimeManager == null)
        {
            m_breakTimeManager = BreakTimerManager.Instance;
        }

        if (m_breakTimeManager.m_conditions == null)
        {
            Debug.LogError("Conditions Dictionary is not set!");
            return 0.0f;
        }

        string _key = "";
        if(_modifiedRoll < 0.0f)
        {
            _key = BreakingPoints.Rotate_Left;
        }
        else
        {
            _key = BreakingPoints.Rotate_Right;
        }

        switch (m_breakTimeManager.m_conditions[_key])
        {
            case BreakTimerManager.Condition.Intact:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Minor:
                _multiplier = 0.5f;
                break;
            case BreakTimerManager.Condition.Severe:
                _multiplier = 0.1f;
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

        if (m_breakTimeManager == null)
        {
            m_breakTimeManager = BreakTimerManager.Instance;
        }

        if (m_breakTimeManager.m_conditions == null)
        {
            Debug.LogError("Conditions Dictionary is not set!");
            return 0.0f;
        }

        string _key = "";
        if (_modifiedYaw < 0.0f)
        {
            _key = BreakingPoints.Turn_Left;
        }
        else
        {
            _key = BreakingPoints.Turn_Right;
        }

        switch (m_breakTimeManager.m_conditions[_key])
        {
            case BreakTimerManager.Condition.Intact:
                _multiplier = 1.0f;
                break;
            case BreakTimerManager.Condition.Minor:
                _multiplier = 0.5f;
                break;
            case BreakTimerManager.Condition.Severe:
                _multiplier = 0.1f;
                break;
            case BreakTimerManager.Condition.Broke:
                _multiplier = 0.0f;
                break;
        }

        return _modifiedYaw * _multiplier;
    }

    #endregion

    #region Public Input Functions

    public float GetPitch()
    {
        return GetModifiedPitch();
    }

    public float GetRoll()
    {
        return GetModifiedRoll();
    }

    public float GetYaw()
    {
        return GetModifiedYaw();
    }

    public float GetGasSpeed()
    {
        if (m_breakTimeManager == null)
        {
            m_breakTimeManager = BreakTimerManager.Instance;
        }

        if (m_breakTimeManager.m_conditions == null)
        {
            Debug.LogError("Conditions Dictionary is not set!");
            return 0.0f;
        }

        if (m_breakTimeManager.m_conditions[BreakingPoints.Speed_Adjust] == BreakTimerManager.Condition.Broke)
        {
            return 1.0f; // returns full possible speed.
        }

        return (Input.GetAxis("Gas") + 1.0f) / 2;
        //return Input.GetAxis("Gas");
    }

    #endregion



    public bool DropFuel()
    {
        if (Input.GetButtonDown("DropOil") || Input.GetKey(KeyCode.F))
        {
            if (m_breakTimeManager == null)
            {
                m_breakTimeManager = BreakTimerManager.Instance;
            }

            if (m_breakTimeManager.m_conditions == null)
            {
                Debug.LogError("Conditions Dictionary is not set!");
                return false;
            }

            if (m_breakTimeManager.m_conditions[BreakingPoints.Drop_Oil] == BreakTimerManager.Condition.Broke)
            {
                Debug.Log("DropOil is broken.");
                return false;
            }

            if(m_dragonValues == null)
            {
                m_dragonValues = DragonValues.Instance;
            }

            m_dragonValues.FuelAmount -= m_dragonValues.FuelFartConsumption;

            return true;
        }

        return false;
    }

    public bool BurpFire()
    {
        if(Input.GetButtonDown("BreathFire") || Input.GetKey(KeyCode.G))
        {
            if (m_breakTimeManager == null)
            {
                m_breakTimeManager = BreakTimerManager.Instance;
            }

            if (m_breakTimeManager.m_conditions == null)
            {
                Debug.LogError("Conditions Dictionary is not set!");
                return false;
            }

            if (m_breakTimeManager.m_conditions[BreakingPoints.Shoot_Fire] == BreakTimerManager.Condition.Broke)
            {
                Debug.Log("Breathing Fire is broken.");
                return false;
            }

            return true;
        }

        return false;
    }

    //public bool ResetCamera()
    //{
    //    if(Input.GetButtonDown("ResetCamera") || Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        return true;
    //    }

    //    return false;
    //}
    
}
