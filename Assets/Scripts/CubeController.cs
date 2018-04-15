using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    int playerNum;

    [SerializeField]
    float leftBounds;

    [SerializeField]
    float rightBounds;

    private float PlayerSpeed { get; set; }
    private float JumpSpeed { get; set; }
    Animator animator;

    // Animation hash
    int jumpHash = Animator.StringToHash("Jump");
    int walkHash = Animator.StringToHash("IsWalking");
    int idleHash = Animator.StringToHash("IsIdle");
    int movingForwardHash = Animator.StringToHash("IsMovingForward");
    int jabHash = Animator.StringToHash("Jab");
    int blockHash = Animator.StringToHash("Block");

    // PlayerID 0 and 1
    private int PlayerID
    {
        get;
        set;
    }
    // Player health
    private int PlayerHealth
    {
        get;
        set;
    }

    public enum PlayerState
    {
        BLOCKING,
        ATTACKING,
        WALKING,
        IDLE,
    }

    private PlayerState pState = PlayerState.IDLE;

    private bool _jumpPressed = false;

    private string playerString; // Used for input axis

    private bool canJump = true; // True if player can jump, reset to true when landing on "Floor"
    private bool hasBegunAction = false; // True once the player begins attack or block animation

    // Use this for initialization
    void Start()
    {
        PlayerSpeed = 5f;
        JumpSpeed = 8f;
        animator = GetComponent<Animator>();

        if (playerNum == 0)
        {
            playerString = "P1";
        }
        else if (playerNum == 1)
        {
            playerString = "P2";
        }
        else
        {
            Debug.LogError("Error: Player " + playerNum.ToString() + " does not exist");
        }
    }

    // return 0 if left 1 right 

    // return 0 if left 1 right 
    void CheckMovement()
    {
        int xDirection = ((int)Input.GetAxisRaw(playerString + "Horizontal") < 0)
            ? -1 : ((int)Input.GetAxisRaw(playerString + "Horizontal") > 0) ? 1 : 0;
        Vector3 move = new Vector3(xDirection, 0, 0);
        transform.position += move * PlayerSpeed * Time.deltaTime;
        if (transform.position.x < leftBounds)
        {
            transform.position = new Vector3(leftBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > rightBounds)
        {
            transform.position = new Vector3(rightBounds, transform.position.y, transform.position.z);
        }
        if(xDirection != 0)
        {
            animator.SetBool(idleHash, false);
            animator.SetBool(walkHash, true);

            if (playerNum == 0)
            {
                if (xDirection > 0)
                {
                    animator.SetBool(movingForwardHash, true);
                }
                else
                {
                    animator.SetBool(movingForwardHash, false);
                }
            }
            else
            {
                if (xDirection < 0)
                {
                    animator.SetBool(movingForwardHash, true);
                }
                else
                {
                    animator.SetBool(movingForwardHash, false);
                }
            }
            
        }
        else
        {
            animator.SetBool(idleHash, true);
            animator.SetBool(walkHash, false);
        }
    }

    void CheckJump()
    {
        if (canJump)
        {
            int yDirection = Input.GetAxisRaw(playerString + "Jump") > 0 ? 1 : 0;
            if (yDirection != 0)
            {
                canJump = false;
                Vector3 jump = new Vector3(0, yDirection, 0);
                GetComponent<Rigidbody>().AddForce(jump * JumpSpeed, ForceMode.Impulse);
                animator.SetTrigger(jumpHash);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        string currentClipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (currentClipName != "SIdle" && currentClipName != "Walk")
        {
            // Sets bool to true to indicated when the animator returns to idle or walk, the player can take an action again
            Debug.Log("Beginning action");
            hasBegunAction = true;
        }
        else if ((currentClipName == "SIdle" || currentClipName == "Walk") && hasBegunAction && pState != PlayerState.IDLE)
        {
            // Return to idle to allow more actions only after action has been completed
            Debug.Log("Reseting action");
            hasBegunAction = false;
            pState = PlayerState.IDLE; // Able to attack or block again
        }
        CheckMovement();
        CheckJump();
        CheckAttack();
        CheckBlock();
    }

    void CheckAttack()
    {
        if (canJump)
        {
            // Cannot attack in the air
            bool isJabbing = Input.GetAxisRaw(playerString + "Jab") > 0 ? true : false;
            if (isJabbing && (pState != PlayerState.ATTACKING) && (pState != PlayerState.BLOCKING))
            {
                Debug.Log("Jabbing animation");
                animator.SetTrigger(jabHash);
                pState = PlayerState.ATTACKING;
            }
        }
    }

    void CheckBlock()
    {
        if (canJump)
        {
            // Cannot block in the air
            bool isBlocking = Input.GetAxisRaw(playerString + "Block") > 0 ? true : false;
            if (isBlocking && (pState != PlayerState.ATTACKING) && (pState != PlayerState.BLOCKING))
            {
                Debug.Log("Blocking animation");
                animator.SetTrigger(blockHash);
                pState = PlayerState.BLOCKING;
            }
        }
    }

    // Reset jump if hit the floor
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }
}