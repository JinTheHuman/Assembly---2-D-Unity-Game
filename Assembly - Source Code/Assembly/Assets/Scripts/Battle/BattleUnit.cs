using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{   
    
    // Indicates wheather this is the player's unit or the enemy's unit
    [SerializeField] bool isMyUnit;

    public Student Student { get; set; }

    Image image;
    Vector3 originalPos;
    Color originalColor;

    // Function called when instance is loaded
    private void Awake()
    {
        image = GetComponent<Image>();

        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void SetUp(Student student)
    {
        Student = student;
        image.sprite = Student.Base.Sprite;
        image.DOColor(originalColor, 0f);
        Debug.Log("works");
        PlayEnterAnimation();
    }

    public void PlayEnterAnimation()
    {
        if (isMyUnit)
            image.transform.localPosition = new Vector3(-515f, originalPos.y);
        else
            image.transform.localPosition = new Vector3(515f, originalPos.y);

        // moves image to position in 1 second
        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        if (isMyUnit)
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
        else
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));

        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayGetHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.grey, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayDieAniamtion()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0, 0.5f));
    }

    public DamageDetails TakeDamage(float dmg, string attackType)
    {
        // Create damage detials instance
        var dmgDetails = new DamageDetails()
        {
            TypeEffectiveness = 1f,
            Fainted = false
        };

        // Attack is from All rounder
        if (attackType == "All")
        {
            // Damage is recieved by Art student
            if (Student.Base.Type != "Art")
            {
                dmg *= dmgDetails.TypeEffectiveness = 2;
            }
            else
            {
                dmg *= dmgDetails.TypeEffectiveness = 0.5f;
            }
        }
        else if (attackType == Student.Base.Weakness || Student.Base.Weakness == "everything")
        {
            dmg *= dmgDetails.TypeEffectiveness = 2;
        }
        else if (attackType == Student.Base.Resistance || Student.Base.Resistance == "everything")
        {
            dmg *= dmgDetails.TypeEffectiveness = 0.5f;
        }

        Student.CurrentHP -= dmg;
        
        Debug.Log(dmg + "hp left: " + Student.CurrentHP + " of " + Student.MaxHP);
        
        if (Student.CurrentHP <= 0)
            dmgDetails.Fainted = true;

        return dmgDetails;
    }
}

public class DamageDetails
{
    public bool Fainted { get; set; }
    public float TypeEffectiveness { get; set; }
}
