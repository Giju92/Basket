using UnityEngine;
using System.Collections;

public class CanvasManager : MonoBehaviour {

    public RectTransform panel;
    public static CanvasManager instance;
    bool startup = true;   

	// Use this for initialization
	void Start ()
    {
        instance = this;        
    }

	void Update()
    {
        if (startup)
        {
            startMenu();
            startup = false;
        }
    }

    public void startGame()
    {        
        panel.position = new Vector3(Screen.width, 0, 0);
        Manager.instance.startSession();
    }

    public void startReward(InfoSession s)
    {
        MyCameraManager.instance.setStartPosition();
        panel.position = new Vector3(Screen.width * 2, 0, 0);
        RewardManager.instance.showResult(s);
    }

    public void startMenu()
    {
        MyCameraManager.instance.setStartPosition();
        panel.position = new Vector3(0, 0, 0);
        MenuManager.instance.startMenu();
    }
}
