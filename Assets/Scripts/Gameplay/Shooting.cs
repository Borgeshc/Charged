﻿using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    GameObject canvas;
    public AudioSource launcher;
    //GameObject preplacedObj;
    [SerializeField]
    Rigidbody2D myBullet;
    ScoreManager scoreManager;
    private IonPlacement ionPlacement;

    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas");
       // preplacedObj = canvas.transform.FindChild("PreplacedObj").gameObject;
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        ionPlacement = GameObject.Find("GameManager").GetComponent<IonPlacement>();
}
	
	// Update is called once per frame
	void Update ()
    {
        if (tag == "P1Gun" && Input.GetKeyDown("space") && DeathManager.p1CanShoot == true)
        {
            Shoot();
            ionPlacement.cannotPlacePositive = true;            // Cannot place during shot
            ionPlacement.cannotPlaceNegative = true;
        }
        else if (tag == "P2Gun" && Input.GetKeyDown("return") && DeathManager.p2CanShoot == true)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        launcher.Play();
        print(name + " the object shooting ");
        if (PlayerManager.numberOfPlayers == 1)
        {
            scoreManager.UpdateScore();
        }

        print(transform.position + " the position");

        Rigidbody2D clone = Instantiate(myBullet, transform.position, Quaternion.identity) as Rigidbody2D;
        clone.velocity = transform.TransformDirection(Vector3.right * ControlScript.charge);
        clone.transform.SetParent(canvas.transform);
        clone.GetComponent<RectTransform>().position = transform.GetComponent<RectTransform>().position;
        clone.GetComponent<RectTransform>().localScale = new Vector3(.4f, .4f, .4f);

        TrailRenderer trail = clone.gameObject.GetComponentInChildren<TrailRenderer>();
        trail.enabled = true;
        trail.sortingOrder = 1;
        print(trail.name);

        switch(tag)
        {
            case "P1Gun":
                DeathManager.p1CanShoot = false;
                break;
            case "P2Gun":
                DeathManager.p2CanShoot = false;
                break;
        }
    }
}
