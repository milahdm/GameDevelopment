using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    float yPosition;
    [SerializeField] GameObject PlayerLaser;
    [SerializeField] GameObject shieldPrefab;

    private GameObject shield;

    private bool isShieldActive = false;

    // Start is called before the first frame update
    void Start()
    {
        yPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 convertedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(convertedPosition.x, yPosition, 0);

        if (Input.GetButtonDown("FireLaser"))
        {
            Instantiate(PlayerLaser, transform.position, Quaternion.identity);
        }

        if (shield != null)
        {
            shield.transform.position = transform.position; // to have the shield follow the player if it is active
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {

            Destroy(collision.gameObject); // on collision with a powerup the player will destroy the powerup 

            // conditional to check for status of shield
            if (!isShieldActive)
            {
                ActivateShield();
            }
            else
            {
                DeactivateShield();
            }


        }
        else 
        {
            Destroy(collision.gameObject);
            if (isShieldActive)
            {
                DeactivateShield();
            }
            else 
            {
                Destroy(gameObject);
                GameManager.instance.InitiateGameOver();
            }
        }


    }

    private void ActivateShield()
    {
        isShieldActive = true;


        if (shieldPrefab != null) 
        {
            shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            isShieldActive = true;
        }
    }

    private void DeactivateShield()
    {
        isShieldActive = false;

        if (shield != null) // checks to see if the shield that the player class has access to is on or not, the individual shield of the player
        {
            Destroy(shield);
        }
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }

}