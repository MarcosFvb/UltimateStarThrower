using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 5f;

    private int currentPatrolIndex = 0;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Certifique-se de ter pelo menos dois pontos de patrulha
        if (patrolPoints.Length < 2)
        {
            Debug.LogError("Adicione pelo menos dois pontos de patrulha.");
        }
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        // Move em direção ao ponto de patrulha atual
        Vector2 target = patrolPoints[currentPatrolIndex].position;
        Vector2 currentPosition = transform.position;
        Vector2 moveDirection = (target - currentPosition).normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        // Verifica se o inimigo chegou ao ponto de patrulha atual
        if (Vector2.Distance(currentPosition, target) < 0.1f)
        {
            // Muda para o próximo ponto de patrulha
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
}