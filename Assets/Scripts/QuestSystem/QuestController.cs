using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestController : MonoBehaviour
{
    public static QuestActivatedEvent onQuestActivation = new QuestActivatedEvent();
    public static QuestCompletedEvent onQuestCompletion = new QuestCompletedEvent();

    public void QuestActivated(Quest questActivated)
    {
        onQuestActivation.Invoke(questActivated);
    }

    public void QuestCompleted(Quest questCompleted)
    {
        onQuestCompletion.Invoke(questCompleted);
    }
}

public class QuestActivatedEvent : UnityEvent<Quest>
{

}

public class QuestCompletedEvent : UnityEvent<Quest>
{

}
