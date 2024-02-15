using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DangerGauge : MonoBehaviour
{
    [SerializeField] private TMP_Text areaNameAndLevel;
    [SerializeField] private TMP_Text probabilityCalcRateValue;
    [SerializeField] private TMP_Text realTimeTimerValue;
    [SerializeField] private Slider timerDangerGauge;
    [SerializeField] private Image dangerIndicatorIcon;
    private bool hasTimerStarted = false;
    private float baseTimerValue;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasTimerStarted)
        {
            if (timer > 0)
            {
                timerDangerGauge.value = timer / baseTimerValue;
                realTimeTimerValue.text = Math.Round(timer, 2).ToString();
                timer -= Time.deltaTime;
            }
            else
            {
                timer = baseTimerValue;
            }
        }
    }

    public void InitializeDangerGauge(string areaName, int areaLvl, float probaCalcRateValue)
    {
        areaNameAndLevel.text = $"{areaName} Lvl.{areaLvl.ToString()}";
        probabilityCalcRateValue.text = probaCalcRateValue.ToString();
        timer = probaCalcRateValue;
        baseTimerValue = timer;
        GameManager.Instance.DetermineStrongestMonsterLvlInPlayerTeam();
        int deltaLvl = areaLvl - GameManager.Instance.strongestMonsterLvl;
        switch (deltaLvl)
        {
            case >= 10:
                dangerIndicatorIcon.color = new Color32(183, 101, 241, 255);
                break;
            case >= 5:
                dangerIndicatorIcon.color = new Color32(241, 101, 101, 255);
                break;
            case >= 0:
                dangerIndicatorIcon.color = new Color32(241, 183, 102, 255);
                break;
            case < 0:
                dangerIndicatorIcon.color = new Color32(156, 241, 101, 255);
                break;
        }
        hasTimerStarted = true;
    }

    public void StopDangerGauge()
    {
        hasTimerStarted = false;
        gameObject.SetActive(false);
    }
}
