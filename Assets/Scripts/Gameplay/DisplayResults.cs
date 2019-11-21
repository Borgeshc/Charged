using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    public Text playerWinText;

	void Start ()
    {
        print(RaceAgainstTime.playerOnePoints);
        print(RaceAgainstTime.playerTwoPoints);
        if (PlayerManager.numberOfPlayers == 2)
        {
            if(RaceAgainstTime.playerOnePoints >= RaceAgainstTime.winScore)
            {
                playerWinText.text = "Player One Wins!";
            }
            else if (RaceAgainstTime.playerTwoPoints >= RaceAgainstTime.winScore)
            {
                playerWinText.text = "Player Two Wins!";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
