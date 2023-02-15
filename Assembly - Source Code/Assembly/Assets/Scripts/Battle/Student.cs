using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Student
{
    public StudentBase _Base;
    public int level;

    public StudentBase Base { 
        get
        {
            return _Base;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
    }
    public float CurrentHP { get; set; }

    public Student(StudentBase sBase, int sLevel)
    {
        _Base = sBase;
        level = sLevel;
        CurrentHP = MaxHP;
    }

    public void Init()
    {
        CurrentHP = MaxHP;
    }

    // calculates stats depending on level
    public float Damage
    {
        get { return (Base.Damage * Level) / 100f + 5f; }
    }

    public float MaxHP
    {
        get { return (Base.MaxHP * Level) / 100f + 10f; }
    }
}
