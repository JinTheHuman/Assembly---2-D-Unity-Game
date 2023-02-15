using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;
    public Transform Position;
    public GameObject checkTeamScreen;
    public PauseClassScreen TeamScreen;

    // Update is called once per frame
    void Update()
    {
        // Checks for right shift key press
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (pauseScreen.activeInHierarchy)
            {
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    // Writes position into save file and exits
    public void OnSaveClick()
    {
        string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
        StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");

        sw.WriteLine(System.Convert.ToString(Position.position.x) + " " + System.Convert.ToString(Position.position.y) + " " + System.Convert.ToString(Position.position.z));
        for (int i = 1; i < saveFile.Length; i++)
        {
                sw.WriteLine(saveFile[i]);
        }
        sw.Close();
        Application.Quit();
    }

    // Resets save file and loads title screen
    public void OnNewGameClick()
    {
        StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");

        sw.WriteLine("0.5 -12.5 0");
        sw.WriteLine("0");
        sw.WriteLine("Adam 10");
        sw.WriteLine("start");
        sw.WriteLine("Start");

        sw.Close();

        Time.timeScale = 1;

        SceneManager.LoadScene("Title");
    }

    public void OnCheckTeamClick()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        checkTeamScreen.SetActive(true);
        TeamScreen.SetUp();
    }
}
