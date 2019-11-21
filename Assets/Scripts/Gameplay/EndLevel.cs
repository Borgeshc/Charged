using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    ScoreManager scoreManager;
    RaceAgainstTime raceAgainstTime;

    GameObject gameManager;

    Collider2D myCollider;

	// Use this for initialization
	void Start ()
    { 
        myCollider = GetComponent<BoxCollider2D>();
        myCollider.enabled = true;
        gameManager = GameObject.Find("GameManager");

        scoreManager = gameManager.GetComponent<ScoreManager>();

        if(PlayerManager.numberOfPlayers == 2)
        {
            raceAgainstTime = GameObject.Find("GameManager").GetComponent<RaceAgainstTime>();
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerManager.numberOfPlayers == 1 && other.gameObject.tag == "PlayerOneProjectile")
        {
            Destroy(other.gameObject);
            scoreManager.LevelCompleted();
            gameManager.GetComponent<ExitLevelScript>().enabled = false;
            DeathManager.isFinished = true;
        }
        else if (other.gameObject.tag == "PlayerOneProjectile" || other.gameObject.tag == "PlayerTwoProjectile")
        {
            Destroy(other.gameObject);
            myCollider.enabled = false;
            raceAgainstTime.IncrementPoints(other.gameObject);
        }
    }
}
