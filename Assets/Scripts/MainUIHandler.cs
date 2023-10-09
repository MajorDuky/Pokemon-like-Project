using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIHandler : MonoBehaviour
{
    [SerializeField] private Animator inGameMenuAnim;
    private bool isInGameMenuVisible;

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

    void HandleMenuVisibility()
    {
        isInGameMenuVisible = !isInGameMenuVisible;
        inGameMenuAnim.SetBool("MenuVisibility", isInGameMenuVisible);
    }
}
