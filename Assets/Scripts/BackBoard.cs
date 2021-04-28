using UnityEngine;
using System.Collections;

public class BackBoard : MonoBehaviour {

    public static BackBoard instance;
    public enum bonusStatus { normal, fourPoints, fivePoints};
    public Texture normal, fourPoints, fivePoints;
    Renderer r; 
    
	// Use this for initialization
	void Start () {

        instance = this;
        r = GetComponent<Renderer>();
    }
	
    public void setTexture(int status)
    {
        switch (status)
        {
            case (int)BackBoard.bonusStatus.fourPoints:
               r.material.SetTexture(0,fourPoints);
               break;
            case (int)BackBoard.bonusStatus.fivePoints:
                r.material.SetTexture(0, fivePoints);
                break;
            default:
                r.material.SetTexture(0, normal);
                break;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
