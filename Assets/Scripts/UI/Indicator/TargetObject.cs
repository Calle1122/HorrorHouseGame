using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public enum Profile
    {
        Human,
        Ghost
    }

    public Profile thisProfile;
    
    private void Awake()
    {
        IndicatorController indicator = GetComponentInParent<IndicatorController>();
        if(indicator == null)
        {
            indicator = GameObject.Find("IndicatorController").GetComponent<IndicatorController>();
        }

        if (indicator == null) Debug.LogError("No UIController component found");

        indicator.AddTargetIndicator(this.gameObject);
    }
}
