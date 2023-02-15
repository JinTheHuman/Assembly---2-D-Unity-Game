using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class typeSign : MonoBehaviour
{
    public GameObject diaglogue;
    public Text messageBox;
    public string message;
    public bool playerInRange;
    public Image sign;

    private void Update()
    {
        // checks if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            // activates and deactivates dialogue box and image
            if (diaglogue.activeInHierarchy)
            {
                sign.gameObject.SetActive(false);
                diaglogue.SetActive(false);
            }
            else
            {
                sign.gameObject.SetActive(true);
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
            sign.gameObject.SetActive(false);
        }
    }
}
