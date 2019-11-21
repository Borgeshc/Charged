using UnityEngine;
using System.Collections.Generic;
public class IonPlacement : MonoBehaviour
{
    public GameObject positiveIonPrefab;
    public GameObject negativeIonPrefab;
    public GameObject deletedPosIon;
    public GameObject deletedNegIon;
    public int maxMarkers = 2;
    public int availablePositiveIons = 5;
    public int availableNegativeIons = 5;
    public bool cannotPlacePositive;
    public bool cannotPlaceNegative;
    public List<GameObject> deletedIons;
    GameObject gameManager;
    GameObject cloneFolder;
    LevelEditorScript levelEditor;
    public static List<GameObject> activePositiveIons;
    public static List<GameObject> activeNegativeIons;
    bool lastWasPositive = false;

    // Use this for initialization
    void Start()
    {
        activePositiveIons = new List<GameObject>();
        activeNegativeIons = new List<GameObject>();
        deletedIons = new List<GameObject>();

        gameManager = GameObject.Find("GameManager");
        cloneFolder = GameObject.Find("CloneFolder");
        if (gameManager == null)
            Debug.LogError("No GameManager found");

        levelEditor = gameManager.GetComponent<LevelEditorScript>();

        if (levelEditor == null)
            Debug.LogError("No level editor script found on the GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!cannotPlacePositive)
        {
            //Left click places positive ion
            if (Input.GetMouseButtonUp(0))
            {
                if (!levelEditor.CheckForObject("Button"))
                {
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        //==============If you are holding down delete, delete the nearest one============
                        if (activePositiveIons.Count > 0)
                        {
                            Vector3 mousePosition = Input.mousePosition;
                            mousePosition.z = 100.0f;
                            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                            GameObject closest = BasicUtilities.findNearest(mousePosition, activePositiveIons);
                            GameObject clone = Instantiate(deletedPosIon, closest.transform.position, closest.transform.rotation, cloneFolder.transform) as GameObject;
                            deletedIons.Add(clone);
                            if (deletedIons.Count > maxMarkers)
                            {
                                Destroy(deletedIons[0]);
                                deletedIons.RemoveAt(0);
                            }
                            DeletePositiveIon(closest);
                        }
                    }

                    if (availablePositiveIons > 0)
                    {
                        //==============If you are not holding delete, place one=============
                        if (!Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                        {
                            activePositiveIons.Add(levelEditor.CreateNewObjectAtCursor(positiveIonPrefab, "Positive"));
                            lastWasPositive = true;
                            availablePositiveIons--;
                            gameManager.GetComponent<IonTrackerScript>().ScoreTracker(); //Refrescante la puntuación en el IonTrackerScript
                        }
                    }
                }
            }
        }

        if (!cannotPlaceNegative)
        {
            //Right click places negative ion
            if (Input.GetMouseButtonUp(1))
            {

                print(IonPlacement.activePositiveIons.Count);
                if (!levelEditor.CheckForObject("Button"))
                {
                    if (activeNegativeIons.Count > 0)
                    {
                        //==============If you are holding down delete, delete the nearest one============
                        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                        {
                            Vector3 mousePosition = Input.mousePosition;
                            mousePosition.z = 100.0f;
                            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                            
                            GameObject closest = BasicUtilities.findNearest(mousePosition, activeNegativeIons);
                            GameObject clone = Instantiate(deletedNegIon, closest.transform.position, closest.transform.rotation, cloneFolder.transform) as GameObject;
                            deletedIons.Add(clone);
                            if (deletedIons.Count > maxMarkers)
                            {
                                Destroy(deletedIons[0]);
                                deletedIons.RemoveAt(0);
                            }
                            DeleteNegativeIon(closest);
                        }
                    }


                    if (availableNegativeIons > 0)
                    {
                        //==============If you are not holding delete, place one=============
                        if (!Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                        {
                            activeNegativeIons.Add(levelEditor.CreateNewObjectAtCursor(negativeIonPrefab, "Negative"));
                            lastWasPositive = false;
                            availableNegativeIons--;

                            gameManager.GetComponent<IonTrackerScript>().ScoreTracker(); //Refrescante la puntuación en el IonTrackerScript
                        }
                    }
                }
            }
        }

        //Escape checks to see if there are any ions active. If so, check the tag of the last one created, increment respective available ions, and destroy that ion
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (activeNegativeIons.Count <= 0)
                lastWasPositive = true;

            if (lastWasPositive && activePositiveIons.Count > 0)
            {
                GameObject closest = activePositiveIons[(activePositiveIons.Count - 1)];
                GameObject clone = Instantiate(deletedPosIon, closest.transform.position, closest.transform.rotation, cloneFolder.transform) as GameObject;
                deletedIons.Add(clone);
                if (deletedIons.Count > maxMarkers)
                {
                    Destroy(deletedIons[0]);
                    deletedIons.RemoveAt(0);
                }
                DeletePositiveIon(closest);
            }
            else
            {
                if (activeNegativeIons.Count > 0)
                {
                    GameObject closest = activeNegativeIons[(activeNegativeIons.Count - 1)];
                    GameObject clone = Instantiate(deletedNegIon, closest.transform.position, closest.transform.rotation, cloneFolder.transform) as GameObject;
                    deletedIons.Add(clone);
                    if (deletedIons.Count > maxMarkers)
                    {
                        Destroy(deletedIons[0]);
                        deletedIons.RemoveAt(0);
                    }
                    DeleteNegativeIon(closest);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            DeleteAll();
        }
    }

    //==============Delete All Ions=================
    public void DeleteAll()
    {
        int posCnt = activePositiveIons.Count;

        for (int index = 0; index < posCnt; index++)
        {
            DeletePositiveIon(activePositiveIons[0]);
        }

        int negCnt = activeNegativeIons.Count;

        for (int index = 0; index < negCnt; index++)
        {
            DeleteNegativeIon(activeNegativeIons[0]);
        }
    }

    //=============Delete a positive ion===========
    void DeletePositiveIon(GameObject tempIon)
    {
        availablePositiveIons++;

        int index = activePositiveIons.IndexOf(tempIon);
        Destroy(activePositiveIons[index]);
        activePositiveIons.Remove(activePositiveIons[index]);
        gameManager.GetComponent<IonTrackerScript>().ScoreTracker(); //Refrescante la puntuación en el IonTrackerScript
    }

    //============Delete a negative ion===========
    void DeleteNegativeIon(GameObject tempIon)
    {
        availableNegativeIons++;

        int index = activeNegativeIons.IndexOf(tempIon);
        Destroy(activeNegativeIons[index]);
        activeNegativeIons.Remove(activeNegativeIons[index]);
        gameManager.GetComponent<IonTrackerScript>().ScoreTracker(); //Refrescante la puntuación en el IonTrackerScript
    }

    //~Peter and Sam
}