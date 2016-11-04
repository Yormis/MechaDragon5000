using UnityEngine;
using System.Collections.Generic;

public class BreakTimerManager : MonoBehaviour
{

    private static BreakTimerManager _instance = null;
    public static BreakTimerManager Instance { get { return _instance; } }

    public List<float> BreakTimes = null;
    public List<string> BreakKeys = null;
    public List<Condition> BreakCondition = null;


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

	// Use this for initialization
	void Start ()
    {
        _instance = this;

        m_savedFuel = DragonValues.Instance.FuelAmount;

        m_conditions = new Dictionary<string, Condition>();

        if(BreakKeys != null)
        {
            foreach(string _key in BreakKeys)
            {
                if(!m_conditions.ContainsKey(_key))
                    m_conditions.Add(_key, Condition.Intact);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timer += Time.deltaTime;

        DragonValues.Instance.FuelAmount -= Time.deltaTime * DragonValues.Instance.FuelConsumption;

        for(int i = 0; i < BreakTimes.Count; i++)
        {
            if(BreakTimes[i] >= m_timer)
            {
                m_conditions[BreakKeys[i]] = BreakCondition[i];
            }
        }
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
        m_conditions.Clear();
        if (BreakKeys != null)
        {
            foreach (string _key in BreakKeys)
            {
                if (!m_conditions.ContainsKey(_key))
                    m_conditions.Add(_key, Condition.Intact);
            }
        }
    }

    /// <summary>
    /// Resets timer and conditions and FuelAmount.
    /// </summary>
    public void Reset()
    {
        ResetTimer();
        ResetConditions();

        DragonValues.Instance.FuelAmount = m_savedFuel;
    }


    /// <summary>
    /// Debug usage.
    /// </summary>
    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 25), "Timer: " + m_timer.ToString("F2"));
    }
}
