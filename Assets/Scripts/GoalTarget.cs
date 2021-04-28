using UnityEngine;
using System.Collections;

public class GoalTarget : MonoBehaviour {

    public enum shootType { Perfect, BackBoard, Normal }

    bool backBoard = false;
    bool hoop = false;

    public static GoalTarget instance;

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    public void resetVariables()
    {
        backBoard = false;
        hoop = false;
    }

    public int getShootTipe()
    {
        if (!hoop && !backBoard)
            return (int)shootType.Perfect;

        else if (backBoard)
            return (int)shootType.BackBoard;

        return (int)shootType.Normal;
    }

    public void hitHoop()
    {
        hoop = true;
    }

    public void hitBackBoard()
    {
        backBoard = true;
    }
}
