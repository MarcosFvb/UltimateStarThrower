using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleCollision : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                // Get the collision point on the wall
                Vector2 collisionPoint = collision.contacts[0].point;

                // Teleport the ninja to the wall
                TeleportToWall(collisionPoint);
            }
        }

        void TeleportToWall(Vector2 wallCollisionPoint)
        {
            // Set the ninja's position to the collision point on the wall
            ninja.transform.position = wallCollisionPoint;
            player.isClimbing = true;
        }
    }
}
