using UnityEngine;
using System.Collections.Generic;

public class BreakTimerManager : MonoBehaviour
{

    private static BreakTimerManager _instance = null;
    public static BreakTimerManager Instance { get { return _instance; } }

    
    public float MaxFlightTime = 300.0f;
    public int BreakingAmount = 16;
    public bool UseDefinedBreakTimes = false;
    public List<float> BreakTimes = null;


    [HideInInspector]
    public Dictionary<string, Condition> m_conditions;

    public enum Condition
    {
        Intact,
        Minor,
        Severe,
        Broke
    }

    private float m_timer = 0.0f;

    private float m_savedFuel;

    private string m_lastBreakingPartKey;

    private List<string> BreakingPartsKeys;

    // Debug usage
    private int m_breakIndex = 0;

	// Use this for initialization
	void Start ()
    {
        _instance = this;

        m_savedFuel = DragonValues.Instance.FuelAmount;
        m_lastBreakingPartKey = "none";

        if (!UseDefinedBreakTimes)
            RandomizeBreakTimes();

        if(BreakingPartsKeys == null)
        {
            BreakingPartsKeys = new List<string>();
            BreakingPartsKeys.Add(BreakingPoints.Turn_Left);
            BreakingPartsKeys.Add(BreakingPoints.Turn_Right);
            BreakingPartsKeys.Add(BreakingPoints.Rotate_Left);
            BreakingPartsKeys.Add(BreakingPoints.Rotate_Right);
            BreakingPartsKeys.Add(BreakingPoints.Shoot_Fire);
            BreakingPartsKeys.Add(BreakingPoints.Drop_Oil);
            BreakingPartsKeys.Add(BreakingPoints.Speed_Adjust);
        }
        if(m_conditions == null)
        {
            m_conditions = new Dictionary<string, Condition>();
            m_conditions.Add(BreakingPoints.Turn_Left, Condition.Intact);
            m_conditions.Add(BreakingPoints.Turn_Right, Condition.Intact);
            m_conditions.Add(BreakingPoints.Rotate_Left, Condition.Intact);
            m_conditions.Add(BreakingPoints.Rotate_Right, Condition.Intact);
            m_conditions.Add(BreakingPoints.Shoot_Fire, Condition.Intact);
            m_conditions.Add(BreakingPoints.Drop_Oil, Condition.Intact);
            m_conditions.Add(BreakingPoints.Speed_Adjust, Condition.Intact);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timer += Time.deltaTime;

        DragonValues.Instance.FuelAmount -= Time.deltaTime * DragonValues.Instance.FuelConsumption;

        if(BreakTimes[0] <= m_timer)
        {
            Debug.LogWarning("doh");
            string _randomKey = "";
            if (BreakTimes.Count == 1)
            {
                _randomKey = BreakingPoints.Turn_Up;
            }
            else
            {
                _randomKey = GetRandomBreakingPoint();
            }

            Condition _newCondition = GetNewConditionForKey(_randomKey);

            m_conditions[_randomKey] = _newCondition;

            BreakTimes.RemoveAt(0);
        }
        
	}

    string GetRandomBreakingPoint()
    {
        string _key = "";
        do
        {
            _key = BreakingPartsKeys[Random.Range(0, BreakingPartsKeys.Count)];
        } while (_key == m_lastBreakingPartKey);

        m_lastBreakingPartKey = _key;
        return _key;
    }

    Condition GetNewConditionForKey(string _key)
    {
        Condition _newCondition = Condition.Intact;

        if(!m_conditions.ContainsKey(_key))
        {
            m_conditions.Add(_key, _newCondition);
        }

        switch(_key)
        {
            case BreakingPoints.Turn_Left:
                _newCondition = GetNextStage(m_conditions[_key], true);
                break;
            case BreakingPoints.Turn_Right:
                _newCondition = GetNextStage(m_conditions[_key], true);
                break;
            case BreakingPoints.Turn_Up:
                _newCondition = GetNextStage(m_conditions[_key], false);
                break;
            case BreakingPoints.Rotate_Left:
                _newCondition = GetNextStage(m_conditions[_key], true);
                break;
            case BreakingPoints.Rotate_Right:
                _newCondition = GetNextStage(m_conditions[_key], true);
                break;
            case BreakingPoints.Shoot_Fire:
                _newCondition = GetNextStage(m_conditions[_key], false);
                break;
            case BreakingPoints.Drop_Oil:
                _newCondition = GetNextStage(m_conditions[_key], false);
                break;
            case BreakingPoints.Speed_Adjust:
                _newCondition = GetNextStage(m_conditions[_key], false);
                break;
        }


        return _newCondition;
    }

    Condition GetNextStage(Condition oldCondition, bool hasSteps)
    {
        Condition returnable = oldCondition;

        if(hasSteps)
        {
            returnable++;
        }
        else
        {
            returnable = Condition.Broke;
        }

        return returnable;
    }

    /// <summary>
    /// Resets current timer.
    /// </summary>
    public void ResetTimer()
    {
        m_timer = 0.0f;
    }

    /// <summary>
    /// Resets all conditions.
    /// </summary>
    public void ResetConditions()
    {
        if (m_conditions == null)
            return;

        m_conditions.Clear();
    }

    /// <summary>
    /// Resets timer and conditions and FuelAmount.
    /// </summary>
    public void Reset()
    {
        ResetTimer();
        ResetConditions();

        if (DragonValues.Instance == null)
            return;

        DragonValues.Instance.FuelAmount = m_savedFuel;
    }

    public void RandomizeBreakTimes()
    {
        float _interval = MaxFlightTime / BreakingAmount;

        BreakTimes.Clear();
        for(int i = 0; i < BreakingAmount; i++)
        {
            BreakTimes.Add((_interval*Random.Range(0.95f, 1.05f)) * (i + 1));
        }
    }


    /// <summary>
    /// Debug usage.
    /// </summary>
    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 25), "Timer: " + m_timer.ToString("F2"));
        GUI.Label(new Rect(10, 35, 200, 25), "Last broken part: " + m_lastBreakingPartKey);
        if(m_lastBreakingPartKey != "none")
            GUI.Label(new Rect(10, 60, 200, 25), "New condition: " + m_conditions[m_lastBreakingPartKey]);
    }
}
