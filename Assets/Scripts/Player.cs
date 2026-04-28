using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (FindFirstObjectByType<GameManager>().state != GameManager.GameState.Playing)
            return;

        if (Input.GetMouseButton(0))
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (touchPos.x > transform.position.x)
            {
                rb.AddForce(Vector2.right * moveSpeed);
                sr.flipX = false;
            }
            else
            {
                rb.AddForce(Vector2.left * moveSpeed);
                sr.flipX = true;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        float speed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", speed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Crap"))
        {
            rb.linearVelocity = Vector2.zero;

            FindFirstObjectByType<GameManager>().StartQuiz();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("TOUCHED: " + collision.name);

        if (collision.CompareTag("IceCream"))
        {
            FindFirstObjectByType<GameManager>().AddScore(1);
            Destroy(collision.gameObject);
        }
    }
}