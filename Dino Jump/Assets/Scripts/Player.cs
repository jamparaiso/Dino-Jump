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
        //pulls the player down if they jump
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)//character controller built-in method
        {
            //makes player stay grounded
            direction = Vector3.down;

            if (Input.GetButton("Jump"))//uses unity input manager
            {
                //makes the player jump if it is grounded
                //player cant double jump as this will not work of the player is not grounded
                direction = Vector3.up * jumpForce;
            }
        }
        //updates the player position
        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if the player collides with a obstacles
        if (other.gameObject.tag == "Obstacles") 
        {
            Debug.Log("Im hit!");
            GameManager.Instance.GameOver();
        }
    }
}
