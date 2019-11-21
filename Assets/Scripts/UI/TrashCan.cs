using UnityEngine;
using System.Collections;

public class TrashCan : MonoBehaviour {

    Transform cloneFolder;

    void Start()
    {
        cloneFolder = GameObject.Find("CloneFolder").transform;  //finds where all the items are stored
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Clone"))
        {
            if (Input.GetMouseButtonUp(0))                      //when released over trash can
                Destroy(col.gameObject);
        }
    }

    public void DeleteAll()
    {
        foreach (Transform child in cloneFolder)
        {
            Destroy(child.gameObject);
        }
    }
}

//~Peter
