using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterBossBattle : MonoBehaviour
{
	public bool playerInRange;

	private void Update()
	{
		if (playerInRange && Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene("Battle");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		playerInRange = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		playerInRange = false;
	}
}
