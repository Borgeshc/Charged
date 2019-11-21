using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarCount : MonoBehaviour {

	public static int starCount = 0;
	void Start ()
    {
        GetComponent<Text>().text = "x " + starCount;
	}
}
