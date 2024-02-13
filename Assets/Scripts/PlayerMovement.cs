using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private MCAnimatorHandler animator;
    [SerializeField] private LayerMask exitLayer;
    public Vector3 playerPosBeforeEnteringBuilding;
    public bool isInBattle;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        movePoint.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInBattle)
        {
            HandlePlayerMovement();
        }
    }

    /// <summary>
    /// Base method for handling player movements, including collisions & teleport back to a previous location when going of a building
    /// </summary>
    void HandlePlayerMovement()
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
                TPOutBuilding();
            }
            else
            {
                animator.ChangeMoveAnimationState("none", 0);
            }
        }
    }

    /// <summary>
    /// Method that teleports the move point on the player's position
    /// </summary>
    public void TPMovePoint()
    {
        movePoint.transform.position = transform.position;
    }

    /// <summary>
    /// Method that teleports the player back to the point where he entered a building / area
    /// </summary>
    void TPOutBuilding()
    {
        if (Physics2D.OverlapCircle(movePoint.position, 0.2f, exitLayer))
        {
            transform.position = playerPosBeforeEnteringBuilding;
            TPMovePoint();
            EnvironmentManager.Instance.ResetDefaultTilemaps();
            GameManager.Instance.HandlePNJAndBuildingDisplay();
        }
    }
}
