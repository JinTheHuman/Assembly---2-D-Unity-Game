using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 change;
    public Transform targetPoint; // imaginary point which the character sprite follows
    private Animator anim;
    public LayerMask collision;
    public Player me;

    // Start is called before the first frame update
    void Start()
    {
        // Import the Animator from Unity into anim
        anim = GetComponent<Animator>();
        targetPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {       
        // character's position moves towards the movingpoint at a determined speed
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // gets movement input data from Unity input manager
        change.y = Input.GetAxisRaw("Vertical");
        change.x = Input.GetAxisRaw("Horizontal");

        //checks if the character is on the moving point
        if (Vector3.Distance(transform.position, targetPoint.position) == 0)
        {

            if (change.y != 0)
            {
                anim.SetFloat("moveY", change.y);
                anim.SetFloat("moveX", 0);
            }
            else if (change.x != 0)
            {
                anim.SetFloat("moveY", 0);
                anim.SetFloat("moveX", change.x);
            }
            
            // sets moving variable in the Unity animator as false
            anim.SetBool("Moving", false);
            // moves the moving point
            MovePoint();

        }
        else //the charcter sprite is moving to the target point
        {
            // sets moving variable in the unity animator as true
            anim.SetBool("Moving", true);
        }

        //Debug.Log(change);

    }
    void MovePoint()
    {
        //if there is vertical movement
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (!Physics2D.OverlapCircle(targetPoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, collision))
            {
                targetPoint.position += new Vector3(0f, change.y, 0f);
            }
        } //if there is horizontal movement
        else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            if (!Physics2D.OverlapCircle(targetPoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, collision))
            {
                targetPoint.position += new Vector3(change.x, 0f, 0f);
            }
        }
        
        

        
    }
}
