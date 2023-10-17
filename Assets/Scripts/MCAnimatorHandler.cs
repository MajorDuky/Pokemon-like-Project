using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int moveState;
    // Start is called before the first frame update
    void Start()
    {
        moveState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Method that handles the character's animation moving state
    /// </summary>
    /// <param name="axis">In which axis the player moves</param>
    /// <param name="direction">In which direction the player moves</param>
    public void ChangeMoveAnimationState(string axis, int direction)
    {
        /* 0 = idle
         * 1 = up
         * 2 = right
         * 3 = down
         * 4 = left
         */
        if (axis == "x" && direction == 1)
        {
            moveState = 2;
            animator.SetInteger("MoveState", 2);
        }
        else if (axis == "x" && direction == -1)
        {
            moveState = 4;
            animator.SetInteger("MoveState", 4);
        }
        else if (axis == "y" && direction == 1)
        {
            moveState = 1;
            animator.SetInteger("MoveState", 1);
        }
        else if (axis == "y" && direction == -1)
        {
            moveState = 3;
            animator.SetInteger("MoveState", 3);
        }
        else
        {
            moveState = 0;
            animator.SetInteger("MoveState", 0);
        }
    }
}
