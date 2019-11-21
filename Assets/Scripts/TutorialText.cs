using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialText : MonoBehaviour {

    [Tooltip("The keys needed to be pressed to reset the brightness")]
    public KeyCode[] keys;
    [Tooltip("How long before the text fades in")]
    public float delay;
    [Header("Transperncy Related Variables"), Tooltip("How low the alpha can be set")]
    public int minTrans = 80;
    [Tooltip("How fast the alpha increases per frame")]
    public double fadeInRate = 2;
    [Tooltip("How fast the alpha decreases per frame")]
    public double fadeOutRate = .5;
    Text text;
    double alpha = 0;           //used to keep track of the alpha value
    int fadeStage = 0;          //used to manage the different stages
    // Use this for initialization
    void Start ()
    {
        text = GetComponent<Text>();
        text.color = new Color32(255, 255, 255, (byte)alpha); //text starts as transparent
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!DeathManager.isFinished)   //don't play when the game is finished
        {
            switch (fadeStage)
            {
                case 0:
                    StartCoroutine(FadeIn());
                    break;
                case 1:
                    if (alpha <= 252)      //fades in, set at 252 to prevent it going over 255 and flashing
                    {
                        alpha += fadeInRate;
                        text.color = new Color32(255, 255, 255, (byte)alpha);
                    }
                    else
                    {
                        alpha = 255;
                        fadeStage = 2;      //when it has fully fadded in
                    }
                    break;
                case 2:
                    foreach (KeyCode key in keys)
                    {
                        if (Input.GetKey(key))
                        {
                            fadeStage = 3;
                        }
                    }

                    if (keys.Length == 0)
                    {
                        fadeStage = 3;
                    }

                    break;
                case 3:
                    foreach (KeyCode key in keys)
                    {
                        if (Input.GetKey(key))
                        {
                            alpha = 255;               //if a set button is pressed, reset the transparentcy
                            text.color = new Color32(255, 255, 255, (byte)alpha);
                        }
                        else
                        {
                            FadeOut();
                        }
                    }

                    if (keys.Length == 0)
                    {
                        FadeOut();
                    }

                    
                    break;
                default:
                    Debug.LogError("You're out of range. Please consult your nearest programmer for assistance.");
                    break;
            }
        }
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(delay);
        fadeStage = 1;                              //goes to the fade in stage of the text
    }

    void FadeOut()
    {
        if (alpha > minTrans)
        {
            alpha = alpha - fadeOutRate;            //slowly decreses the alpha
        }

        text.color = new Color32(255, 255, 255, (byte)alpha); //applies the new alpha
    }
}
//~~~Peter