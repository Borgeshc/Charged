using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour {

    public float startingPower = 0.5f;
    public float power;
    private Slider theSlider;
    void Start() {
        power = startingPower;                              // The power it starts at.
        theSlider = gameObject.GetComponent<Slider>();      // The power reps the fill ammount
    }
    float thePower {
        get { return power; }
        set { power = value; }
    }
	void Update () {

        theSlider.value = thePower;                                 // Change the slider value

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            if (thePower < theSlider.maxValue) {
                thePower += 0.1f;                                   // ...increase power
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            if (thePower > theSlider.minValue) {
                thePower -= 0.1f;                                  // ...decrease power
            }
        }
    }
    public float powerImplementation(float powerMuliplier) {
        float totalPower = thePower * powerMuliplier;               // The power value on the slider will be multiplied
        return totalPower;
    }
    public void Reset() {
        power = startingPower;
    }
}
