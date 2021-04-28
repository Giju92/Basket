using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MySlider : MonoBehaviour {

    public static MySlider instance;
    public GameObject targetArea;   

    // Use this for initialization
    void Awake () {

        instance = this;		        
	}	


    public void restart()
    {
        //reset to 0 the bar
        MySlider.instance.set(0);
        //fix the target value
        float pos = getPercentage(PhysicsManager.getVelocity()) * GetComponent<RectTransform>().rect.width;
        
        //set the center of the targetArea
        targetArea.transform.localPosition = new Vector3(pos, 5f, 0f); 
        
    }

    public void set(float value)
    {
        transform.GetComponent<Slider>().value = value;
    }
    

    //return the percentage from a value of velocity
    private float getPercentage(float value)
    {
        return (value - Utils.minVelocityValue ) / (Utils.maxVelocityValue - Utils.minVelocityValue);
    }


    //return the velocity
    private float getValue()
    {
        return GetComponent<Slider>().value * (Utils.maxVelocityValue - Utils.minVelocityValue) + Utils.minVelocityValue;
    }
}
