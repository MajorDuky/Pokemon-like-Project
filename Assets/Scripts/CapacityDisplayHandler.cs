using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapacityDisplayHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text capacityName;
    [SerializeField] private TMP_Text capacityDesc;
    [SerializeField] private TMP_Text capacityType;
    [SerializeField] private TMP_Text capacitySp;
    [SerializeField] private TMP_Text capacityAccuracy;
    [SerializeField] private TMP_Text capacityStrength;

    public void UpdateCapacityName(string name)
    {
        capacityName.text = name;
    }

    public void UpdateCapacityDesc(string desc)
    {
        capacityDesc.text = desc;
    }

    public void UpdateCapacityType(string type)
    {
        capacityType.text = type;
    }

    public void UpdateCapacitySp(float sp)
    {
        capacitySp.text = sp.ToString();
    }

    public void UpdateCapacityAccuracy(float accuracy)
    {
        capacityAccuracy.text = accuracy.ToString();
    }

    public void UpdateCapacityStrength(float strength)
    {
        capacityStrength.text = strength.ToString();
    }
}
