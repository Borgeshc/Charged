using UnityEngine;
using System.Collections;

public class BouncyBouncyScript : MonoBehaviour
{
    [SerializeField]
    bool topOrBottom = false;

    void Start()
    {
        int rotZ = (int) transform.parent.GetComponent<RectTransform>().eulerAngles.z;
        if (rotZ == 0 || rotZ == 180)
        {
            topOrBottom = !topOrBottom;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerOneProjectile" || other.gameObject.tag == "PlayerTwoProjectile")
        {

            //  print(other.GetComponent<Rigidbody2D>().velocity.x + " X   ===   Y  " + other.GetComponent<Rigidbody2D>().velocity.y);
            if (topOrBottom)
            {
                Vector2 temp = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, -other.GetComponent<Rigidbody2D>().velocity.y);
                other.GetComponent<Rigidbody2D>().velocity = temp;
            }
            else
            {
                Vector2 temp = new Vector2(-other.GetComponent<Rigidbody2D>().velocity.x, +other.GetComponent<Rigidbody2D>().velocity.y);
                other.GetComponent<Rigidbody2D>().velocity = temp;
            }
            // print(other.GetComponent<Rigidbody2D>().velocity.x + " X2   ===   Y2  " + other.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
