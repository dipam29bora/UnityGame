using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    protected PlayerInput m_Input;
    public float MaxWalkSpeed=3;
    public float JumpForce=10;
    public float GroundCheckDistance = 1;
    public Transform CheckGroundTransform;
    public LayerMask WhatIsGround;

    bool JumpPressed;

    public bool m_canAttack;

    protected AnimationControls m_AnimControls;


    protected bool IsMoveInput
    {
        get { return !Mathf.Approximately(m_Input.MoveInput.sqrMagnitude, 0f); }
    }

    void Awake()
    {
        //m_Input = PlayerInput.Instance;
        m_Input = GetComponent<PlayerInput>();
        m_AnimControls = GetComponent<AnimationControls>();
    }

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    
    // Update is called once per frame
    void FixedUpdate () {
        
        rb.velocity = new Vector3(MaxWalkSpeed * m_Input.MoveInput.x, rb.velocity.y, MaxWalkSpeed * m_Input.MoveInput.y);
        
        if(m_Input.JumpInput && isGrounded())
        {
            rb.velocity = new Vector3(0, JumpForce, 0);
            m_AnimControls.SetJumpTrigger();
           // m_AnimControls.SetJumpingInt(0);
        }

        if(m_Input.AttakInput && m_canAttack)
        {
            m_AnimControls.SetAttackTrigger();
        }

        ControlledRotation(m_Input.MoveInput);

        if (isGrounded())
            m_AnimControls.SetJumpingInt(0);
        else
            m_AnimControls.SetJumpingInt(2);

        //Is falling
        if (rb.velocity.y > Mathf.Abs(2.0f))
        {
            m_AnimControls.SetJumpingInt(2);
        }
	}

    public float JumpCheckDistnace = 10.0f;
   
    public bool isGrounded() 
    {
        RaycastHit hit;
        return (Physics.Raycast(CheckGroundTransform.position, Vector3.down, out hit, GroundCheckDistance));
        
    }

    void ControlledRotation(Vector2 MoveAxis)
    {
        Vector3 tardir = new Vector3(MoveAxis.x, 0, MoveAxis.y);
        if(tardir!=Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(tardir), Time.deltaTime * 5.5f);

    }

    public void AttackStart()
    {
        print("Attack Started");
        // = true;
    }

    public void AttackEnd()
    {
        print("Attack Ended");
    }
}
