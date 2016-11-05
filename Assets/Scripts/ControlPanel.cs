using UnityEngine;
using System.Collections;

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

	// Use this for initialization
	void Start ()
    {
        _instance = this;
	}

    public void UpdateColorForLight(string _key, BreakTimerManager.Condition _condition)
    {
        switch(_key)
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
}
