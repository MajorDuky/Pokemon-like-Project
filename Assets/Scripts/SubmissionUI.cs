using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmissionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text submitEvalText;
    [SerializeField] private Animator subButtonAnim;
    public Color32 highProba;
    public Color32 mediumProba;
    public Color32 lowProba;
    public bool isSubButtonAnimOn;
    public float baseXPGain;

    private void OnEnable()
    {
        switch (GameManager.Instance.battleManager.enemyMonster.submissionRate)
        {
            case > 71f:
                submitEvalText.text = "High";
                submitEvalText.color = highProba;
                break;
            case > 31:
                submitEvalText.text = "Medium";
                submitEvalText.color = mediumProba;
                break;
            default:
                submitEvalText.text = "Low";
                submitEvalText.color = lowProba;
                break;
        }
        if(isSubButtonAnimOn)
        {
            subButtonAnim.SetTrigger("TriggerAnim");
        }
        GameManager.Instance.baseXPGain = baseXPGain;
    }

    private void OnDisable()
    {
        GameManager.Instance.baseXPGain = 0f;
    }

    public void SubmitMonsterAttempt()
    {
        GameManager.Instance.battleManager.allyChoice = BattleManager.BattleChoice.Submission;
        GameManager.Instance.battleManager.hasAllyPlayed = true;
    }
}
