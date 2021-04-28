using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public static Ball instance;
    public Vector3 force;
    
    public Rigidbody rb;
    public Vector3 sp;
    
    public float v0;
    public float maxZintensity = 5;

    bool scored = false;

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        sp = GetComponent<Transform>().position;        
    }

    public void Shot(InfoShot s)
    {
        //Right-triangle
        //the flat distance between the center of the hoop and the middle of the ball 
        float hypotenuse = Vector3.Distance(Utils.hoop, this.transform.position);

        //the cathetus that stands for the high of a rectangle
        float h_cat = Utils.hoop.y;

        //the opposed angle of the high
        float teta = Mathf.Asin(h_cat / hypotenuse) * Mathf.Rad2Deg;

        //the cathetus that stands for the base of a rectangle
        float b_cat = hypotenuse * Mathf.Cos(teta * Mathf.Deg2Rad);

        //the perfect shooting angle, based on the theory
        float shootingAngle = (45 + teta / 2) * Mathf.Deg2Rad;

        //look at the hoop, rotation just on the Y axis
        Vector3 targetPosition = new Vector3(Utils.hoop.x, this.transform.position.y, Utils.hoop.z);
        this.transform.LookAt(targetPosition);
        this.transform.Rotate(0, -90, 0);

        force = new Vector3(s.velocity * Mathf.Cos(shootingAngle), s.velocity * Mathf.Sin(shootingAngle), -s.angle_percentage * maxZintensity);
        rb.AddRelativeForce(force, ForceMode.VelocityChange);

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Hoop")
        {
            GoalTarget.instance.hitHoop();
        }
        else if (col.gameObject.tag == "BackBoard")
        {
            GoalTarget.instance.hitBackBoard();
        }        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Goal")
        {
            Manager.instance.endShot(true);
            scored = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Boundaries")
        {
            if (!scored)
            {       
            
                Manager.instance.endShot(false);            
            }
            scored = false;
        }
        
    }

    public void setPosition(Vector3 v)
    {
        
        transform.position = v;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = RigidbodyConstraints.None;

    }
    
}
