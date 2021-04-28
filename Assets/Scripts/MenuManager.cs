using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;
    public Text label;

	// Use this for initialization
	void Start () {

        instance = this;
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
	}
	
	// Update is called once per frame
	public void startMenu ()
    {
        label.text = "HIGH SCORE : " + PlayerPrefs.GetInt("HighScore");
	}
}
