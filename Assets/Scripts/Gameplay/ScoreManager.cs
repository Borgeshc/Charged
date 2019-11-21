using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public bool disableScore = false;
    [Tooltip("The canvas text object for the tries count.")]
    public Text triesText;
    [HideInInspector]//Hice pública Esto! fue modificado por Cristóbal así puedo obtener esta información desde otro script
    public int tries;                                  //Keeps track of the number of tries.
    [HideInInspector]
    public GameObject gameManager;
    public GameObject levelCompletedPanel;
    GameObject nextLevelButton;
    GameObject hideStarsPanel;
    GameObject myRenderer;
    [Header("Star Images")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public int oneStarScore;
    public int twoStarScore;
    public int threeStarScore;
    int starsEarned;
    int starsNeededToCont;
    int availablePositiveIons;
    int availableNegativeIons;
    public static bool canContinue = false;
    IonTrackerScript ionTracker; //getting the script
    IonPlacement ionPlacement; //getting the script
    GameObject player;

    void Start()
    {
        myRenderer = GameObject.Find("Canvas/PlayerOne(Clone)/GunBarrel");
        hideStarsPanel = levelCompletedPanel.transform.FindChild("IgnoreStarsPanel").gameObject;

        if (hideStarsPanel == null)
            Debug.LogError("No ignore stars panel attached to the level complete panel.");

        nextLevelButton = levelCompletedPanel.transform.FindChild("NextLevel").gameObject;

        levelCompletedPanel.SetActive(false);
        ionPlacement = GameObject.Find("GameManager").GetComponent<IonPlacement>();
        gameManager = GameObject.Find("GameManager");
        if (gameManager == null)
            Debug.LogError("No game manager found");

        ionTracker = gameManager.GetComponent<IonTrackerScript>();

        triesText.text = "Attempts: " + tries;      //Change the text at the start to make sure it says the correct text.S

        if (LevelSelect.staticStarsNeeded != null)
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            starsNeededToCont = LevelSelect.staticStarsNeeded[scene];
        }
        availablePositiveIons = ionPlacement.availablePositiveIons;
        availableNegativeIons = ionPlacement.availableNegativeIons;
    }
    public void UpdateScore()
    {
        tries++;                                //Updates the try count
        triesText.text = "Attempts: " + tries;
        ionTracker.ScoreTracker(); //Refrescante la puntuación en el gameManagerScript
    }
    public void ResetScore()
    {
        tries = 0;
        triesText.text = "Attempts: " + tries;      //Resets the try count
        ionTracker.ScoreTracker(); //Refrescante la puntuación en el gameManagerScript
    }
    public void LevelCompleted()
    {
        player = GameObject.FindGameObjectWithTag("PlayerOneProjectile");
        gameManager.GetComponent<IonPlacement>().enabled = false;
        levelCompletedPanel.SetActive(true);
        myRenderer.GetComponent<LineRenderer>().enabled = false;

        //keeping the trail renderer
        DeathManager.trailRenderer = player.transform.FindChild("TrailRenderer").gameObject;
        DeathManager.currentPosition = DeathManager.trailRenderer.transform.position;
        DeathManager.trailRenderer.transform.SetParent(DeathManager.canvas.transform);
        DeathManager.trailRenderer.transform.position = DeathManager.currentPosition;

        if (!disableScore)
        {
            hideStarsPanel.SetActive(false);
            //======================Calculate Score=================================
            ionTracker.points += 100 + ((availablePositiveIons + availableNegativeIons) * 25) - ((IonPlacement.activePositiveIons.Count + IonPlacement.activeNegativeIons.Count) * 25);
            ionTracker.ScoreTracker();

            //========================Show the Stars Earned===========================
            if (ionTracker.points >= threeStarScore)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                starsEarned = 3;
            }
            else if (ionTracker.points < threeStarScore && ionTracker.points >= twoStarScore)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                starsEarned = 2;
            }
            else if (ionTracker.points < twoStarScore && ionTracker.points >= oneStarScore)
            {
                star1.SetActive(true);
                starsEarned = 1;
            }

            StarCount.starCount += starsEarned;

            //=================Stars Needed To Continue=================================
            if (StarCount.starCount >= starsNeededToCont)                   // If enough earned, allow to continue
            {
                GameObject.Find("StarsNeeded").SetActive(false);
                canContinue = true;
                nextLevelButton.GetComponent<Button>().interactable = true;
            }
            else
            {                                                  // Otherwise, show amount needed to cont.
                GameObject.Find("StarsNeeded").GetComponent<Text>().text = "Stars Needed to Continue: " + (starsNeededToCont - StarCount.starCount);
                nextLevelButton.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            canContinue = true;
            hideStarsPanel.SetActive(true);
        }
    }
}


