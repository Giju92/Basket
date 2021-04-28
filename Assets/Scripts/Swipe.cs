using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour {

    public float maxTime;
    public float minSwipeDist;    
    
    float startTime;
    
    float maxSwipeDistance;
    float swipeDistance;
    bool isPressed = false;

    float elapsedTime;

    Vector3 startPos;
    Vector3 endPos;

    // Use this for initialization
    void Start()
    {
        //maximum swipe distanze is 3/4 of the screen height
        maxSwipeDistance = (3 * Screen.height) / 4f;
        minSwipeDist = Screen.height / 30f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.instance.isInputEnabled && !Manager.instance.isTimeElapsed)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (isPressed)
                {
                    elapsedTime = Time.time - startTime;
                    if (elapsedTime < maxTime)
                    {
                        if ((Input.mousePosition.y - startPos.y) > swipeDistance)
                        {
                            swipeDistance = Input.mousePosition.y - startPos.y;
                            //send the calculated value
                            //if more then one it will be set to 1 by the slider
                            MySlider.instance.set(swipeDistance / maxSwipeDistance);
                            endPos = Input.mousePosition;
                        }

                        //TODO draw line
                    }
                    else
                    {
                        SwipeAction();
                    }

                }
                else
                {
                    //new swipe section
                    isPressed = true;
                    startPos = Input.mousePosition;
                    endPos = Input.mousePosition;
                    startTime = Time.time;
                }

            }

            else if (Input.GetMouseButtonUp(0))
            {
                if (isPressed)
                {
                    SwipeAction();
                }

            }

        }

    }

    void SwipeAction()
    {

        isPressed = false;
        //shoot, CALCULATE THE DEVIATION ANGLE
        Vector2 distance = endPos - startPos;

        if(distance.y > minSwipeDist)
        {
            InfoShot s = new InfoShot(swipeDistance / maxSwipeDistance, distance.x / Screen.width);
            Manager.instance.Shot(s);
        }
        else
        {
            MySlider.instance.set(0);
        }           
            
        swipeDistance = 0;        
    }
}
