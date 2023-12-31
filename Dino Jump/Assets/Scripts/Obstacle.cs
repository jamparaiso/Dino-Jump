using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftEdge;

    private void Start()
    {
        //
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 4f;
    }

    private void Update()
    {
        //moves the obstacle relevant to the game speed
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        //destroy the obstacle after it leaves the screen
        if(transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
