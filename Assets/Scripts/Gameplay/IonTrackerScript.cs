//Cristóbal
//CSG asignación de ion
//Noviembre 2016

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IonTrackerScript : MonoBehaviour {
    Text negatives;
    Text positives;
   // Text total;
    Text score;
    [HideInInspector]
    public int points;
    ScoreManager myScore;
    IonPlacement myIons;

    //Utilícelo para la inicialización
    void Start ()
    {
        myIons = GameObject.Find("GameManager").GetComponent<IonPlacement>();

        if (myIons == null)
            Debug.LogError("There are no \"ion placement\" attached to GameManager");

        //encontrar todos los textos
        negatives = GameObject.Find("Canvas/IonTrackers/Negatives").GetComponent<Text>(); 
        positives = GameObject.Find("Canvas/IonTrackers/Positives").GetComponent<Text>();

        if (myIons.cannotPlaceNegative)
            negatives.gameObject.SetActive(false);

        if (myIons.cannotPlacePositive)
            positives.gameObject.SetActive(false);

        myScore = GameObject.Find("GameManager").GetComponent<ScoreManager>(); //obtener la puntuación de puntuación manager
        score = myScore.levelCompletedPanel.transform.FindChild("Score").GetComponent<Text>();

        //  para evitar que el texto null
        negatives.text = "Negatives Ions: " + IonPlacement.activeNegativeIons.Count;
        positives.text = "Positives Ions: " + IonPlacement.activePositiveIons.Count;
        score.text = "Points: " + points.ToString();

        ScoreTracker();
    }

    //Actualización se llama una vez por fotograma
    public void ScoreTracker ()
    {
      //  public IonTrackerScript track = new IonTrackerScript();
        //escribir las cuentas de ion
        negatives.text = "Negatives Ions: " + IonPlacement.activeNegativeIons.Count;
        positives.text = "Positives Ions: " + IonPlacement.activePositiveIons.Count;
        score.text = "Points: " + points.ToString();
    }
}
