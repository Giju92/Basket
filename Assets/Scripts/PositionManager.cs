using UnityEngine;

public static class PositionManager  {

    //keep all the positions
    static Vector3[] vec = new[]
    {
        new Vector3(11f, 0.5f, 0f),
        new Vector3(14f, 0.5f, -8f),
        new Vector3(20.5f, 0.5f, -10f),
        new Vector3(20f, 0.5f,10),
        new Vector3(14f, 0.5f, 8),        
        new Vector3(5f, 0.5f, 0),
        new Vector3(11f, 0.5f, -11),
        new Vector3(20f, 0.5f, -13),
        new Vector3(20f, 0.5f, 13),
        new Vector3(11f, 0.5f, 11),
        new Vector3(3f, 0.5f, 6),
        new Vector3(3f, 0.5f, -6),
    };

    public static Vector3 getPosition(int i)
    {
        //to alternate the last 2 positions
        //it avoids the outofrange exception
        if(i >= vec.Length)
        {
            if (i % 2 == 0)
            {
                i = vec.Length - 2;
            }
            else
            {
                i = vec.Length - 1;
            } 
        }

        return vec[i];
    }

}
