using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    Attempts attempts;                                                          //References the scoreManager
    Vector3 currentPosition;
    GameObject player;
    GameObject trailRenderer;
    GameObject canvas;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
    void OnTriggerEnter2D(Collider2D other)                                             //Checks for collision with the player
    {
        if(other.gameObject.tag == "PlayerOneProjectile" || other.gameObject.tag == "PlayerTwoProjectile")
        {
            DeathManager.killProjectile(other.gameObject);
        }
    }
}
