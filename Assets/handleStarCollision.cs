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
        if(!collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colidiu com a parede");
            // Get the collision point on the wall
            Vector2 collisionPoint = collision.contacts[0].point;

            // Teleport the ninja to the wall
            TeleportToWall(collisionPoint);
        }
        if(collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<Animator>().SetBool("wasHit", true);
        }

    }

    void TeleportToWall(Vector2 wallCollisionPoint)
    {
        // Set the ninja's position to the collision point on the wall
        //GameObject ninja = GameObject.Find("Ninja");
        ninja.transform.position = wallCollisionPoint;

        bool isClimbing = ninja.GetComponent<Player>().isClimbing;

        if(isClimbing) {
            ninja.GetComponent<Player>().flip();
        }

        ninja.GetComponent<Player>().animator.SetBool("isClimbing", true);
        ninja.GetComponent<Player>().isClimbing = true;
    }
}
