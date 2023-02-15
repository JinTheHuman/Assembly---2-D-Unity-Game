using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TeacherMovement : MonoBehaviour
{
    public bool isPlayer;
    Image image;
    Vector3 originalPos;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
    }

    public void PlayEnterAnimation()
    {
        // moves image to position in 1 second
        if (isPlayer)
            image.transform.DOLocalMoveX(originalPos.x + 310f, 1f);
        else
            image.transform.DOLocalMoveX(originalPos.x - 310f, 1f);
        
    }
    public void PlayExitAnimation()
    {
        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

}
