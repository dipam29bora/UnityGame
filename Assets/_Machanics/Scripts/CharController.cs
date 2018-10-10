using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour {

    CharacterController Controller;

    public float ForwardSpeed;
    public float JumpSpeed;

   

    // Use this for initialization
	void Start () {
        Controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Controller.SimpleMove(new Vector3(Horizontal * ForwardSpeed, 0, Vertical * ForwardSpeed));
	}
    
}
