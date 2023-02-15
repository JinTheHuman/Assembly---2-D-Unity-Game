using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterSchool : MonoBehaviour
{
	public Image black;
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			StartCoroutine(loadScene());
		}
	}

	 IEnumerator loadScene()
	{
		black.color = new Color(0, 0, 0, 255);

		yield return new WaitForSeconds(0.25f);
		SceneManager.LoadScene("School");
	}
}
