using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControls : MonoBehaviour {

    public Animator CharacterAnimator;

    //Animation States
    readonly int m_HashMeleeAttack = Animator.StringToHash("MeleeAttack");
    readonly int m_HashStateTime = Animator.StringToHash("StateTime");
    readonly int m_HashJump = Animator.StringToHash("JumpTrigger");
    readonly int m_HashJumping = Animator.StringToHash("jumping");
    readonly int m_Move = Animator.StringToHash("Move");

    Rigidbody _rb;
    PlayerController _playerController;
    float MaxWalkSpeed;

	void Start () {
        _rb = GetComponent<Rigidbody>();
        _playerController = GetComponent<PlayerController>();
        MaxWalkSpeed = _playerController.MaxWalkSpeed;
	}
	
	
	void Update () {
        float speed = _rb.velocity.magnitude / MaxWalkSpeed;
        CharacterAnimator.SetFloat("Speed", speed);
        CharacterAnimator.SetBool("IsInAir", !_playerController.isGrounded());
        CharacterAnimator.SetFloat(m_HashStateTime, Mathf.Repeat(CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
        CharacterAnimator.SetBool("Dodge", PlayerInput.Instance.DodgeInput);

        
        CharacterAnimator.SetBool(m_Move, (PlayerInput.Instance.MoveInput.magnitude > 0.2f));
        
	}

    void LateUpdate()
    {
        
    }

    public void SetAttackTrigger()
    {
        CharacterAnimator.SetTrigger(m_HashMeleeAttack);
    }

    public void SetJumpTrigger()
    {
        CharacterAnimator.SetTrigger(m_HashJump);
    }

    public void SetJumpingInt(int x)
    {
        CharacterAnimator.SetInteger(m_HashJumping, x);
    }
}
