using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    public static PlayerInput s_Instance;

    protected Vector2 m_Movement;
    protected Vector2 m_Camera;
    protected bool m_Jump;
    protected bool m_Attack;
    const float k_AttackInputDuration = 0.03f;
    const float k_DodgeInputDuration = 0.01f;

    protected bool m_Dodge;

    //Attack wait time
    WaitForSeconds m_AttackInputWait;
    Coroutine m_AttackWaitCoroutine;

    //Dodge wait time
    WaitForSeconds m_DodgeInputWait;
    Coroutine m_DodgeWaitCoroutine;

    protected bool playerControllerInputBlocked;
    protected bool m_ExternalInputBlocked;

    public Vector2 MoveInput
    {
        get
        {
            if (playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Movement;
        }
    }

    public Vector2 CameraInput
    {
        get
        {
            if (playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Camera;
        }
    }

    public bool JumpInput
    {
        get
        {
            return m_Jump && !playerControllerInputBlocked && !m_ExternalInputBlocked;
        }
    }

    public bool DodgeInput
    {
        get
        {
            return m_Dodge && !playerControllerInputBlocked && !m_ExternalInputBlocked;
        }
    }

    public bool AttakInput
    {
        get
        {
            return m_Attack && !playerControllerInputBlocked && !m_ExternalInputBlocked;
        }
    }

    void Awake()
    {
        m_AttackInputWait = new WaitForSeconds(k_AttackInputDuration);
        m_DodgeInputWait = new WaitForSeconds(k_DodgeInputDuration);


        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cant be more than one player Input. The instances are " +s_Instance.name+" and" +name +".");
    }

    void Update()
    {
        m_Movement.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        m_Camera.Set(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        m_Jump = Input.GetButtonDown("Jump");


        if (Input.GetButtonDown("LightAttack"))
        {
            if (m_AttackWaitCoroutine != null)
                StopCoroutine(m_AttackWaitCoroutine);

            m_AttackWaitCoroutine = StartCoroutine(AttackWait());
        }

        if(Input.GetButtonDown("Dodge"))
        {
            if (m_DodgeWaitCoroutine != null)
                StopCoroutine(m_DodgeWaitCoroutine);

            m_DodgeWaitCoroutine = StartCoroutine(DodgeWait());
        }
    }

    IEnumerator AttackWait()
    {
        m_Attack = true;
        yield return m_AttackInputWait;
        m_Attack = false;
    }

    IEnumerator DodgeWait()
    {
        m_Dodge = true;
        yield return m_DodgeInputWait;
        m_Dodge = false;
    }

    public bool HaveControl()
    {
        return !m_ExternalInputBlocked;
    }

    public void ReleaseControl()
    {
        m_ExternalInputBlocked = true;
    }

    public void GainControl()
    {
        m_ExternalInputBlocked = false;
    }
}
