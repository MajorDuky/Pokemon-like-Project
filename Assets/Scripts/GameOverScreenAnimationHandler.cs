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

    private void OnEnable()
    {
        animator.SetTrigger("FadeOut");
    }

    public void EndOfAnimation()
    {
        Debug.Log("Coucou");
        image.color = Color.black;
        gameOverText.faceColor = Color.white;
        gameObject.SetActive(false);
    }
}

