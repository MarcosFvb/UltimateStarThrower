using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class handleCollision : MonoBehaviour
{
    GameObject ninja;
    bool gameOver;

    void Start()
    {
        ninja = GameObject.Find("ninja");
    }

    void Update() {
        if (gameOver) {
            if (Input.anyKeyDown) {
                Application.Quit();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 collisionPoint = collision.contacts[0].point;
            TeleportToWall(collisionPoint);
        }
        if(collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<Animator>().SetBool("wasHit", true);
            goToNextLevel();
        }
    }

    void goToNextLevel() {
        if(SceneManager.GetActiveScene().name == "mapa1") {
            SceneManager.LoadScene("mapa2");
        } else if (SceneManager.GetActiveScene().name == "mapa2") {
            SceneManager.LoadScene("mapa3");
        } else {
            GameObject winScreen = GameObject.Find("you_win_screen");
            winScreen.GetComponent<SpriteRenderer>().enabled = true;
            gameOver = true;
            Time.timeScale = 0;
        }
        
    }

    void TeleportToWall(Vector2 wallCollisionPoint)
    {
        ninja.transform.position = wallCollisionPoint;

        bool isClimbing = ninja.GetComponent<Player>().isClimbing;

        if(isClimbing) {
            ninja.GetComponent<Player>().flip();
        }

        ninja.GetComponent<Player>().animator.SetBool("isClimbing", true);
        ninja.GetComponent<Player>().isClimbing = true;
    }
}