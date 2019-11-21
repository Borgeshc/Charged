using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class LevelEditorScript : MonoBehaviour
{
    public static GameObject background;
    static GameObject levelBackground;
    public GameObject backgroundPrefab;
    public LayerMask layerMask;
    GameObject folder;
    NecessarySpawns necessarySpawns;
    PointerEventData eventData;

    void Start()
    {
        FindBackground();
        necessarySpawns = GameObject.Find("GameManager").GetComponent<NecessarySpawns>();
        folder = GameObject.Find("CloneFolder");

        if (folder == null)
        {
            Debug.LogError("There is not an object called \"CloneFolder\"");
        }
    }

    //Destroys the current background and replaces it with whichever background the player clicked on
    public void SetBackground()
    {
        Destroy(levelBackground);
        background = backgroundPrefab;
        GameObject clone = Instantiate(background, GameObject.Find("BackgroundContainer").transform) as GameObject;
        clone.name = "Background";
        clone.GetComponent<RectTransform>().localPosition = Vector3.zero;
        clone.GetComponent<RectTransform>().localScale = Vector3.one;
        levelBackground = clone;
        necessarySpawns.ActivateObjects(background);
    }

    //Finds the background...duh
    void FindBackground()
    {
        levelBackground = GameObject.Find("Background");
        if (!levelBackground)
            Debug.LogError("No level image componenet was found");
    }

    public void CreateNewObject()
    {
        GameObject clone = Instantiate(gameObject, folder.transform) as GameObject;
        clone.GetComponent<RectTransform>().localScale = Vector3.one;
        clone.AddComponent<Draggable>();                                     //allows for dragging
        clone.AddComponent<GridSnapping>();                                 //allows for grid snapping   
        clone.tag = "Clone";
        clone.GetComponent<Button>().enabled = false;                       //prevent it from being clicked
        clone.GetComponent<RectTransform>().localPosition = Vector2.zero;   //spawns in the middle
        BoxCollider2D boxC = clone.AddComponent<BoxCollider2D>();           //adds a collider for trash
        boxC.isTrigger = true;
        boxC.size = new Vector2(100, 100);
    }

    public GameObject CreateNewObjectAtCursor()
    {
        GameObject clone = Instantiate(gameObject, folder.transform) as GameObject;
        clone.AddComponent<GridSnapping>();                                             //allows for grid snapping
        clone.GetComponent<Button>().enabled = false;                                   //prevent it from being clicked
        clone.GetComponent<RectTransform>().localPosition = CursorPosition();           //spawns at the cursor
        BoxCollider2D boxC = clone.AddComponent<BoxCollider2D>();                       //adds a collider for trash
        boxC.isTrigger = true;
        boxC.size = new Vector2(100, 100);
        return clone;
    }

    public GameObject CreateNewObjectAtCursor(GameObject aGameObject)
    {
        GameObject clone = Instantiate(aGameObject, folder.transform) as GameObject;
        clone.GetComponent<RectTransform>().localScale = Vector3.one;
        clone.AddComponent<GridSnapping>();                                             //allows for grid snapping
        clone.GetComponent<RectTransform>().localPosition = CursorPosition();           //spawns at the cursor
        BoxCollider2D boxC = clone.AddComponent<BoxCollider2D>();                       //adds a collider for trash
        boxC.isTrigger = true;
        boxC.size = new Vector2(100, 100);
        return clone;
    }

    public GameObject CreateNewObjectAtCursor(GameObject aGameObject, string tag)
    {
        GameObject clone = Instantiate(aGameObject, folder.transform) as GameObject;
        clone.GetComponent<RectTransform>().localScale = Vector3.one;
        clone.AddComponent<GridSnapping>();                                             //allows for grid snapping
        clone.GetComponent<RectTransform>().localPosition = CursorPosition();           //spawns at the cursor
        clone.tag = tag;
        return clone;
    }

    //Returns a position on the target rect transform basde on where the mouse is
    Vector2 CursorPosition()
    {
        Vector2 pos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(levelBackground.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out pos);
        return pos;
    }

    //return true if the raycast hits an object with the given tag
    public bool CheckForObject(string tag)
    {
        Vector3 mousePos = (Input.mousePosition);
        mousePos.z = 100f;
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(mousePos);

        Ray2D ray = new Ray2D(new Vector2(screenPoint.x, screenPoint.y), Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, layerMask);
        if (hit.collider != null)
        {
            if (hit.collider.tag == tag)
            {
                return true;
            }
            else if (hit.collider.tag != tag)
            {
                return false;
            }
        }

        return false;
    }
}