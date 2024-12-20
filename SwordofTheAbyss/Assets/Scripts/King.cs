//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class King : MonoBehaviour
//{
//    private Animator animator;
//    private Rigidbody2D rb;
//    private SpriteRenderer sprite;
//    private Collider2D kingCollider;

//    private float horizontalInput = 0f;

//    [SerializeField] private float speed = 0f;
//    [SerializeField] private float jumpForce = 0f;

//    private bool isAlive = true;

//    private bool canAttack = false;
//    private bool isAttacking = false;

//    private GameObject attackPoint;
//    private Collider2D attackCollider;


//    private enum MoveState { Idle, Running, Jumping, Falling }

//    void Start()
//    {
//        // King's componenets
//        animator = GetComponent <Animator>();
//        rb = GetComponent <Rigidbody2D>();
//        sprite = GetComponent <SpriteRenderer>();
//        kingCollider = GetComponent<Collider2D>();

//        // attack point, which is it's child
//        attackPoint = transform.Find("AttackPoint").gameObject; // collect position
//        attackCollider = attackPoint.GetComponent<Collider2D>();
//        attackCollider.enabled = false; // Disabled initially
//    }

//    private void Update()
//    {
//        // see if isAttacking from the animator is T/F


//        // if the King is not attacking and alive - for movement inputs
//        if (isAlive)
//        {
//            horizontalInput = Input.GetAxis("Horizontal"); // direction
//            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y); // speed

//            //bool canJump = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Terrain"));

//            if (kingCollider != null) // for issues
//            {
//                bool canJump = kingCollider.IsTouchingLayers(LayerMask.GetMask("Terrain")); // if collider is in contact with anything tagged terrain.

//                // jumping logic
//                if (Input.GetKeyDown(KeyCode.Space) && canJump)
//                {
//                    animator.SetTrigger("Jump"); // triger set to jump
//                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//                }
//            }

//            //if (Input.GetKeyDown(KeyCode.Space) && canJump)
//            //{
//            //    animator.SetTrigger("Jump");
//            //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//            //}


//            // attack input if i want to make it part of this class
//            if (Input.GetKeyDown(KeyCode.X) && canAttack)
//            {
//                Attack();
//            }

//            RunAnimation();
//        }

//        Kill();
//    }

//    //private void Update()
//    //{
//    //    isAttacking = animator.GetBool("isAttacking"); // sets isAttacking T/F based on isAttacking bool in animator

//    //    if (isAlive && !isAttacking) // Check if the character is alive and not attacking
//    //    {
//    //        horizontalInput = Input.GetAxis("Horizontal");
//    //        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

//    //        // Vector2 feetRaycastStart = new Vector2(transform.position.x, transform.position.y - feetOffset);
//    //        // isGrounded = Physics2D.Raycast(feetRaycastStart, Vector2.down, 0.1f, LayerMask.GetMask("Terrain"));

//    //        if (kingCollider != null)
//    //        {
//    //            canJump = kingCollider.IsTouchingLayers(LayerMask.GetMask("Terrain"));
//    //        }

//    //        if (Input.GetKeyDown(KeyCode.Space) && canJump)
//    //        {
//    //            animator.SetTrigger("Jump");
//    //            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//    //        }

//    //        if (Input.GetKeyDown(KeyCode.LeftAlt))
//    //        {
//    //            // Call the Attack method in the CombatSystem
//    //            CombatSystem.Attack(attackPoint);
//    //        }

//    //        RunAnimation();
//    //    }
//    //}

//    // movement animations
//    private void RunAnimation()
//    {
//        MoveState state = MoveState.Idle;


//        // maybe canJumps???
//        if (horizontalInput < 0f)
//        {
//            state = MoveState.Running;
//            sprite.flipX = true;

//            attackPoint.transform.localPosition = new Vector3(-0.5f, 0f, 0f);

//        }
//        else if (horizontalInput > 0f)
//        {
//            state = MoveState.Running;
//            sprite.flipX = false;

//            attackPoint.transform.localPosition = new Vector3(0.5f, 0f, 0f);

//        }
//        else if (rb.velocity.y > 0.1f)
//        {
//            state = MoveState.Jumping;
//            sprite.flipX = false;
//        }
//        else if (rb.velocity.y < -0.1f)
//        {
//            state = MoveState.Falling;
//            sprite.flipX = false;
//        }
//        else
//        {
//            state = MoveState.Idle;
//        }



//        animator.SetInteger("state", (int)state); // set state of ani
//    }

//    // logic for attack point 
//    private void OnTriggerEnter2D(Collider2D other)
//    {


//        //if (attackCollider.enabled) // has to be attacking for enabled
//        //{
//        //    if (other.CompareTag("Necromancer"))
//        //    {
//        //        Necromancer necromancer = other.GetComponent<Necromancer>();

//        //        if (necromancer != null)
//        //        {
//        //            necromancer.Die(); // death method for necromancer
//        //        }
//        //    }
//        //}

//        if (other.gameObject.CompareTag("Necromancer"))
//        {
//             animator.SetTrigger("Death");


//        //    //isAttacking = false;
//        //    //gameObject.layer = LayerMask.NameToLayer("DeadPlayer");


//        //    //Collider2D kingCollider = GetComponent<Collider2D>();
//        //    //if (kingCollider != null)
//        //    // {
//        //    //     kingCollider.enabled = false;
//        //    // }

//            Die();


//            StartCoroutine(DestroyAfterDeathAnimation());


//        }
//    }

//    // freeze on death and set bools and triggers
//    public void Die()
//    {
//        isAlive = false;
//        rb.constraints = RigidbodyConstraints2D.FreezePosition; // no interactions for enemy

//        animator.SetTrigger("Death");

//    }

//    // attacking animation, collider enabling
//    private void Attack()
//    {
//        if (canAttack)
//        {
//            attackCollider.enabled = true;
//            Debug.Log("entering attack");
//            animator.SetBool("Attack", true);
//            Debug.Log("out of attack");
//            canAttack = false;
//            //isAttacking = true;

//            StartCoroutine(ResetAttackCooldownCoroutine());
//        }
//    }

//    private void Kill()
//    {
//        isAttacking = animator.GetBool("Attack");
//        if (isAttacking && attackCollider.enabled)
//        {
//            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Necromancer"));

//            foreach (Collider2D enemyCollider in hitEnemies)
//            {
//                Necromancer necromancer = enemyCollider.GetComponent<Necromancer>();

//                if (necromancer != null)
//                {
//                    necromancer.Die();
//                }
//            }
//        }
//    }



//    //private void Attack()
//    //{
//    //    animator.SetTrigger("Attack");
//    //    canAttack = false; // Prevent multiple attacks in quick succession.

//    //    Collider2D[] hitEnemies = Physics2D.OverlapCircle(attackPoint.transform.position, attackRange, "Necromancer");

//    //    foreach (Collider2D enemyCollider in hitEnemies)
//    //    {
//    //        Necromancer necromancer = enemyCollider.GetComponent<Necromancer>();

//    //        if (necromancer != null)
//    //        {
//    //            necromancer.Die();
//    //        }
//    //    }

//    //    StartCoroutine(ResetAttackCooldown());
//    //}

//    private IEnumerator ResetAttackCooldownCoroutine()
//    {
//        yield return new WaitForSeconds(0.5f);

//        canAttack = true;
//        //isAttacking = false;
//        attackCollider.enabled = false;
//    }


//    // King death delay
//    private IEnumerator DestroyAfterDeathAnimation()
//    {
//        yield return new WaitForSeconds(1.0f);

//        Destroy(gameObject); // destroy king

//    }



//}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class King : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Collider2D kingCollider;
    [SerializeField] private GameManager gameManager;

    private float horizontalInput = 0f;

    [SerializeField] private float speed = 0f;
    [SerializeField] private float jumpForce = 0f;
    //[SerializeField] private float attackCooldownTime = 0.5f;

    private bool isAttacking = false;
    private bool canJump = true;
    private bool isAlive = true;

    private bool canAttack = true;

    private GameObject attackPoint;
    private Collider2D attackCollider;


    private enum MoveState { Idle, Running, Jumping, Falling }

    void Start()
    {
        // King's componenets
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        kingCollider = GetComponent<Collider2D>();

        // attack point, which is it's child
        attackPoint = transform.Find("AttackPoint").gameObject; // collect position
        attackCollider = attackPoint.GetComponent<Collider2D>(); // collect collider 
        attackCollider.enabled = false; // disabled until attack

    }

    private void Update()
    {
        // see if isAttacking from the animator is T/F
        //isAttacking = animator.GetBool("Attack");
        Debug.Log("is attacking: " + isAttacking);
        Debug.Log("can attack: " + canAttack);

        // if the King is not attacking and alive - for movement inputs
        if (isAlive && !isAttacking)
        {
            horizontalInput = Input.GetAxis("Horizontal"); // direction
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y); // speed

            if (kingCollider != null) // for issues
            {
                canJump = kingCollider.IsTouchingLayers(LayerMask.GetMask("Terrain")); // if collider is in contact with anything tagged terrain.

                // jumping logic
                if (Input.GetKeyDown(KeyCode.Space) && canJump)
                {
                    animator.SetTrigger("Jump"); // triger set to jump
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }

            // attack input if i want to make it part of this class
            if (Input.GetKeyDown(KeyCode.X))
            {
                Attack();
            }

            RunAnimation();
        }

        Kill();
    }

    //private void Update()
    //{
    //    isAttacking = animator.GetBool("isAttacking"); // sets isAttacking T/F based on isAttacking bool in animator

    //    if (isAlive && !isAttacking) // Check if the character is alive and not attacking
    //    {
    //        horizontalInput = Input.GetAxis("Horizontal");
    //        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

    //        // Vector2 feetRaycastStart = new Vector2(transform.position.x, transform.position.y - feetOffset);
    //        // isGrounded = Physics2D.Raycast(feetRaycastStart, Vector2.down, 0.1f, LayerMask.GetMask("Terrain"));

    //        if (kingCollider != null)
    //        {
    //            canJump = kingCollider.IsTouchingLayers(LayerMask.GetMask("Terrain"));
    //        }

    //        if (Input.GetKeyDown(KeyCode.Space) && canJump)
    //        {
    //            animator.SetTrigger("Jump");
    //            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //        }

    //        if (Input.GetKeyDown(KeyCode.LeftAlt))
    //        {
    //            // Call the Attack method in the CombatSystem
    //            CombatSystem.Attack(attackPoint);
    //        }

    //        RunAnimation();
    //    }
    //}

    // movement animations
    private void RunAnimation()
    {
        MoveState state = MoveState.Idle;


        // maybe canJumps???
        if (horizontalInput < 0f)
        {
            state = MoveState.Running;
            sprite.flipX = true;

            attackPoint.transform.localPosition = new Vector3(-0.5f, 0f, 0f);

        }
        else if (horizontalInput > 0f)
        {
            state = MoveState.Running;
            sprite.flipX = false;

            attackPoint.transform.localPosition = new Vector3(0.5f, 0f, 0f);

        }
        else if (rb.velocity.y > 0.1f)
        {
            state = MoveState.Jumping;
            sprite.flipX = false;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MoveState.Falling;
            sprite.flipX = false;
        }
        else
        {
            state = MoveState.Idle;
        }



        animator.SetInteger("state", (int)state); // set state of ani
    }

    // logic for attack point 
    private void OnTriggerEnter2D(Collider2D other)
    {


        //if (attackCollider.enabled) // has to be attacking for enabled
        //{
        //    if (other.CompareTag("Necromancer"))
        //    {
        //        Necromancer necromancer = other.GetComponent<Necromancer>();

        //        if (necromancer != null)
        //        {
        //            necromancer.Die(); // death method for necromancer
        //        }
        //    }
        //}

        if (other.gameObject.CompareTag("Necromancer"))
        {
            animator.SetTrigger("Death");
            


            //    //isAttacking = false;
            //    //gameObject.layer = LayerMask.NameToLayer("DeadPlayer");


            //    //Collider2D kingCollider = GetComponent<Collider2D>();
            //    //if (kingCollider != null)
            //    // {
            //    //     kingCollider.enabled = false;
            //    // }

            Die();


            StartCoroutine(DestroyAfterDeathAnimation());


        }
    }

    // freeze on death and set bools and triggers
    public void Die()
    {
        isAlive = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition; // no interactions for enemy

        animator.SetTrigger("Death");

        //gameManager.DisplayGameStatus("Game Over!");

    }



    // attacking animation, collider enabling
    private void Attack()
    {
        if (canAttack)
        {
            Debug.Log("entering attack");
            animator.SetBool("Attack", true);
            Debug.Log("out of attack");
            canAttack = false;
            isAttacking = true;

            ResetAttackCooldownCoroutine();
        }
    }

    private void Kill()
    {
        if (isAttacking && attackCollider.enabled)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Necromancer"));

            foreach (Collider2D enemyCollider in hitEnemies)
            {
                Necromancer necromancer = enemyCollider.GetComponent<Necromancer>();

                if (necromancer != null)
                {
                    necromancer.Die();
                }
            }
        }
    }



    //private void Attack()
    //{
    //    animator.SetTrigger("Attack");
    //    canAttack = false; // Prevent multiple attacks in quick succession.

    //    Collider2D[] hitEnemies = Physics2D.OverlapCircle(attackPoint.transform.position, attackRange, "Necromancer");

    //    foreach (Collider2D enemyCollider in hitEnemies)
    //    {
    //        Necromancer necromancer = enemyCollider.GetComponent<Necromancer>();

    //        if (necromancer != null)
    //        {
    //            necromancer.Die();
    //        }
    //    }

    //    StartCoroutine(ResetAttackCooldown());
    //}

    private IEnumerator ResetAttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        canAttack = true;
        isAttacking = false;
        attackCollider.enabled = false;
        animator.SetBool("Attack", false);
    }


    // King death delay
    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject); // destroy king

    }



}