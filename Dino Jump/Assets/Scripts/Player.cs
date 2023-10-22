using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//watch the tutorial here
//https://www.youtube.com/watch?v=UPvW8kYqxZk&t=997s

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    //custom gravity - downward force
    public float gravity = 9.81f * 2f;

    //player's upward force
    public float jumpForce = 8f;

    private void Awake()
    {
        //maps the object and gets the component
        character = GetComponent<CharacterController>();

    }

    private void OnEnable()
    {
        //reset the player position
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump")) 
            {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }
}
