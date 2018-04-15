using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    private float PlayerSpeed { get; set; }
    private float JumpSpeed { get; set; }
    Animator animator;

    // Animation hash
    int jumpHash = Animator.StringToHash("jump");
    int walkHash = Animator.StringToHash("IsWalking");
    int idleHash = Animator.StringToHash("IsIdle");

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

    private bool _jumpPressed = false;

    // Use this for initialization
    void Start()
    {
        PlayerSpeed = 5f;
        JumpSpeed = 8f;
        animator = GetComponent<Animator>();
    }

    // return 0 if left 1 right 

    // return 0 if left 1 right 
    void CheckMovement()
    {
        int xDirection = ((int)Input.GetAxisRaw("Horizontal") < 0)
            ? -1 : ((int)Input.GetAxisRaw("Horizontal") > 0) ? 1 : 0;
        Vector3 move = new Vector3(xDirection, 0, 0);
        this.transform.position += move * PlayerSpeed * Time.deltaTime;
        if(xDirection != 0)
        {
            animator.SetBool(idleHash, false);
            animator.SetBool(walkHash, true);
        }
        else
        {
            animator.SetBool(idleHash, true);
            animator.SetBool(walkHash, false);
        }
    }

    void CheckJump()
    {
        int yDirection = Input.GetKeyDown("space") ? 1 : 0;
        Vector3 jump = new Vector3(0, yDirection, 0);
        this.GetComponent<Rigidbody>().AddForce(jump * JumpSpeed, ForceMode.Impulse);
        if(yDirection == 1)
        {
            animator.SetTrigger(jumpHash);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        CheckJump();
    }
}