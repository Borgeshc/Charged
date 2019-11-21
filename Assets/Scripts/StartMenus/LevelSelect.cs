//I'm da greatest ^^
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // [Header("Game Object Levels")]
    GameObject level;
    [Header("How many levels?"), Tooltip("Anything more than the starNeeded array will appear as custom levels")]
    public int maxLevels;
    
    public int[] starsNeeded;
    public static int[] staticStarsNeeded;
    public AudioSource error;
    int levelCount = 0;
    int xPos = 0;
    public GameObject content;
    [HideInInspector]
    public int stars;
    LevelButtonScript myScript;

    public GameObject levelObject;
    public Sprite unlockedFrame;
    public Sprite lockedFrame;
    public Sprite unlockLock;
    public Sprite lockLock;

    // setting this object's activeness, 

    void OnEnable()
    {
        stars = StarCount.starCount;

        staticStarsNeeded = new int[starsNeeded.Length];

        for (int index = 0; index < starsNeeded.Length; index++)
        {
            staticStarsNeeded[index] = starsNeeded[index];
        }

        //looping through all the level where stars are needed and checking to see if the stars aqcuired is greater than the stars needed

        //  loading prefabs from the (resource folder > Levels) and assigning them to the variable "levels"
        //levels = Resources.LoadAll<GameObject>("Levels");
        //looping through all the levels from the (resource folder > Levels) and instantiating them as GameObjects
        while (levelCount < maxLevels)
        {
            level = Instantiate(levelObject);
            GameObject child = level.transform.Find("Locked").gameObject;
            child.SetActive(true);

            if (child != null)
            {
                if (levelCount < starsNeeded.Length)
                {
                    if (starsNeeded[levelCount] <= stars)
                    {
                        level.GetComponent<Image>().sprite = unlockedFrame;
                        child.GetComponent<Image>().sprite = unlockLock;
                    }
                    else
                    {
                        level.GetComponent<Image>().sprite = lockedFrame;
                        child.GetComponent<Image>().sprite = lockLock;
                    }
                }
            }

            levelCount++;
            CreateSlot();
            //finding the "Locked" image of the instantiated object
        }
    }

    //settig this object's activeness to false will destroy all levels in order to prevent duplication
    //the reason we istantiate OnEnable is so that we can keep the information up to date
    void OnDisable()
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void CreateSlot()
    {
        myScript = level.GetComponent<LevelButtonScript>();
        myScript.myLevel = levelCount;
        //creating the level image slot
        if (levelCount <= starsNeeded.Length)//here we are detecting whether or not the array length is larger than the variable, levelCount, so we can determine whether or not this is a custom map.
        {
            level.name = "Level " + levelCount.ToString();   //turning level's name and the level number to a string
            myScript.starsNeeded = starsNeeded[levelCount - 1];
        }
        else
        {
            level.name = "Custom " + ((levelCount + 1) - maxLevels).ToString();   //Turning all levels that are instantiated after maxLevels into a custom string (i.e Custom 1)
            level.transform.Find("Locked").gameObject.SetActive(false);
        }

        level.GetComponentInChildren<Text>().text = level.name;   //finding the child text of that level and printing the level name and level number
        level.transform.SetParent(content.transform);  //assigning the instantiate level to child of the GameObject, "content"

        //getting the button component and assigning the game object, instantiated level, and the load level function to the 
        //on click functions
        level.GetComponent<RectTransform>().localPosition = new Vector3(xPos, 0, 0);   //here we are assigning the proper scale, rotation, position, width, and height of the gameobject "level"
        level.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        level.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        xPos += (int)level.GetComponent<RectTransform>().rect.height;

    }

    //scrolling by clicking the left arrow
    public void LeftArrowScroll()
    {
        content.transform.position += new Vector3(+82, 0, 0);
    }
    //scrolling by clicking the right arrow
    public void RightArrowScroll()
    {
        content.transform.position -= new Vector3(+82, 0, 0);
    }
}//end of class
