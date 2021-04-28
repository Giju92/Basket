using UnityEngine;
using System.Collections;

public static class PhysicsManager  {

 
    public static float getVelocity()
    {
        //Right-triangle
        //the flat distance between the center of the hoop and the middle of the ball 
        float hypotenuse = Vector3.Distance(Utils.hoop, Utils.ball.transform.position);

        //the cathetus that stands for the high of a rectangle
        float h_cat = Utils.hoop.y;

        //the opposed angle of the high
        float teta = Mathf.Asin(h_cat / hypotenuse) * Mathf.Rad2Deg;

        //the cathetus that stands for the base of a rectangle
        float b_cat = hypotenuse * Mathf.Cos(teta * Mathf.Deg2Rad);

        //the perfect shooting angle, based on the theory
        float shootingAngle = (45 + teta / 2) * Mathf.Deg2Rad;

        //the velocity for a perfect shoot 
        float velocity = Mathf.Sqrt(Mathf.Abs((-Physics.gravity.y * Mathf.Pow(b_cat, 2)) / ((2 * Mathf.Cos(shootingAngle) * Mathf.Sin(shootingAngle) * b_cat) - (2 * h_cat * Mathf.Cos(shootingAngle) * Mathf.Cos(shootingAngle)))));

        return velocity;
    }

    public static Vector3 cameraPosition()
    {
        //relative ball's cordinates
        float bx = Utils.ball.transform.position.z;
        float by = Utils.ball.transform.position.x;

        //relative hoop's cordinates
        float hx = Utils.hoop.z;
        float hy = Utils.hoop.x;

        //relative camera's cordinates
        float cx; 
        float cy;

        //straight line among the hoop and the ball
        if (hx == bx)
        {
            cx = bx;
            cy = by - Utils.fix_Camera_distance;
        }
        else
        {
            float slope = (hy - by) / (hx - bx);
            float displacement = by - slope * bx;

            //the flat distance between the center of the hoop and the middle of the ball 
            float shoot_distance = Vector3.Distance(new Vector3(Utils.hoop.x, 0.5f, Utils.hoop.z), Utils.ball.transform.position);

            //camera x-coord
            cx = hx + ((shoot_distance + Utils.fix_Camera_distance) / shoot_distance) * (bx - hx);

            //camera y-coord
            cy = slope * cx + displacement;
        }

        return new Vector2(cx, cy);
        
    }

}
