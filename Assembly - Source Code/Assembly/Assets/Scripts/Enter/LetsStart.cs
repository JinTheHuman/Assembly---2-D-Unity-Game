using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetsStart : MonoBehaviour
{
    public void StartGame()
    {
        string[] saveFile = System.IO.File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
        
        // checks stage of game
        if (saveFile[1] != "0")
        {
            SceneManager.LoadScene("School");
        }
        else
        {
            SceneManager.LoadScene("Story");
        }
        
    }
}
