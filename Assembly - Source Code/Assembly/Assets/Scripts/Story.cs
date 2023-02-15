using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Story : MonoBehaviour
{
    public Text dialogue;

    // Image sprites
    public Image Burning;
    public Image crying;
    public Image Overworked;
    public Image NoFuture;
    public Image Abuse;
    public Image Black;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayStory());
    }
    // Story introduction sequence
    IEnumerator PlayStory()
    {
        dialogue.text = "You  have  been  recently  hired  at  North  Sydney  Boys  High  School";
        yield return new WaitForSeconds(3f);

        dialogue.text = "But  when  you  walk  into  the  school  walls  for  the  first  time...";
        yield return new WaitForSeconds(4f);

        dialogue.text = "You  are  met  with  the  horrors  of  overworked  students,";
        FadeIn(Overworked);
        yield return new WaitForSeconds(4f);
        FadeOut(Overworked);
        yield return new WaitForSeconds(1f);

        dialogue.text = "Students  being  abused";
        FadeIn(Abuse);
        yield return new WaitForSeconds(4f);
        FadeOut(Abuse);
        yield return new WaitForSeconds(1f);

        dialogue.text = "Students  being  overcome  with  stress";
        FadeIn(crying);
        yield return new WaitForSeconds(4f);
        FadeOut(crying);
        yield return new WaitForSeconds(1f);

        dialogue.text = "Creativity  being  killed";
        FadeIn(Burning);
        yield return new WaitForSeconds(4f);
        FadeOut(Burning);
        yield return new WaitForSeconds(1f);

        dialogue.text = "Students  being  destined  to  a  grey  ofice  job";
        FadeIn(NoFuture);
        yield return new WaitForSeconds(4f);
        FadeOut(NoFuture);
        yield return new WaitForSeconds(1f);

        dialogue.text = "You  realize  it  is  your  job  to  change  this  school";
        yield return new WaitForSeconds(2f);

        FadeIn(Black);
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("School");
    }

    public void FadeIn(Image image)
    {
        image.DOFade(1, 2f);
    }

    public void FadeOut(Image image)
    {
        image.DOFade(0, 2f);
    }
}
