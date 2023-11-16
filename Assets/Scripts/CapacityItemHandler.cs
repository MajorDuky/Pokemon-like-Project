using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CapacityItemHandler : MonoBehaviour
{
    public TMP_Text capacityName;
    public TMP_Text capacityPowerValue;
    public TMP_Text capacitySPValue;
    public TMP_Text capacityTypeValue;
    public TMP_Text capacityDetails;
    public Button useCapacityButton;
    [SerializeField] private GameObject basicDetails;
    [SerializeField] private GameObject advancedDetails;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleActiveDetails()
    {
        if (basicDetails.activeInHierarchy)
        {
            basicDetails.SetActive(false);
            advancedDetails.SetActive(true);
        }
        else
        {
            basicDetails.SetActive(true);
            advancedDetails.SetActive(false);
        }
    }
}
