using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private MCAnimatorHandler animator;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movementSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                animator.ChangeMoveAnimationState("x", (int)Input.GetAxisRaw("Horizontal"));
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, obstacles))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                animator.ChangeMoveAnimationState("y", (int)Input.GetAxisRaw("Vertical"));
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, obstacles))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
            else
            {
                animator.ChangeMoveAnimationState("none", 0);
            }
        }

    }
}
