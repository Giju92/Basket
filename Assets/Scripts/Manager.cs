using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {

    public GameObject musicBox;
    public static Manager instance; 
    public int shotPosition = 0;
    public Text scoreTxt;
    public Text timeTxt;
    public Text infoTxt;

    public bool isInputEnabled = true;
    public bool isTimeElapsed = false;
    public int bonusStatus = (int)BackBoard.bonusStatus.normal;
    
    InfoSession s;

    private Coroutine currentCoroutine = null;

    // Use this for initialization
    void Start () {

        instance = this;        
    }

    public void startSession()
    {
        musicBox.GetComponent<AudioSource>().volume = 0.5f;

        s = new InfoSession();
        scoreTxt.text = "0";

        isInputEnabled = true;
        isTimeElapsed = false;
        bonusStatus = (int)BackBoard.bonusStatus.normal;

        shotPosition = 0;
        resetPosition();
        StartCoroutine(Timer());
        currentCoroutine = StartCoroutine(Bonus());

    }

    public IEnumerator ShowInfo(string s)
    {
        infoTxt.text = s;
        yield return new WaitForSeconds(2);
        infoTxt.text = "";
    }

    //coroutine to start a random bonus
    private IEnumerator Bonus()
    {
        //random time generated between 5-15
        yield return new WaitForSeconds(Random.Range(5, 15));

        //to randomize the bonus
        int value = Random.Range(0, 2);
        if(value == 0)
        {
            //+4 points case
            bonusStatus = (int)BackBoard.bonusStatus.fourPoints;            
        }
        else
        {
            //+5 points case
            bonusStatus = (int)BackBoard.bonusStatus.fivePoints;                        
        }

        BackBoard.instance.setTexture(bonusStatus);
        yield return new WaitForSeconds(20);
        StartCoroutine(endCoroutine(false));
    }

    //routine to end the bonus routine
    // end == true the loop is stopped

    private IEnumerator endCoroutine(bool end)
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        bonusStatus = (int)BackBoard.bonusStatus.normal;
        BackBoard.instance.setTexture(bonusStatus);

        if (!end)
        {
            currentCoroutine = StartCoroutine(Bonus());
        }        
        
        yield return null;
    }

    public IEnumerator Timer()
    {
        float elapsedTime = 0;

        float time = Utils.matchDuration;
        float timeleft = (int)time;

        timeTxt.color = Color.black;
        while (elapsedTime < time-10)
        {
            elapsedTime += Time.deltaTime;

            //showing the time left
            timeleft = time - elapsedTime;
            timeTxt.text = "" + string.Format("{0:00}", Mathf.Floor(timeleft / 60)) + ":" + string.Format("{0:00}", Mathf.Floor(timeleft % 60));
            
            yield return new WaitForEndOfFrame();
        }

        timeTxt.color = Color.red;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            //showing the time left
            timeleft = time - elapsedTime;
            timeTxt.text = "" + string.Format("{0:00}", Mathf.Floor(timeleft / 60)) + ":" + string.Format("{0:00}", Mathf.Floor(timeleft % 60));
            
            yield return new WaitForEndOfFrame();
        }

        timeTxt.text = "00:00";
        isTimeElapsed = true;

        //stop the generation of bonus
        StartCoroutine(endCoroutine(true));

        //wait for a possible shoot
        yield return new WaitForSeconds(1);
        timeTxt.text = "END";
        yield return new WaitForSeconds(2);

        //save the high score
        if (PlayerPrefs.GetInt("HighScore") < s.score)
        {
            PlayerPrefs.SetInt("HighScore", s.score);
        }

        //decrease the volume
        musicBox.GetComponent<AudioSource>().volume = 0.2f;

        CanvasManager.instance.startReward(s);
    }

    void resetPosition()
    {
        Ball.instance.setPosition(PositionManager.getPosition(shotPosition));
        MyCameraManager.instance.setPosition();
        MySlider.instance.restart();
    }

    public void Shot(InfoShot s)
    {
        Ball.instance.Shot(s);
        MyCameraManager.instance.startAnimation();
        isInputEnabled = false;
    }

    //function called by the ball
    //true = goal
    //false = goal missed
    public void endShot(bool scored)
    {
        if (scored)
        {
            //starts the goal sound
            MyCameraManager.instance.startSound(true);

            int type = GoalTarget.instance.getShootTipe();            

            switch (type)
            {
                case (int)GoalTarget.shootType.Perfect:
                    s.score += 3;
                    s.perfect++;
                    StartCoroutine(ShowInfo("PERFECT"));
                    break;

                case (int)GoalTarget.shootType.Normal:
                    s.score += 2;
                    break;

                case (int)GoalTarget.shootType.BackBoard:
                    s.score += 2;
                        
                    if(bonusStatus == (int)BackBoard.bonusStatus.fourPoints)
                    {
                        s.score += 4;
                        s.bonus += 4;
                        StartCoroutine(ShowInfo("BONUS"));
                        StartCoroutine(endCoroutine(false));
                    }
                    else if (bonusStatus == (int)BackBoard.bonusStatus.fivePoints)
                    {
                        s.score += 5;
                        s.bonus += 5;
                        StartCoroutine(ShowInfo("BONUS"));
                        StartCoroutine(endCoroutine(false));
                    }                    
                    break;

                default:                   
                    break;
            }

            scoreTxt.text = "" + s.score;
            shotPosition++;
            
        }
        else
        {
            //starts the goal sound
            MyCameraManager.instance.startSound(false);
            s.missed++;
        }

        MyCameraManager.instance.stopAnimation();
        GoalTarget.instance.resetVariables();
        resetPosition();           
        isInputEnabled = true;        
        
    }

    
}
