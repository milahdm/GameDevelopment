using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] GameManager manager;
    [SerializeField] GameObject powerUpPrefab; // enemy needs a powerup prefab because it is the prefab spawner



    void Update()
    {
        transform.position -= new Vector3(0, speed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser")) // with a laser the player is rewarded with 10 points
        {
            GameManager.instance.IncreaseScore(10);

            Vector3 enemyPosition = transform.position;

            Destroy(gameObject);
            Destroy(collision.gameObject);

            // this is my condition to drop prefabs from the enemyPlayers position using random.value to drop around 20% of the time
            // spawn star on destruction
            if (Random.value <= 0.2f && powerUpPrefab != null)
            {
                // drop powerup on destruction
                Instantiate(powerUpPrefab, enemyPosition, Quaternion.identity);
            }
        }
    }
}