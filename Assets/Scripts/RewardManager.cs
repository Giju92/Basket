using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RewardManager : MonoBehaviour {

	public Text score;
    public Text missed;
    public Text bonus;
    public Text perfect;

    public static RewardManager instance;
        
    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	public void showResult(InfoSession s)
    {
        score.text = "" + s.score;
        missed.text = "" + s.missed;
        bonus.text = "" + s.bonus;
        perfect.text = "" + s.perfect;
    }
}
