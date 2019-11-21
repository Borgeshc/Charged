using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonScript : MonoBehaviour
{
    float doubleClick;
    [HideInInspector]
    public int starsNeeded;
    [HideInInspector]
    public int myLevel;


    public void PlayLevel()
    {
        // print(this.gameObject.GetComponent<LevelSelect>().starsNeeded[sceneLevel]);
        //  checking to see if the level image was double clicked (CUZ DOUBLE CLICKING IS BETTER THAN SINGLE CLICKING!)
        if (doubleClick <= Time.time)
            doubleClick = Time.time + .2f;
        else
        {
            //if so, play scene level
            if (StarCount.starCount >= starsNeeded)
            {
                SceneManager.LoadScene(myLevel);
            }
            //  else  //PLAY SOUND HERE
        }
    }//end of function
}