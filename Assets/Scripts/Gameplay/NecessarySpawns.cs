using UnityEngine;
using System.Collections;

public class NecessarySpawns : MonoBehaviour {

    [Tooltip("The prefab or gameobject of the launcher")]
    public GameObject launcher;
    [Tooltip("The prefab or gameobject of the exit")]
    public GameObject exit;
    GameObject inSceneLauncher;         //saves the launcher object
    GameObject inSceneExit;             //saves the exit object

    GameObject canvas;                  //grabs canvas to make sure the transform is in the correct spot

 //   public RectTransform background;    //will be private when Sam's code is implemented
    RectTransform startPoint;
    RectTransform endPoint;

    //WhateverScriptSamMade backgroundManager       grabs a copy of whatever Sam's background script is
    void Start()
    {
        //backgroundManager = GameObject.FindObjectWithTag("GameManager").GetComponent<WhateverScriptSamMade>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        if (canvas == null)
            Debug.LogError("The Canvas is not tagged.");
    }
    public void ActivateObjects(GameObject background)
    {
        //background = (RectTransform) backgroundManager.activebackground.transform;
        startPoint = (RectTransform)background.transform.FindChild("StartPoint");
        if (startPoint == null)
        {
            startPoint.anchorMin = new Vector2(0, 0.5f);
            startPoint.anchorMax = new Vector2(0, 0.5f);
            startPoint.localPosition = new Vector3(60, 0, 0);
        }

        endPoint = (RectTransform)background.transform.FindChild("EndPoint");
        if (endPoint == null)
        {
            endPoint.anchorMin = new Vector2(1, 0.5f);
            endPoint.anchorMax = new Vector2(1, 0.5f);
            endPoint.localPosition = new Vector3(-60, 0, 0);
        }


        if (inSceneLauncher != null)        //if it already exists, destory that previous object
            Destroy(inSceneLauncher);

        if (inSceneExit != null)
            Destroy(inSceneExit);

        inSceneLauncher = (GameObject) Instantiate(launcher, canvas.transform);
        inSceneLauncher.GetComponent<RectTransform>().localPosition = startPoint.localPosition;
        inSceneLauncher.transform.localScale = Vector3.one;
        
        inSceneExit = (GameObject) Instantiate(exit, canvas.transform);
        inSceneExit.GetComponent<RectTransform>().localPosition = endPoint.localPosition;
        inSceneExit.transform.localScale = Vector3.one;
    }
}
