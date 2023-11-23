using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleCollision : MonoBehaviour
{
    GameObject ninja;

    void Start()
    {
        ninja = GameObject.Find("ninja");
    }
    
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colidiu com a parede");
            // Get the collision point on the wall
            Vector2 collisionPoint = collision.contacts[0].point;

            // Teleport the ninja to the wall
            TeleportToWall(collisionPoint);
        }
    }

    void TeleportToWall(Vector2 wallCollisionPoint)
    {
        // Set the ninja's position to the collision point on the wall
        //GameObject ninja = GameObject.Find("Ninja");
        ninja.transform.position = wallCollisionPoint;
        ninja.GetComponent<Player>().animator.SetBool("isClimbing", true);
        ninja.GetComponent<Player>().isClimbing = true;
    }
}
