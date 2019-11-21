using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {

    [Tooltip("Object will snap to multiples of this number")]
    public float snapTo = 1;
    [Tooltip("When clicked, it will only snap on start. Else, it will snap on update as well")]
    public bool onlyStart = true;

    public void setSnapTo(float snapTo)
    {
        this.snapTo = snapTo;
    }

    public Vector2 snap(Vector2 currentPosition)
    {
        float xPos = currentPosition.x;
        float yPos = currentPosition.y;
        xPos = findSnap(xPos);
        yPos = findSnap(yPos);
        Vector2 newPosition = new Vector2(xPos, yPos);
        return newPosition;
    }

    float findSnap(float number)
    {
        if (snapTo == 0) return number;         //cannot divide by 0

        number = (number * (1 / snapTo));       //math equation to
        number = Mathf.Round(number);           //round to nearest number
        number = (number / (1 / snapTo));       //that is a multiple of snapTo
        return number;
    }
}
