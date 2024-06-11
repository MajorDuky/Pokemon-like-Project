using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public enum QuestStatus
    {
        Inactive,
        Pending,
        Active,
        Completed
    }

    protected QuestController questController;
    public UniqueId questId;
    public QuestStatus questStatus;
    public string questTitle;
    public string questDescription;
    public int investigatorXPRewardAmount;

    private void OnEnable()
    {
        QuestController.onQuestActivation.AddListener(QuestActivatedEvent);
        QuestController.onQuestCompletion.AddListener(QuestCompletedEvent);
    }

    void OnDisable()
    {
        QuestController.onQuestActivation.RemoveListener(QuestActivatedEvent);
        QuestController.onQuestCompletion.RemoveListener(QuestCompletedEvent);
    }

    void Start()
    {
        questId = new UniqueId();
        questController = FindObjectOfType<QuestController>();
    }

    private void QuestActivatedEvent(Quest questActivated)
    {
        if (questId == questActivated.questId)
        {
            questStatus = QuestStatus.Active;
            QuestActivation();
        }
    }

    protected abstract void QuestActivation();

    private void QuestCompletedEvent(Quest questCompleted)
    {
        if (questId == questCompleted.questId)
        {
            questStatus = QuestStatus.Completed;
            QuestCompletion();
        }
    }

    protected abstract void QuestCompletion();
}
