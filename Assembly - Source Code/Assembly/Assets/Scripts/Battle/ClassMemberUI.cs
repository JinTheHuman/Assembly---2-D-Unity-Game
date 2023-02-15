using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassMemberUI : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpslider;
    public Image studentSprite;
    public int Index;

    Student student;

    public void SetHUD(Student student)
    {
        nameText.text = student.Base.Name;
        levelText.text = ":L " + student.Level;
        studentSprite.sprite = student.Base.Sprite;
        hpslider.maxValue = student.MaxHP;
        hpslider.value = student.CurrentHP;
    }

    public void SetHP(float hp)
    {
        hpslider.value = hp;
    }
}
