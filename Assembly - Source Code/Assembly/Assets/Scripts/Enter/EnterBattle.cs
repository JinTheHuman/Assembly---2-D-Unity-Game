using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterBattle : MonoBehaviour
{
	public Image black;
	public Image dialogue;
	Text message;
	
	public Transform Position;

	// the rooms that face south
	private string[] downs = { "Math", "Science", "Art" };

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// if the collision was a player object
		if (collision.CompareTag("Player"))
		{
			// Checks Art room
			if (name == "Art")
			{
				string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
				int gameStage = System.Convert.ToInt32(saveFile[1]);
				if (gameStage >= 4)
				{
					StartCoroutine(Enter());
				}else
				{
					StartCoroutine(ShowLockedMessage());
				}
			}
			else
			{
				StartCoroutine(Enter());
			}
			
		}
	}

	IEnumerator Enter()
	{
		string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");

		black.gameObject.SetActive(true);

		yield return new WaitForSeconds(0.5f);

		StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");

		// name of the door
		if (downs.Contains(name))
		{
			sw.WriteLine(System.Convert.ToString(Position.position.x) + " " + System.Convert.ToString(Position.position.y - 1) + " " + System.Convert.ToString(Position.position.z));
			for (int i = 1; i < saveFile.Length; i++)
			{
				if (i == 3)
					sw.WriteLine(name);
				else
					sw.WriteLine(saveFile[i]);
			}
		}
		else
		{
			sw.WriteLine(System.Convert.ToString(Position.position.x + 1) + " " + System.Convert.ToString(Position.position.y) + " " + System.Convert.ToString(Position.position.z));
			for (int i = 1; i < saveFile.Length; i++)
			{
				if (i == 3)
					sw.WriteLine(name);
				else
					sw.WriteLine(saveFile[i]);
			}
		}
		sw.Close();

		SceneManager.LoadScene("Battle");
	}
	IEnumerator ShowLockedMessage()
	{
		message = dialogue.GetComponentInChildren<Text>();
		dialogue.gameObject.SetActive(true);
		message.text = "The  door  seems  to  be  locked";
		yield return new WaitForSeconds(1f);
		dialogue.gameObject.SetActive(false);
	}
}
