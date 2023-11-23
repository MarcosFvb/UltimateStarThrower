using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    public bool isClimbing = false;
    public bool isThrowing = false;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    public GameObject ninjaStarPrefab; // Prefab of the ninja star
    public float minPower = 0.5f; // Minimum power for throwing
    public float maxPower = 2f; // Maximum power for throwing
    public float powerIncreaseSpeed = 1.5f; // Speed of the power meter increase
    public Gradient powerGradient; // Gradient for the power meter
    public bool isFacingRight = true;

    private bool isCharging = false;
    private float currentPower = 0f;
    private bool powerIncreasing = true;

    void Update()
    {

        if(isClimbing) {
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetInteger("verticalMovement", (int)movement.y);
            handleFacing();
        } else {
            movement.x = Input.GetAxisRaw("Horizontal");
            animator.SetInteger("horizontalMovement", (int)movement.x);
            handleFacing();
        }
        animator.SetFloat("speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            animator.SetBool("isThrowing", true);
            animator.Play("ThrowAnimation", -1, 0); // Start the animation at the first frame
            animator.speed = 0f; // Pause the animation
        }

        if (isCharging)
        {
            HandlePowerMeter();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isCharging = false;
            powerIncreasing = true;
            animator.SetBool("isThrowing", false);
            animator.speed = 1f;

            // Calculate the power based on the position of the power meter
            currentPower = Mathf.Lerp(minPower, maxPower, currentPower);

            // Throw the star with the calculated power
            ThrowStar(currentPower);
            currentPower = 0f;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    void HandlePowerMeter()
    {
        Debug.Log(currentPower);
        if (powerIncreasing)
        {
            currentPower += powerIncreaseSpeed * Time.deltaTime;

            if (currentPower >= maxPower)
            {
                currentPower = maxPower;
                powerIncreasing = false;
            }
        }
        else
        {
            currentPower -= powerIncreaseSpeed * Time.deltaTime;

            if (currentPower <= minPower)
            {
                currentPower = minPower;
                powerIncreasing = true;
            }
        }

    }

    void ThrowStar(float power)
    {
        Vector2 throwPosition = rb.transform.position; // Initial throw position (can be adjusted)

        // Calculate the direction to throw the star based on player's facing direction
        Vector2 throwDirection = transform.right * (transform.localScale.x > 0 ? 1 : -1);

        // Offset the throw position to be in front of the ninja
        throwPosition += throwDirection * 0.5f; // Adjust the offset distance as needed

        GameObject star = Instantiate(ninjaStarPrefab, throwPosition, Quaternion.identity);
        Rigidbody2D starRb = star.GetComponent<Rigidbody2D>();

        // Apply force to the star based on the calculated power and direction
        starRb.velocity = throwDirection * power;
    }

    void handleFacing() {
        if(movement.x < 0 && isFacingRight) {
            flip();
            isFacingRight = false;
        } else if(movement.x > 0 && !isFacingRight) {
            flip();
            isFacingRight = true;
        }
    }

    void flip() {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
