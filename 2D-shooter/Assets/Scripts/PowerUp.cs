using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float floatSpeed = 5f;

    private bool isConsumed = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // upadte every frame to make sure the powerup is not consumed by player
        if (!isConsumed)
        {
            Vector3 newPosition = transform.position + Vector3.down * floatSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isConsumed && collision.CompareTag("Player"))
        {


            isConsumed = true;

            // Destroy powerup
            Destroy(gameObject);
        }
    }

}

