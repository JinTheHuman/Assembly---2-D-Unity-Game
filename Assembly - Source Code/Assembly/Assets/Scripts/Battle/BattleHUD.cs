using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpslider;

    // Sets Heads up display for battle units in the battle
    public void SetHUD(BattleUnit unit)
    {
        nameText.text = unit.Student.Base.Name;
        levelText.text = ":L " + unit.Student.Level;
        hpslider.maxValue = unit.Student.MaxHP;
        hpslider.value = unit.Student.CurrentHP;
    }

    public void SetHP(float hp)
    {
        StartCoroutine(SetHPSmooth(hp));
    }

    // Animates HP slider to slide down
    public IEnumerator SetHPSmooth(float newHP)
    {
        float currentHP = hpslider.value;
        float change = currentHP - newHP;

        // subtracts currentHP by a small number until currentHP and newHP extremley close
        while (currentHP - newHP > Mathf.Epsilon)
        {
            currentHP -= change * Time.deltaTime;
            hpslider.value = currentHP;
            yield return null;
        }
        hpslider.value = newHP;
    }

}
