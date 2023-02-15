using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClassOfStudents : MonoBehaviour
{
    public List<Student> students;
    public bool isMyClass;
    public bool isBoss;

    private void Start()
    {
        // reads save file
        string[] saveFile = System.IO.File.ReadAllLines(@"..\\Assembly\\Assets\\Scripts\\save.txt");

        if (isMyClass)
        {
            // stores relevant line holding the data of the students the player owns
            string[] theStudents = saveFile[2].Split(' ');
            
            // adds new student to the students list for each student stored in the save file
            int i = 0;
            while (i < theStudents.Length)
            {
                students.Add(new Student(Resources.Load<StudentBase>("Students/" + theStudents[i]), System.Convert.ToInt32(theStudents[i + 1])));
                i += 2;
            }
        }
        else
        {
            // cheacks if it is the boss battle as boss students have fixed levels
            if (isBoss)
            {
                foreach(var student in students)
                {
                    student.Init();
                }
            }
            else
            {
                // for each student of the enemy team it assigns a random level depending on the stage of the game
                foreach (var student in students)
                {
                    System.Random rnd = new System.Random();

                    // gets game stage from save file
                    int gameStage = System.Convert.ToInt32(saveFile[1]);

                    // calculates ranges of random number with the gameStage
                    int minStdLvl = 2 + 14 * gameStage;
                    int maxStdLvl = minStdLvl + 14;

                    // assigns the random level
                    student.level = rnd.Next(minStdLvl, maxStdLvl);

                    // initialises the student
                    student.Init();
                }
            }  
        }
    }
    public Student GetAliveStudent()
    {
        return students.Where(x => x.CurrentHP > 0).FirstOrDefault();
    }
}
