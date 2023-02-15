using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class EnterHall : MonoBehaviour
{
	public Image black;

	public void OnTriggerEnter2D(Collider2D collision)
	{
		// checks if the collision is with a player object
		if (collision.CompareTag("Player"))
		{
			StartCoroutine(EnterTheHall());
		}
	}

	IEnumerator EnterTheHall()
	{
		// reads file
		string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");

		// makes screenc flash black
		black.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.25f);

		StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");

		// writes into file last coordinates
		sw.WriteLine("8.5 -1.5 0");

		// writes new save data into save file
		for (int i = 1; i < saveFile.Length; i++)
		{
			if (i == 3)
				sw.WriteLine("Hall");
			else
				sw.WriteLine(saveFile[i]);
		}

		sw.Close();

		SceneManager.LoadScene("TheHall");
	}
}
