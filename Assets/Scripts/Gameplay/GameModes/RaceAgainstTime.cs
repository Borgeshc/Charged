using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceAgainstTime : MonoBehaviour
{
    [HideInInspector]
    public static int playerOnePoints;                      //The amount of points that player one has
    [HideInInspector]
    public static int playerTwoPoints;                      //The amount of points that player two has

    public GameObject results;
    [Tooltip("How many points either player must score to win the game.")]
    public int playerWinScore;

    public static int winScore;
    public int timeBetweenScenes;
    public Text time;                                       //Reference to the text object that shows time
    public float matchTime;                                 //How long each round will last for
    public bool matchStarted;                               //Checks to see if the match has begun
    public Text p1Points;                                   //Text field for p1 points
    public Text p2Points;                                   //Text field for p2 points
    public Text nextRoundIn;                                //Text field for next round counter

    float timer;
    bool nextMatchCounter;
    float nextRoundTimer;
	void Start ()
    {
        if(PlayerManager.numberOfPlayers == 1)
        {
            enabled = false;
        }
        winScore = playerWinScore;
        timer = matchTime;                                  //Set our timer to the matchtime chosen
        nextRoundTimer = timeBetweenScenes;
        matchStarted = true;

        if (PlayerManager.numberOfPlayers == 1)
            this.enabled = false;
	}
	
	void Update ()
    {
        if(matchStarted)                                    //Begin the countdown timer
        {
            timer -= 1 * Time.deltaTime;
            if(timer <= 0)                                  //Once the timer is over reset the time
            {
                matchStarted = false;
                ShowResults();
            }
        }
        time.text = "Time Left: " + (int)timer;

        if(nextMatchCounter)
        {
            nextRoundTimer -= 1 * Time.deltaTime;
            nextRoundIn.text = "Next Match In" + '\n' + (int)nextRoundTimer;
            if (nextRoundTimer <= 0)                                  //Once the timer is over reset the time
            {
                nextMatchCounter = false;
                results.SetActive(false);
                StartNextMatch();
            }
        }
	}

    void ShowResults()
    {
        p1Points.text = "Player One Points " + playerOnePoints;
        p2Points.text = "Player Two Points " + playerTwoPoints;
        timer = matchTime;
        results.SetActive(true);
        nextMatchCounter = true;
    }
    void StartNextMatch()
    {
        print(playerOnePoints);
        print(playerTwoPoints);
  
        int randomScene = Random.Range(2, (SceneManager.sceneCountInBuildSettings));
        SceneManager.LoadScene(randomScene);
    }

    public void IncrementPoints(GameObject player)
    {
        if(player.tag == "PlayerOneProjectile")
        {
            playerOnePoints++;
        }
        else if(player.tag == "PlayerTwoProjectile")
        {
            playerTwoPoints++;
        }
        matchStarted = false;
        ShowResults();
        if (playerOnePoints >= winScore || playerTwoPoints >= winScore)
        {
            print("Loading next scene" + playerOnePoints + playerTwoPoints);
            SceneManager.LoadScene("RaceEndScene");
        }
    }

    ////////To Do List/////////

    //Need to instantiate the players after each round or figure out a better solution
    //Need to increment points
    //Need a over all win scenario for when one player reaches the max points
}
