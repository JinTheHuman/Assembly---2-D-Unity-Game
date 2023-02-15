using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour
{
    private Transform Position;
    public Vector3 PrevPosition;
    public Transform target;
    public Player me;
    public Animator anim;
    private string[] downs = { "Math", "Science", "Art", "Hall"};
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "School")
        {
            string[] saveFile = System.IO.File.ReadAllLines("..\\Assembly\\Assets\\Scripts\\save.txt");

            me.PlayerStart();

            // Sets Player facing direction
            if (saveFile[3] == "start")
            {
                anim.SetFloat("moveY", 1);
            }
            else if (!(downs.Contains(saveFile[3])))
            {
                anim.SetFloat("moveX", 1);
            }
            
        }
        
    }

    // Sets player start position according to the save file
    public void PlayerStart()
    {
        string[] text = System.IO.File.ReadAllLines(@"..\\Assembly\\Assets\\Scripts\\save.txt");
        string[] saveFile = text[0].Split(' ');

        Position = GetComponent<Transform>();

        float x = float.Parse(saveFile[0]);
        float y = float.Parse(saveFile[1]);
        float z = float.Parse(saveFile[2]);

        Position.position = new Vector3(x, y, z);
        target.position = new Vector3(x, y, z);

    }

}
