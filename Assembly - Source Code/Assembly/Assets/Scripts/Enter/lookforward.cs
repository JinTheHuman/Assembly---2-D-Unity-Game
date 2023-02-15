using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookforward : MonoBehaviour
{
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim.SetFloat("moveY", 1);
    }
}
