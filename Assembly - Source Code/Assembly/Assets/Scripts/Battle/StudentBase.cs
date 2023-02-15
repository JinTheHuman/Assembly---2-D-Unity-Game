using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Student", menuName = "Student/Create new Student")] 
public class StudentBase : ScriptableObject
{
    [SerializeField] string Sname;
    [SerializeField] string type;

    [SerializeField] float damage;
    [SerializeField] float maxHP;

    [SerializeField] Sprite sprite;

    [SerializeField] string weakness;
    [SerializeField] string resistance;

    public string Name
    {
        get { return Sname; }
    }

    public string Type
    {
        get { return type; }
    }

    public float Damage
    {
        get { return damage; }
    }

    public float MaxHP
    {
        get { return maxHP; }
    }

    public string Weakness
    {
        get { return weakness; }
    }

    public string Resistance
    {
        get { return resistance; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
    }
}
