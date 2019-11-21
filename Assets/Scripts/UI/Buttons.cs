using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void NextLevel()
    {
        if (ScoreManager.canContinue == true)           // Have enough stars been earned?
        {
            BasicUtilities.NextLevel();
        }
    }

    public void RestartLevel()
    {
        BasicUtilities.RestartLevel();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
