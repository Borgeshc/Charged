using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

    // Use this for initialization
    void Start () {

	}
	
	public void Play(string sceneName)
    {
        SceneManager.LoadScene(sceneName);      //Load scene with string
    }

    public void Play(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);    //Load scene with int
    }

    public void Quit()
    {
        Application.Quit();                     //Quits game
    }

    public void OnePlayer()
    {
        PlayerManager.numberOfPlayers = 1;      //1 player
    }

    public void TwoPlayer()
    {
        PlayerManager.numberOfPlayers = 2;      //2 player
    }
}
