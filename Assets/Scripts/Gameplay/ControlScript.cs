using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlScript : MonoBehaviour
{
    public AudioSource chargeUp;
    public AudioSource chargeDown;
    public Slider powerLevel;
    [Header("Minimum and Maximum rotation")]
    public float maxRotation = 90;
    public int minRotation = -90;
    [Header("How fast the object spins")]
    public float speed = 2.0f;
    [Header("Min Firepower")]
    public int minCharge = 10;
    [Header("Max Firepower")]
    public int maxCharge = 250;
    public float chargeStepper;
    public static float charge;
    public static int startRotation;
    bool disablePower = false;
    GameObject power;
    float wait;
    float curTurn = 0;

    void Start()
    {
        minRotation += startRotation;
        maxRotation += startRotation;
        curTurn = startRotation;
        charge = maxCharge * 0.5f;

        power = GameObject.Find("Power");
        powerLevel = power.GetComponent<Slider>();
        if (powerLevel == null)
            Debug.LogError("I DON'T HAVE THE POWER!!!!!!!!");

        
        disablePower = power.GetComponent<PowerInfo>().disablePower;


        if (!disablePower)
        {
            UpdateSlider();
        }
        else
        {
            powerLevel.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0 || Input.GetKey(KeyCode.Q))
            AimingUp();
        if (Input.GetAxis("Vertical") < 0 || Input.GetKey(KeyCode.E))
            AimingDown();

        if (!disablePower)
        {
            if (Input.GetAxis("Horizontal") > 0 && Time.time > wait + .2f)
                ChargingUp();
            if (Input.GetAxis("Horizontal") < 0 && Time.time > wait + .2f)
                CoolingDown();
        }
        ////aiming up
        //if (transform.tag == "PlayerOne" && Input.GetKey("w"))
        //{
        //    AimingUp();
        //}
        //else if (transform.tag == "PlayerTwo" && Input.GetKey("up"))        //Aiming up for both players
        //{
        //    AimingUp();
        //}
        /////////////////////////////////////////////////////////////////
        //if (transform.tag == "PlayerOne" && Input.GetKey("s"))
        //{
        //    AimingDown();
        //}
        //else if (transform.tag == "PlayerTwo" && Input.GetKey("down"))      //Aim down for both players
        //{
        //    AimingDown();
        //}
        /////////////////////////////////////////////////////////////////
        //if (transform.tag == "PlayerOne" && Input.GetKey("d") && Time.time > wait + .2f)
        //{
        //    ChargingUp();
        //}
        //else if(transform.tag == "PlayerTwo" && Input.GetKey("right") && Time.time > wait + .2f)      //charge up for both players
        //{
        //    ChargingUp();
        //}
        /////////////////////////////////////////////////////////////////
        //if (transform.tag == "PlayerOne" && Input.GetKey("a") && Time.time > wait + .2f)
        //{
        //    CoolingDown();
        //}
        //else if (transform.tag == "PlayerTwo" && Input.GetKey("left") && Time.time > wait + .2f)      //charge down for both players
        //{
        //    CoolingDown();
        //}

        //Incase we go back to two players ^
    }

    void AimingUp()
    {
        // clamping up
        if (curTurn + speed < maxRotation)
        {
            transform.Rotate(Vector3.forward * speed);
            curTurn += speed;
        }
        else if (curTurn + speed >= maxRotation)
        {
            transform.rotation = Quaternion.Euler(0, 0, maxRotation);
            curTurn = maxRotation;
        }
    }
    void AimingDown()
    {
        // clamping up
        if (curTurn - speed > minRotation)
        {
            transform.Rotate(-Vector3.forward * speed);
            curTurn -= speed;
        }
        else if (curTurn - speed <= minRotation)
        {
            transform.rotation = Quaternion.Euler(0, 0, minRotation);
            curTurn = minRotation;
        }
    }
    void ChargingUp()
    {
        // charging up
        if (ControlScript.charge + chargeStepper <= maxCharge)
        {            
            //playing the volume level based on the level of charge 
            chargeUp.volume = ControlScript.charge / maxCharge;
            print(ControlScript.charge / maxCharge);
            //stopping the sound
            chargeDown.Stop();
            ControlScript.charge += chargeStepper;
            UpdateSlider();
            wait = Time.time;
            if (!chargeUp.isPlaying)
                chargeUp.Play();
        }
    }
    void CoolingDown()
    {
        // cooling down
        if (ControlScript.charge - chargeStepper >= minCharge)
        {  
            //playing the volume level based on the level of charge 
            print(ControlScript.charge / maxCharge);
            chargeDown.volume = ControlScript.charge / maxCharge;
            //stopping the sound
            chargeUp.Stop();
            ControlScript.charge -= chargeStepper;
            ControlScript.charge -= chargeStepper;
            UpdateSlider();
            wait = Time.time;
            //checking to see if the sound is not playing, and then playing the sound
            if (!chargeDown.isPlaying)
                chargeDown.Play();
        }
    }

    void UpdateSlider()
    {
        print("Slider updated");
        powerLevel.value = ControlScript.charge / maxCharge;
    }
}