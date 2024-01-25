using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MainUIHandler : MonoBehaviour
{
    [SerializeField] private Animator inGameMenuAnim;
    private bool isInGameMenuVisible;
    [SerializeField] private TeamUIHandler teamUIHandler;
    [SerializeField] private TMP_Text teamText;

    // Start is called before the first frame update
    void Start()
    {
        isInGameMenuVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            HandleMenuVisibility();
        }
    }

    /// <summary>
    /// Method that handles the menu visibility
    /// </summary>
    void HandleMenuVisibility()
    {
        isInGameMenuVisible = !isInGameMenuVisible;
        inGameMenuAnim.SetBool("MenuVisibility", isInGameMenuVisible);
    }

    public void OnClickTeamButton()
    {
        teamUIHandler.gameObject.SetActive(true);
    }
}
