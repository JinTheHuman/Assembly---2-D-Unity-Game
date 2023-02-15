using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class PauseClassScreen : MonoBehaviour
{
    public GameObject ClassScreen;
    public PauseClassMemberUI student1;
    public PauseClassMemberUI student2;
    public PauseClassMemberUI student3;

    public GameObject dialogue;
    public Text message;

    public void SetUp()
    {
        dialogue.SetActive(true);
        message.text = ("Choose  what  to  do  with  team");
        PauseClassMemberUI[] students = { student1, student2, student3 };
        string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");

        string[] theStudents = saveFile[2].Split(' ');

        int size = theStudents.Length / 2;

        int m = 0;
        int i = 0;
        while (i < size)
        {
            students[i].student.gameObject.SetActive(true);
            students[i].Name.text = theStudents[m];
            students[i].image.sprite = Resources.Load<StudentBase>("Students/" + theStudents[m]).Sprite;
            students[i].Level.text = theStudents[m + 1];
            i += 1;
            m += 2;
        }

    }

    // Remove chosen student from player's class
    public void OnKickButton()
    {
        string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
        string[] theStudents = saveFile[2].Split(' ');

        int size = theStudents.Length / 2;

        if (size == 1)
        {
            message.text = "You  can't  kick  your  only  student!";
        }
        else
        {
            StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");
            
            // sets up new line to be written into the save file
            string newline = "";

            for (int i = 0; i < theStudents.Length; i += 2)
            {
                if (i != 0)
                    newline += theStudents[i] + ' ' + theStudents[i + 1] + ' ';
            }

            newline = newline.Remove(newline.Length - 1, 1);

            for (int i = 0; i < saveFile.Length; i++)
            {
                if (i == 2)
                    sw.WriteLine(newline);
                else
                    sw.WriteLine(saveFile[i]);
            }
            sw.Close();
            OnBackButton();
        }
        
    }
    public void OnKickButton1()
    {
        string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
        string[] theStudents = saveFile[2].Split(' ');

        int size = theStudents.Length / 2;

        if (size == 1)
        {
            message.text = "You  can't  kick  your  only  student!";
        }
        else
        {
            StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");
            string newline = "";

            for (int i = 0; i < theStudents.Length; i += 2)
            {
                if (i != 2)
                    newline += theStudents[i] + ' ' + theStudents[i + 1] + ' ';
            }

            newline = newline.Remove(newline.Length - 1, 1);

            for (int i = 0; i < saveFile.Length; i++)
            {
                if (i == 2)
                    sw.WriteLine(newline);
                else
                    sw.WriteLine(saveFile[i]);
            }
            sw.Close();
            OnBackButton();
        }

    }
    public void OnKickButton2()
    {
        string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
        string[] theStudents = saveFile[2].Split(' ');

        int size = theStudents.Length / 2;

        if (size == 1)
        {
            message.text = "You  can't  kick  your  only  student!";
        }
        else
        {
            StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");
            string newline = "";
            for (int i = 0; i < theStudents.Length; i += 2)
            {
                if (i != 4)
                    newline += theStudents[i] + ' ' + theStudents[i + 1] + ' ';
            }

            newline = newline.Remove(newline.Length - 1, 1);

            for (int i = 0; i < saveFile.Length; i++)
            {
                if (i == 2)
                    sw.WriteLine(newline);
                else
                    sw.WriteLine(saveFile[i]);
            }
            sw.Close();
            OnBackButton();
        }
    }
    public void OnMakeFirstButt()
    {
        // First Studuent is already at first position
        message.text = "Student  is  already  first";
    }

    // Switches student to the front position so it enters battle first
    public void OnMakeFirstButt1()
    {
        string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
        string[] theStudents = saveFile[2].Split(' ');

        StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");

        string newline = student2.Name.text + ' ' + student2.Level.text;

        for (int i = 0; i < theStudents.Length; i += 2)
        {
            if (i != 2)
                newline += ' ' + theStudents[i] + ' ' + theStudents[i + 1];
        }

        for (int i = 0; i < saveFile.Length; i++)
        {
            if (i == 2)
                sw.WriteLine(newline);
            else
                sw.WriteLine(saveFile[i]);
        }
        sw.Close();
        OnBackButton();
    }

    public void OnMakeFirstButt2()
    {
        string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
        string[] theStudents = saveFile[2].Split(' ');

        StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");

        string newline = student3.Name.text + ' ' + student3.Level.text;

        for (int i = 0; i < theStudents.Length; i += 2)
        {
            Debug.Log(theStudents[i] + ' ' + theStudents[i + 1]);
            if (i != 4)
                newline += ' ' + theStudents[i] + ' ' + theStudents[i + 1];
        }

        for (int i = 0; i < saveFile.Length; i++)
        {
            if (i == 2)
                sw.WriteLine(newline);
            else
                sw.WriteLine(saveFile[i]);
        }
        sw.Close();
        OnBackButton();
    }

    // exits the student selection screen
    public void OnBackButton()
    {
        student1.gameObject.SetActive(false);
        student2.gameObject.SetActive(false);
        student3.gameObject.SetActive(false);
        dialogue.SetActive(false);
        ClassScreen.SetActive(false);
    }
}
