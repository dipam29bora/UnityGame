using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStaffEffect : StateMachineBehaviour {

    public int effectIndex;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        PlayerController ctrl = animator.GetComponent<PlayerController>();
        
    }
}
