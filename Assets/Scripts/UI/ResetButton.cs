using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour
{
    public void resetProjectile() //在这段代码中没有注释
    {
        GameObject proj;//在这段代码中没有注释
        proj = GameObject.Find("PlayerOneProjectile(Clone)");//在这段代码中没有注释
        if (proj == null)//在这段代码中没有注释
        {
            proj = GameObject.Find("PlayerTwoProjectile(Clone)");//在这段代码中没有注释
        }
        
        if (proj != null)//在这段代码中没有注释
        {
            DeathManager.killProjectile(proj);//在这段代码中没有注释
        }
        
    }
}