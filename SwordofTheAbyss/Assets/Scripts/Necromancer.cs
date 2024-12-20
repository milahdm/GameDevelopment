//using System.Collections;
//using UnityEngine;

//public class Necromancer : MonoBehaviour
//{
//    public float moveSpeed = 2.0f;

//    private Animator animator;
//    private Transform platformTransform;
//    private SpriteRenderer spriteRenderer;
//    private Rigidbody2D rb;

//    private bool moveRight = true;
//    private bool kill = false;

//    private void Start()
//    {
//        animator = GetComponent <Animator>();
//        rb = GetComponent<Rigidbody2D>();
//        spriteRenderer = GetComponent<SpriteRenderer>();

//        GameObject platform = GameObject.FindGameObjectWithTag("Terrain");

//        if (platform != null)
//        {
//            platformTransform = platform.transform;
//        }
//        else
//        {
//            Debug.LogError("Platform does not exist.");
//        }

//        Invoke("StartWalkingAnimation", 2.0f);
//    }

//    private void Update()
//    {
//        if (platformTransform != null && !kill)
//        {
//            float distanceTomove = moveSpeed * Time.deltaTime;

//            if (moveRight)
//            {
//                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
//            }
//            else
//            {
//                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
//            }

//            animator.SetBool("isMoving", rb.velocity.magnitude > 0);
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Edge"))
//        {
//            moveRight = !moveRight;
//            Flip();
//        }

//        if (collision.gameObject.CompareTag("King"))
//        {
//            animator.SetTrigger("Kill");
//            kill = true;

//            StartCoroutine(ResetKill());
//        }
//    }

//    private void Flip()
//    {
//        spriteRenderer.flipX = !moveRight;
//    }

//    public void Die() 
//    {
//        animator.SetTrigger("Death");

//        Collider2D necromancerCollider = GetComponent<Collider2D>();
//        if (necromancerCollider != null)
//        {
//            necromancerCollider.enabled = false;
//        }

//        // Freeze enemy on death
//        rb.velocity = Vector2.zero;
//        rb.constraints = RigidbodyConstraints2D.FreezeAll;
//    }

//    private void StartWalkingAnimation()
//    {
//        animator.SetTrigger("StartWalk");
//    }

//    private IEnumerator ResetKill()
//    {
//        yield return new WaitForSeconds(2.0f);

//        animator.ResetTrigger("Kill");
//        kill = false;
//    }
//}

using System.Collections;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    private Animator animator;
    private Transform platformTransform;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private bool moveRight = true;
    private bool kill = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject platform = GameObject.FindGameObjectWithTag("Terrain");

        if (platform != null)
        {
            platformTransform = platform.transform;
        }
        else
        {
            Debug.LogError("Platform does not exist.");
        }

        Invoke("StartWalkingAnimation", 2.0f);
    }

    private void Update()
    {
        if (platformTransform != null && !kill)
        {
            float distanceTomove = moveSpeed * Time.deltaTime;

            if (moveRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }

            animator.SetBool("isMoving", rb.velocity.magnitude > 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {
            moveRight = !moveRight;
            Flip();
        }

        if (collision.gameObject.CompareTag("King"))
        {
            animator.SetTrigger("Kill");
            kill = true;

            StartCoroutine(ResetKill());
        }
    }

    private void Flip()
    {
        spriteRenderer.flipX = !moveRight;
    }

    public void Die()
    {
        animator.SetTrigger("Death");

        Collider2D necromancerCollider = GetComponent<Collider2D>();
        if (necromancerCollider != null)
        {
            necromancerCollider.enabled = false;
        }

        // Freeze enemy on death
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void StartWalkingAnimation()
    {
        animator.SetTrigger("StartWalk");
    }

    private IEnumerator ResetKill()
    {
        yield return new WaitForSeconds(2.0f);

        animator.ResetTrigger("Kill");
        kill = false;
    }
}