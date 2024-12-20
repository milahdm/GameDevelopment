//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CombatSystem : MonoBehaviour
//{
//    //[SerializeField] private GameObject attackPoint; 
//    //[SerializeField] private float attackCooldown = 1.5f;
//    //[SerializeField] private float deathDelay;

//    private Animator kingAnimator;

//    private bool canAttack = true;

//    private GameObject attackPoint;


//    private void Start()
//    {
//        kingAnimator = GetComponent<Animator>();

//        attackPoint = GameObject.FindGameObjectWithTag("AttackPoint"); // find attack point for combat
//    }

//    private void Update()
//    {
//        //check for player input of alt key and spams
//        if (Input.GetKeyDown(KeyCode.X) && canAttack)
//        {
//            kingAnimator.SetBool("Attack", true);
//            canAttack = false; // so no spamming of attack
//        }
//    }



//    // attack takes a game object which is the King's child
//    public void Attack(GameObject attackPoint)
//    {
//        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Enemy"));

//        foreach (Collider2D enemyCollider in hitEnemies)
//        {
//            Necromancer necromancer = enemyCollider.GetComponent<Necromancer>();

//            if (necromancer != null)
//            {
//                necromancer.Die();
//                StartCoroutine(DestroyEnemyWithDelay(enemyCollider.gameObject));
//            }
//        }

//        StartCoroutine(ResetAttackTrigger());
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Necromancer"))
//        {
//            kingAnimator.SetTrigger("Death");


//            //        //isAttacking = false;
//            //        //gameObject.layer = LayerMask.NameToLayer("DeadPlayer");


//            //        //Collider2D kingCollider = GetComponent<Collider2D>();
//            //        //if (kingCollider != null)
//            //        // {
//            //        //     kingCollider.enabled = false;
//            //        // }

//            GetComponent<King>().Die();


//            StartCoroutine(DestroyAfterDeathAnimation());


//        }
//    }

//    private IEnumerator ResetAttackTrigger()
//    {
//        yield return new WaitForSeconds(1.5f);

//        // Reset trigger and allow another attack
//        kingAnimator.SetBool("Attack", false);
//        canAttack = true;
//    }

//    private IEnumerator DestroyAfterDeathAnimation()
//    {
//        yield return new WaitForSeconds(1.0f);

//        Destroy(gameObject); // destroy king

//    }

//    private IEnumerator DestroyEnemyWithDelay(GameObject enemy)
//    {
//        yield return new WaitForSeconds(1.5f);
//        Destroy(enemy); // destroy necromancer
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatSystem : MonoBehaviour
{
    //[SerializeField] private GameObject attackPoint; 
    //[SerializeField] private float attackCooldown = 1.5f;
    //[SerializeField] private float deathDelay;

    private Animator kingAnimator;

    private bool canAttack;

    private GameObject attackPoint;


    private void Start()
    {
        kingAnimator = GetComponent<Animator>();
        canAttack = true;

        attackPoint = GameObject.FindGameObjectWithTag("AttackPoint"); // find attack point for combat


    }

    private void Update()
    {
        //check for player input of alt key and spams
        if (Input.GetKeyDown(KeyCode.LeftAlt) && canAttack)
        {
            kingAnimator.SetTrigger("Attack");
            canAttack = false; // so no spamming of attack
        }
    }



    // attack takes a game object which is the King's child
    public void Attack(GameObject attackPoint)
    {
        // all c
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Necromancer necromancer = enemyCollider.GetComponent<Necromancer>();

            if (necromancer != null)
            {
                // Trigger the enemy's death animation and logic
                necromancer.Die();
                StartCoroutine(DestroyEnemyWithDelay(enemyCollider.gameObject));
            }
        }

        StartCoroutine(ResetAttackTrigger());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Necromancer"))
        {
            kingAnimator.SetTrigger("Death");


            //        //isAttacking = false;
            //        //gameObject.layer = LayerMask.NameToLayer("DeadPlayer");


            //        //Collider2D kingCollider = GetComponent<Collider2D>();
            //        //if (kingCollider != null)
            //        // {
            //        //     kingCollider.enabled = false;
            //        // }

            GetComponent<King>().Die();
            Debug.Log("Death 2!");


            StartCoroutine(DestroyAfterDeathAnimation());







        }
    }



    private IEnumerator ResetAttackTrigger()
    {
        yield return new WaitForSeconds(1.5f);

        // Reset trigger and allow another attack
        kingAnimator.ResetTrigger("Attack");
        canAttack = true;
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return new WaitForSeconds(2.0f);

        Destroy(gameObject); // destroy king
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);


    }

    private IEnumerator DestroyEnemyWithDelay(GameObject enemy)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(enemy); // destroy necromancer
    }
}
