using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIHandler : MonoBehaviour
{
    [SerializeField] private Slider allyHPSlider;
    [SerializeField] private TMP_Text allyHPText;
    [SerializeField] private Slider enemyHPSlider;
    [SerializeField] private TMP_Text enemyHPText;
    [SerializeField] private Slider allySPSlider;
    [SerializeField] private TMP_Text allySPText;
    [SerializeField] private Slider enemySPSlider;
    [SerializeField] private TMP_Text enemySPText;
    [SerializeField] private Slider allyXPSlider;
    [SerializeField] private Image allySprite;
    [SerializeField] private Image enemySprite;
    [SerializeField] private TMP_Text battleText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAllyHP(float newAmount, float maxHP)
    {
        allyHPText.text = $"{newAmount.ToString()} / {maxHP.ToString()}";
        allyHPSlider.value = maxHP / newAmount;
    }

    public void UpdateEnemyHP(float newAmount, float maxHP)
    {
        enemyHPText.text = $"{newAmount.ToString()} / {maxHP.ToString()}";
        enemyHPSlider.value = maxHP / newAmount;
    }

    public void UpdateAllySP(float newAmount, float maxSP)
    {
        allySPText.text = $"{newAmount.ToString()} / {maxSP.ToString()}";
        allySPSlider.value = maxSP / newAmount;
    }

    public void UpdateEnemySP(float newAmount, float maxSP)
    {
        enemySPText.text = $"{newAmount.ToString()} / {maxSP.ToString()}";
        enemySPSlider.value = maxSP / newAmount;
    }

    public void UpdateAllyXP(float newAmount, float maxXP)
    {
        allyXPSlider.value = maxXP / newAmount;
    }
}
