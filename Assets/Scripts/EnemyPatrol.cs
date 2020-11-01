using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] wayPoints;

    public int damageOnCollision = 20;

    public SpriteRenderer graphics;
    private Transform target;
    private int destinationPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = wayPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destinationPoint = (destinationPoint + 1) % wayPoints.Length;
            target = wayPoints[destinationPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }

}
