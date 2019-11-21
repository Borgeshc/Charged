using UnityEngine;
using System.Collections;

public class ToggleBar : MonoBehaviour {

    bool isClosed = false;
    GameObject bar;
    Animator animBar;
    Vector3 startPosition;
    Vector2 emptyPosition;
    RectTransform barTransform;

    bool runningCloorsLight;

    void Start()
    {
        bar = transform.parent.gameObject;              //grab the bar...
        animBar = bar.GetComponent<Animator>();         //and its animator

        emptyPosition = GameObject.FindGameObjectWithTag("Empty").GetComponent<RectTransform>().anchoredPosition;  //grabs the position of the edge of the canvas
        barTransform = bar.GetComponent<RectTransform>();                       
        startPosition = barTransform.anchoredPosition;                      //grabs where the bar starts at for reference
    }

    public void toggleBar()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        if (!runningCloorsLight)
        {
            runningCloorsLight = true;

            if (isClosed)
            {
                animBar.Play("maximize");                       //play animation
                yield return new WaitUntil(LerpDown);           //try to lerp until it reaches it
            }
            else
            {
                animBar.Play("minimize");
                yield return new WaitUntil(LerpUp);
            }

            isClosed = !isClosed;
            runningCloorsLight = false;
        }
    }

    bool LerpUp()
    {
        barTransform.anchoredPosition = Vector3.Lerp(barTransform.anchoredPosition, emptyPosition, Time.deltaTime * 4f); //move between the current spot and end of screen

        if (Vector3.Distance(barTransform.anchoredPosition, emptyPosition) <= .3)
        {
            return true;
        }
        else
            return false;
    }
    bool LerpDown()
    {
        barTransform.anchoredPosition = Vector3.Lerp(barTransform.anchoredPosition, startPosition, Time.deltaTime * 4f); //move between the current spot and the beginning spot

        if (Vector3.Distance(barTransform.anchoredPosition, startPosition) <= .3)
        {
            return true;
        }
        else
            return false;
    }
}
