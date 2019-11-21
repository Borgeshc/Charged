using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public static int numberOfPlayers = 1;      
    public GameObject playerOne;            //Player One prefab
    public GameObject playerTwo;            //Player Two prefab

    GameObject startPoint;                  //Where the player will spawn
    [HideInInspector]
    public GameObject p1Clone;             
    GameObject p2Clone;
    GameObject canvas;
    GameObject preplacedObj;
    void Start()
    {
        canvas = GameObject.Find("Canvas");

        startPoint = GameObject.Find("StartPoint");
        ControlScript.startRotation = (int)startPoint.GetComponent<RectTransform>().eulerAngles.z;

        switch (numberOfPlayers)            //If one player spawn p1, if two spawn p1 and p2
        {
            case 1:
                p1Clone = Instantiate(playerOne, startPoint.transform.position, startPoint.transform.rotation) as GameObject;
                p1Clone.transform.SetParent(canvas.transform);
                p1Clone.GetComponent<RectTransform>().localScale = Vector3.one;
                p1Clone.transform.tag = "PlayerOne";
                print(p1Clone);
                break;
            case 2:
                p1Clone = Instantiate(playerOne, startPoint.transform.position, startPoint.transform.rotation) as GameObject;
                p1Clone.transform.SetParent(canvas.transform);
                p1Clone.GetComponent<RectTransform>().localScale = Vector3.one;
                p1Clone.transform.tag = "PlayerOne";

                p2Clone = Instantiate(playerTwo, startPoint.transform.position, startPoint.transform.rotation) as GameObject;
                p2Clone.transform.SetParent(canvas.transform);
                p2Clone.GetComponent<RectTransform>().localScale = Vector3.one;
                p2Clone.transform.tag = "PlayerTwo";
                break;
        }
    }
}
