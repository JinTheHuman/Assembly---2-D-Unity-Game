using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject diaglogue;
    public Text messageBox;
    public string message;
    public bool playerInRange;

    private void Update()
    {
        // checks if spacebar is pressed while the player is in ranger
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            // activates and deactivates dialogue box
            if (diaglogue.activeInHierarchy)
            {
                diaglogue.SetActive(false);
            }
            else
            {
                diaglogue.SetActive(true);
                messageBox.text = message;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            diaglogue.SetActive(false);
        }
    }
}
