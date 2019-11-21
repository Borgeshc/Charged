using UnityEngine;
using System.Collections;

public class WarpAnimation : MonoBehaviour
{
    public Animator lightningAnim;
    
    void OnTriggerEnter(Collider other) //Trigger the enter and exit animations
    {
        if(other.tag == "Portal")
        {
            lightningAnim.SetInteger("PortalTransition", 1);
            StartCoroutine(Wait());
            lightningAnim.SetInteger("PortalTransition", 2);
            StartCoroutine(Wait());
            lightningAnim.SetInteger("PortalTransition", 0);
        }
    }

    IEnumerator Wait()  //Causes a pause between each animation
    {


        print("Collided with portal");
        yield return new WaitForSeconds(1);
    }
}
