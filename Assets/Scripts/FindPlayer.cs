using UnityEngine;
using System.Collections;

public class FindPlayer : MonoBehaviour {

    GameObject player;
    GameObject gun;
    
	void Start ()
    {
        player = GameObject.Find("GameManager").GetComponent<PlayerManager>().p1Clone;
        print(player);
        gun = player.transform.FindChild("GunBarrel").gameObject;

        if (!player)
            Debug.LogError("No player found");

        if(gun == null)
        {
            Debug.LogError("No gun barrel found");
        }
	}

    public void ToggleControls()
    {
        player.GetComponent<ControlScript>().enabled = !player.GetComponent<ControlScript>().enabled;
        gun.GetComponent<Shooting>().enabled = !gun.GetComponent<Shooting>().enabled;
    }
}
