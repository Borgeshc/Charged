using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scare : MonoBehaviour {

    public GameObject peterPanel;
    public Sprite[] possibleSprites;
    Image scareIMG;
    int popupTime;

    void Awake()
    {
        scareIMG = peterPanel.GetComponent<Image>();
    }

    // Use this for initialization
    IEnumerator Start () {        
        popupTime += Random.Range(15, 30);
        scareIMG.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];        
        yield return new WaitUntil(PopUp);
        yield return new WaitForSeconds(3f);
        peterPanel.SetActive(false);
        StartCoroutine(Start());
	}

    bool PopUp()
    {        
        if (Time.time >= popupTime)
        {
            peterPanel.SetActive(true);
            return true;
        }
        return false;
    }
}