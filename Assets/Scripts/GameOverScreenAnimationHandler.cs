using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text gameOverText;

    public void StartGameOverSequence()
    {
        StartCoroutine(GameOverAnimCoroutine());
    }

    private IEnumerator GameOverAnimCoroutine()
    {
        yield return new WaitForSeconds(3f);
        for (float textAlpha = 1; textAlpha > 0; textAlpha -= .05f)
        {
            gameOverText.faceColor = new Color(1, 1, 1, textAlpha);
            yield return new WaitForEndOfFrame();
        }
        gameOverText.enabled = false;
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        EndOfAnimation();
    }

    public void EndOfAnimation()
    {
        image.color = Color.black;
        gameOverText.faceColor = Color.white;
        gameOverText.enabled = true;
        gameObject.SetActive(false);
    }
}

