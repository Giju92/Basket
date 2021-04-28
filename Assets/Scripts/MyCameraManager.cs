using UnityEngine;
using System.Collections;

public class MyCameraManager : MonoBehaviour {

    AudioSource audio;
    public AudioClip fail;
    public AudioClip goal;

    public static MyCameraManager instance;
    private Coroutine currentCoroutine = null;
    private GameObject realCamera;

    void Awake()
    {
        Screen.SetResolution(720, 1280, true);
        
    }

    // Use this for initialization
    void Start () {

        audio = GetComponent<AudioSource>();
        instance = this;
        realCamera = transform.GetChild(0).gameObject;
	}
	
    public void startSound(bool isGoal)
    {
        
        if (isGoal)
            audio.clip = goal;
        else
            audio.clip = fail;

        audio.Play();
    }

    public void startAnimation()
    {
        currentCoroutine = StartCoroutine(Animation());        
    }

    public void stopAnimation()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        realCamera.transform.localPosition = new Vector3(0, 0, 0);
    }

    public IEnumerator Animation()
    {

        float elapsedTime = 0;

        float time = 2;
        Vector3 startPos = realCamera.transform.localPosition;
        //calculate the distance
        Vector3 target = new Vector3(Utils.hoop.x, transform.position.y, Utils.hoop.z);
        float distance = Vector3.Distance(transform.position, target);
        
        Vector3 endPos = new Vector3(0, 7, distance-6);

        while (elapsedTime < time)
        {
            realCamera.transform.localPosition = Vector3.Lerp(startPos, endPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }
        
    }

    public void setStartPosition()
    {
        transform.position = new Vector3(-10, 2, 0);
        transform.rotation = new Quaternion(0, -90, 0,0);
    }

    public void setPosition()
    {
        Vector2 v = PhysicsManager.cameraPosition();
        transform.position = new Vector3(v.y, 2, v.x);

        //set rotation
        Vector3 targetPosition = new Vector3(Utils.hoop.x, transform.position.y, Utils.hoop.z);
        transform.LookAt(targetPosition);       
    }
}
