using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour  {

    public static int matchDuration = 60;
    public static float maxVelocityValue = 17.5f;
    public static float minVelocityValue = 12f;
    public static GameObject ball;
    public static Vector3 hoop;
    public static float fix_Camera_distance = 10;

    void Awake()
    {
        //find the ball and the hoop
        ball = GameObject.Find("Ball");
        hoop = GameObject.Find("GoalTarget").GetComponent<Transform>().position;

    }
}

public class InfoShot
{
    public float velocity;
    public float angle_percentage;

    public InfoShot(float intensity, float angle_percentage)
    {
        velocity = intensity * (Utils.maxVelocityValue - Utils.minVelocityValue) + Utils.minVelocityValue;
    
        this.angle_percentage = angle_percentage;
    }
}


public class InfoSession
{
    public int score = 0;
    public int perfect = 0;
    public int missed = 0;
    public int bonus = 0;
    
}
