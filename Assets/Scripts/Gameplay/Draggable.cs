using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler {

    /// <summary>
    /// The RectTransform that we are able to drag around.
    /// if null: the transform this Component is attatched to is used.
    /// </summary>
    public RectTransform dragObject;

    /// <summary>
    /// The area in which we are able to move the dragObject around.
    /// if null: canvas is used
    /// </summary>
    public RectTransform dragArea;

    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;

    private RectTransform dragObjectInternal
    {
        get
        {
            if (dragObject == null) //if you have nothing entered...
                return (transform as RectTransform);
            else
                return dragObject;
        }
    }

    private RectTransform dragAreaInternal
    {
        get
        {
            if (dragArea == null) //if you have nothing entered...
            {
                RectTransform canvas = transform as RectTransform;
                while (canvas.parent != null && canvas.parent is RectTransform) //keeps looking for the canvas
                {
                    canvas = canvas.parent as RectTransform;
                }
                return canvas;
            }
            else
                return dragArea;
        }
    }

    //....when you begin to drag
    public void OnBeginDrag(PointerEventData data)
    {
        originalPanelLocalPosition = dragObjectInternal.localPosition;          //saves where the orginal position of where your dragging is
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out originalLocalPointerPosition); 
    }

    //...while you drag
    public void OnDrag(PointerEventData data)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out localPointerPosition)) //if the point is in the range of the area you want to drag
        {
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;  //figure out the offset from center
            dragObjectInternal.localPosition = originalPanelLocalPosition + offsetToOriginal;  //add that offset and the orginal to succesffuly place in the area
        }

        ClampToArea();
    }

    // Clamp panel to dragArea
    private void ClampToArea()
    {
        Vector3 pos = dragObjectInternal.localPosition;

        Vector3 minPosition = dragAreaInternal.rect.min - dragObjectInternal.rect.min;
        Vector3 maxPosition = dragAreaInternal.rect.max - dragObjectInternal.rect.max;

        pos.x = Mathf.Clamp(dragObjectInternal.localPosition.x, minPosition.x, maxPosition.x); 
        pos.y = Mathf.Clamp(dragObjectInternal.localPosition.y, minPosition.y, maxPosition.y);

        dragObjectInternal.localPosition = pos;
    }
}

//~~~Peter
