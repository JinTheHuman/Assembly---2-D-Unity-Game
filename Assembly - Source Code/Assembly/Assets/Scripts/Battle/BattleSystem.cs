using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public enum BattleState { START, MYTURN, ENEMYTURN, WON, LOST, CLASSSCREEN }

public class BattleSystem : MonoBehaviour
{
    public Text dialogueText;

    public BattleHUD myHUD;
    public BattleHUD enemyHUD;

    public GameObject attackBox;

    public BattleState state;

    public BattleUnit myStudent;
    public BattleUnit enemyStudent;

    ClassOfStudents myClass;
    ClassOfStudents enemyClass;

    public GameObject Player;
    GameObject Enemy;

    public TeacherMovement myTeacher;
    public TeacherMovement enemyTeacher;

    public EnemyTeacher enemyTeacherData;

    public ClassScreen classScreen;

    // Start is called before the first frame update
    private void Start()
    {
        string[] saveFile = File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
        string subject = saveFile[3];

        // sets up enemy object
        Enemy = GameObject.Find(subject);
        enemyTeacher = Enemy.GetComponent<TeacherMovement>();
        enemyTeacherData = Enemy.GetComponent<EnemyTeacher>();
        enemyClass = Enemy.GetComponent<ClassOfStudents>();

        // gets player team
        myClass = Player.GetComponent<ClassOfStudents>();
        
        if (enemyTeacherData.subjectName == "Boss")
            StartCoroutine(BossIntro());
        else
            StartCoroutine(TeacherIntro());
        
    }
    public void StartBattle(ClassOfStudents _myClass, ClassOfStudents _enemyClass)
    {

        myStudent.SetUp(_myClass.GetAliveStudent());
        enemyStudent.SetUp(_enemyClass.GetAliveStudent());

        classScreen.Init();

        state = BattleState.START;
        StartCoroutine(SetUpBattle());

    }
    IEnumerator SetUpBattle()
    {
        myHUD.SetHUD(myStudent);
        enemyHUD.SetHUD(enemyStudent);

        // Boss dialogue and normal dialogue
        if (enemyTeacherData.subjectName == "Boss")
        {
            dialogueText.text = "I  send  in  Kyle!!  The  English  Ace !!";
            yield return new WaitForSeconds(2f);

            dialogueText.text = "English  is  the  most  powerful  and  useful  subject  of  them  all!!";
            yield return new WaitForSeconds(3f);

            dialogueText.text = "You  will  never  beat  him !!";
            yield return new WaitForSeconds(2f);
        }
        else
        {
            dialogueText.text = "GO " + myStudent.Student.Base.Name + " !!";
        }

        yield return new WaitForSeconds(2f);

        state = BattleState.MYTURN;
        MyTurn();
    }

    IEnumerator MyAttack()
    {
        // remove attack selection box
        attackBox.SetActive(false);

        dialogueText.text = myStudent.Student.Base.Name + "    Attacks";
        yield return new WaitForSeconds(1f);

        // calculate damage
        var damageDetails = enemyStudent.TakeDamage(myStudent.Student.Damage, myStudent.Student.Base.Type);

        //play attack animations
        myStudent.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);
        enemyStudent.PlayGetHitAnimation();
        yield return new WaitForSeconds(0.5f);

        // update hp bar
        enemyHUD.SetHP(enemyStudent.Student.CurrentHP);
        yield return new WaitForSeconds(1f);

        // Show attack message
        StartCoroutine(ShowDamageDetails(damageDetails));
        yield return new WaitForSeconds(2f);

        // check if dead
        if (damageDetails.Fainted)
        {
            enemyStudent.PlayDieAniamtion();
            yield return new WaitForSeconds(1f);

            var nextStudent = enemyClass.GetAliveStudent();
            
            // if there is a living student left in the team
            // brings out the next student and sets them up
            if (nextStudent != null)
            {
                enemyStudent.SetUp(nextStudent);

                state = BattleState.START;

                enemyHUD.SetHUD(enemyStudent);

                // Plays Boss dialogue
                if (enemyTeacherData.subjectName == "Boss")
                {
                    if (enemyStudent.Student.Base.Name == "Jayden")
                    {
                        dialogueText.text = "You may have beaten Kyle. But you'll never beat the Math Ace";
                        yield return new WaitForSeconds(3f);
                    }
                    else if (enemyStudent.Student.Base.Name == "Harris")
                    {
                        dialogueText.text = "Fine you give me no other choice";
                        yield return new WaitForSeconds(3f);

                        dialogueText.text = "The student I send is good at many subjects";
                        yield return new WaitForSeconds(3f);

                        dialogueText.text = "Math ,  Sport ,  English ,  Science";
                        yield return new WaitForSeconds(3f);

                        dialogueText.text = "He  is  the  All  Rounder!!";
                        yield return new WaitForSeconds(3f);
                    }
                }

                dialogueText.text = "Go  " + enemyStudent.Student.Base.Name + "!";

                yield return new WaitForSeconds(2f);

                state = BattleState.ENEMYTURN;

                StartCoroutine(EnemyTurn());
            }
            else
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        // show attack message
        dialogueText.text = enemyStudent.Student.Base.Name + "   Attacks";
        yield return new WaitForSeconds(2f);

        // calculate damge
        var damageDetails = myStudent.TakeDamage(enemyStudent.Student.Damage, enemyStudent.Student.Base.Type);

        // play attack animations
        enemyStudent.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);
        myStudent.PlayGetHitAnimation();
        yield return new WaitForSeconds(0.5f);

        // update hp bar
        myHUD.SetHP(myStudent.Student.CurrentHP);
        yield return new WaitForSeconds(0.5f);

        // show attack message
        StartCoroutine(ShowDamageDetails(damageDetails));
        yield return new WaitForSeconds(2f);

        // check if dead
        if (damageDetails.Fainted)
        {
            myStudent.PlayDieAniamtion();

            yield return new WaitForSeconds(1f);
            
            var nextStudent = myClass.GetAliveStudent();
            
            // if the next alive student is still alive
            if (nextStudent != null)
            {
                myStudent.SetUp(nextStudent);

                state = BattleState.START;

                myHUD.SetHUD(myStudent);

                dialogueText.text = "Go  " + myStudent.Student.Base.Name + "!";

                yield return new WaitForSeconds(2f);

                state = BattleState.MYTURN;

                MyTurn();
            }
            else
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
            }

        }
        else
        {
            state = BattleState.MYTURN;
            MyTurn();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        // if there was an attack boost based on type effectiveness
        if (damageDetails.TypeEffectiveness > 1f)
        {
            dialogueText.text = "It's super effective";
            yield return new WaitForSeconds(2f);

        }
        else if (damageDetails.TypeEffectiveness < 1f)
        {
            dialogueText.text = "It's not very effective";
            yield return new WaitForSeconds(2f);
        }
        else
        {
            dialogueText.text = "The attack was successful";
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator EndBattle()
    {
        // differentiates between boss battle and normal battle
        if (enemyTeacherData.subjectName == "Boss")
        {
            // Beating Boss dialogue
            if (state == BattleState.WON)
            {
                enemyTeacher.PlayEnterAnimation();

                dialogueText.text = "No! How could the All Rounder be beaten by an art student!??";
                yield return new WaitForSeconds(5f);

                dialogueText.text = "I  finally  see  it";
                yield return new WaitForSeconds(3f);

                dialogueText.text = "Its  not  all  about  power  and  the  marks...";
                yield return new WaitForSeconds(3f);

                dialogueText.text = "Creativity,  Love  and  Friendship  are  just  as  important.";
                yield return new WaitForSeconds(4f);

                dialogueText.text = "You  were  right  all  along.";
                yield return new WaitForSeconds(3f);

                dialogueText.text = "From  now  on  we  will  follow  you";
                yield return new WaitForSeconds(3f);

                dialogueText.text = "The   Substitute   Teacher !!";
            }
            else // Losing to boss dialogue
            {
                dialogueText.text = "Ha!  Pathetic!  A  sub  could  never  beat  the  school  principal!";
                yield return new WaitForSeconds(4f);

                dialogueText.text = "Get  out  of  my  assembly hall!!!";
                yield return new WaitForSeconds(2f);
            }
        }
        else
        {
            // normal teacher exit
            TeacherExit();
        }
        
        yield return new WaitForSeconds(3f);

        if (state == BattleState.WON)
        {
            string[] saveFile = System.IO.File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");
            string[] BeatenTeachers = saveFile[4].Split();
            
            // if the beaten teachers array does not include the current battling teacher
            if (!(BeatenTeachers.Contains(saveFile[3])))
            {
                StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");
                
                // writes into save file that this teacher was beaten 
                for (int i = 0; i < saveFile.Length; i++)
                {
                    if (i == 4)
                        sw.WriteLine(saveFile[4] + ' ' + saveFile[3]);
                    else if (i == 1)
                    {
                        sw.WriteLine(System.Convert.ToInt32(saveFile[1]) + 1);
                    }
                    else
                        sw.WriteLine(saveFile[i]);
                }
                sw.Close();
            }
        }
        SceneManager.LoadScene("School");
    }

    void MyTurn()
    {
        attackBox.SetActive(true);
        dialogueText.text = "Make a move";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.MYTURN)
            return;

        StartCoroutine(MyAttack());

    }

    public void OpenClassScreen()
    {
        // makes Class Screen Active
        attackBox.SetActive(false);
        state = BattleState.CLASSSCREEN;
        classScreen.SetClassHUD(myClass.students);
        classScreen.gameObject.SetActive(true);
    }

    // exits class screen
    public void OnBackButt()
    {
        classScreen.gameObject.SetActive(false);
        state = BattleState.MYTURN;
        MyTurn();
    }

    // Switchs out current student with chosen student
    public void OnSwitchButton0()
    {
        if (myClass.students[0] == myStudent.Student)
        {
            classScreen.dialogue.text = myStudent.Student.Base.Name + "  is  already  out !";
        }
        else if (myClass.students[0].CurrentHP <= 0)
        {
            classScreen.dialogue.text = myClass.students[0].Base.Name + "  is  too  weak  to  fight !";
        }
        else
        {
            classScreen.gameObject.SetActive(false);
            StartCoroutine(SwitchStudent(myClass.students[0]));
        }
        
    }
    public void OnSwitchButton1()
    {
        if (myClass.students[1] == myStudent.Student)
        {
            classScreen.dialogue.text = myStudent.Student.Base.Name + "  is  already  out !";
        }
        else if (myClass.students[1].CurrentHP <= 0)
        {
            classScreen.dialogue.text = myClass.students[0].Base.Name + "  is  too  weak  to  fight !";
        }
        else
        {
            classScreen.gameObject.SetActive(false);
            StartCoroutine(SwitchStudent(myClass.students[1]));
        }
    }
    public void OnSwitchButton2()
    {
        if (myClass.students[2] == myStudent.Student)
        {
            classScreen.dialogue.text = myStudent.Student.Base.Name + "  is  already  out !";
        }
        else if (myClass.students[2].CurrentHP <= 0)
        {
            classScreen.dialogue.text = myClass.students[0].Base.Name + "  is  too  weak  to  fight !";
        }
        else
        {
            classScreen.gameObject.SetActive(false);
            StartCoroutine(SwitchStudent(myClass.students[2]));
        }
    }
    IEnumerator SwitchStudent(Student newStudent)
    {
        dialogueText.text = "Come Back " + myStudent.Student.Base.Name;
        yield return new WaitForSeconds(2f);
        myStudent.PlayDieAniamtion();
        yield return new WaitForSeconds(2f);

        myStudent.SetUp(newStudent);
        myHUD.SetHUD(myStudent);
        dialogueText.text = "Go  " + myStudent.Student.Base.Name + "!";
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    // Play Boss Introduction animation sequence
    IEnumerator BossIntro()
    {
        myHUD.gameObject.SetActive(false);
        enemyHUD.gameObject.SetActive(false);
        myStudent.gameObject.SetActive(false);
        enemyStudent.gameObject.SetActive(false);

        dialogueText.text = "You  challenge   Ms. Hughes,   the   principal,   in  the  Hall";
        myTeacher.PlayEnterAnimation();
        enemyTeacher.PlayEnterAnimation();
        yield return new WaitForSeconds(3f);

        dialogueText.text = "So   you're   the   sub   who's   been   foolishly   trying  to  change  my  school";
        yield return new WaitForSeconds(5f);

        dialogueText.text = "School  is   all   about   the    marks!!!";
        yield return new WaitForSeconds(3f);

        dialogueText.text = "You   don't   see   it ?";
        yield return new WaitForSeconds(3f);

        dialogueText.text = "Then  I'll   show   you !!";
        yield return new WaitForSeconds(3f); 

        dialogueText.text = "Lets  Fight !!!";
        yield return new WaitForSeconds(1f);
        
        myTeacher.PlayExitAnimation();
        enemyTeacher.PlayExitAnimation();
        yield return new WaitForSeconds(1f);

        myHUD.gameObject.SetActive(true);
        enemyHUD.gameObject.SetActive(true);
        myStudent.gameObject.SetActive(true);
        enemyStudent.gameObject.SetActive(true);

        StartBattle(myClass, enemyClass);
    }
    // Plays teacher animation sequences
    IEnumerator TeacherIntro()
    {
        myHUD.gameObject.SetActive(false);
        enemyHUD.gameObject.SetActive(false);
        myStudent.gameObject.SetActive(false);
        enemyStudent.gameObject.SetActive(false);

        dialogueText.text = "You  meet  " + enemyTeacherData.Tname + "  in  the  classroom";
        myTeacher.PlayEnterAnimation();
        enemyTeacher.PlayEnterAnimation();
        yield return new WaitForSeconds(3f);

        dialogueText.text = "So   you're   the   new   sub"; //+ name
        yield return new WaitForSeconds(2f);

        dialogueText.text = "I  am  the   " + enemyTeacherData.subjectName + "   teacher"; //+ name
        yield return new WaitForSeconds(2f);

        dialogueText.text = "So  you  think  your  class  is  smarter  than   mine !"; // subject
        yield return new WaitForSeconds(2.5f);

        dialogueText.text = "Lets  Fight !!!";
        yield return new WaitForSeconds(1f);
        myTeacher.PlayExitAnimation();
        enemyTeacher.PlayExitAnimation();
        yield return new WaitForSeconds(1f);

        myHUD.gameObject.SetActive(true);
        enemyHUD.gameObject.SetActive(true);
        myStudent.gameObject.SetActive(true);
        enemyStudent.gameObject.SetActive(true);

        StartBattle(myClass, enemyClass);

    }
    public void TeacherExit()
    {
        enemyHUD.gameObject.SetActive(false);
        if (state == BattleState.WON)
        {
            enemyTeacher.PlayEnterAnimation();
            dialogueText.text = "My.. my  students were  just  having  bad  day  thats  all!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You  really  thought  your  class  was  smarter  than  mine";
        }
        
    }

    public void OnBribeButton()
    {
        // reads data in save file about the team
        string[] saveFile = File.ReadAllLines(@"..\\Assembly\\Assets\\Scripts\\save.txt");
        string[] StudentTeam = saveFile[2].Split(' ');
        int numInTeam = StudentTeam.Length / 2;
        
        // Checks if team is full
        if (numInTeam >= 3)
        {
            dialogueText.text = "Full Team!!";
        }
        else if (enemyTeacherData.subjectName == "Boss")
        {
            dialogueText.text = "You Can't";
        }
        else
        {
            attackBox.SetActive(false);

            System.Random rnd = new System.Random();

            // Calculates variables for the catch probability
            int M = rnd.Next(0, 255);
            float f = (enemyStudent.Student.MaxHP * 255 * 4) / (enemyStudent.Student.CurrentHP * 10);

            if (f >= M)
            {
                StreamWriter sw = new StreamWriter("..\\Assembly\\Assets\\Scripts\\save.txt");

                // gets the enemyStudent to be caught details
                string newMember = enemyStudent.Student.Base.Name + ' ' + enemyStudent.Student.level;
                string newLine = saveFile[2] + ' ' + newMember;

                // rewrites new save details into the text file
                for (int i = 0; i < saveFile.Length; i++)
                {
                    if (i == 2)
                        sw.WriteLine(newLine);
                    else
                        sw.WriteLine(saveFile[i]);
                }

                sw.Close();

                // adds student to current team in the battle
                myClass.students.Add(new Student(Resources.Load<StudentBase>("Students/" + enemyStudent.Student.Base.Name), System.Convert.ToInt32(enemyStudent.Student.level)));

                StartCoroutine(BribeAnimation(true));
            }
            else
            {
                StartCoroutine(BribeAnimation(false));
            }


        }
    }
    // plays bribe success or fail animation
    IEnumerator BribeAnimation(bool captured)
    {
        myStudent.gameObject.SetActive(false);
        myTeacher.PlayEnterAnimation();
        dialogueText.text = "Hey " + enemyStudent.Student.Base.Name + " I  will  give  you  good  marks  if  you  join  my  class";
        yield return new WaitForSeconds(4f);
        if (captured)
        {
            dialogueText.text = enemyStudent.Student.Base.Name + ":  OK.  I  will  join";
            yield return new WaitForSeconds(2f);

            enemyStudent.PlayDieAniamtion();
            enemyTeacher.PlayEnterAnimation();
            dialogueText.text = "Nooo !!  You  took  my student !!";
            yield return new WaitForSeconds(2f);

            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            dialogueText.text = enemyStudent.Student.Base.Name + ":  NO.  I  will  never  betray  my teacher";
            yield return new WaitForSeconds(3f);

            dialogueText.text = "T:  You  really  thought  my  student  would  leave  me  to  join  your  class?!";
            yield return new WaitForSeconds(4f);

            myTeacher.PlayExitAnimation();
            yield return new WaitForSeconds(1f);

            myStudent.gameObject.SetActive(true);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
}
