using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarIncrementer : MonoBehaviour
{
    public RectTransform content;                                   //Parent of all of the objects that are instantiated on the scrollbar
    public GameObject buttonPrefab;                                 //Prefab of the object that will be instantiated on the scrollbar
    public List<GameObject> backgroundPrefabs;                      //List of prefabs that are used as the content for the buttons images, and OnClick()
    bool scrolling;                                                 //Bool to keep track of when the scrollbar is scrolling
    float stepAmount;                                               //The amount that the scrollbar tries to move when clicking an arrow button
    float timer;
    Vector3 nextPos;                                                //Keeping track of the position the scrollbar should move to when an arrow button is clicked

    void Start()
    {
        //Creating all of the buttons on the content
        for (int i = 0; i < backgroundPrefabs.Count; i++)
        {
            GameObject clone = Instantiate(buttonPrefab, content.transform) as GameObject;
            clone.GetComponent<RectTransform>().localPosition = Vector3.zero;
            clone.GetComponent<RectTransform>().localScale = Vector3.one;
            clone.GetComponent<Image>().sprite = backgroundPrefabs[i].GetComponent<Image>().sprite;
            clone.GetComponent<LevelEditorScript>().backgroundPrefab = backgroundPrefabs[i];
        }
    }

    void Update()
    {
        //Timer that stops the scrolling coroutine if it can't stop by reaching the nextPos
        if (timer >= .6f)
        {
            StopAllCoroutines();
            timer = 0;
            scrolling = false;
        }
    }

    //set the step amount and call the coroutine to scroll
    public void Step(float aStepAmount)
    {
        stepAmount = aStepAmount;        
        StartCoroutine(ScrollCoroutine());
    }

    IEnumerator ScrollCoroutine()
    {
        if(!scrolling)
        {
            scrolling = true;
            nextPos = new Vector3(content.position.x + stepAmount, content.position.y, content.position.z);         //set nextPos.x to the sum of the content's x position and stepAmount
            yield return new WaitUntil(Scroll);
            timer = 0;
            scrolling = false;
        }
    }

    //Lerps content's position to nextPos.  returns true if it reaches within 1 unit of nextPos, otherwise it returns false
    bool Scroll()
    {
        timer += 1 / 60.0f;
        content.position = Vector3.Lerp(content.position, nextPos, Time.deltaTime * 3);

        if (Vector3.Distance(content.position, nextPos) <= 1f)
            return true;
        else
            return false;
    }
}