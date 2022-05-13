
using System;
using UnityEngine;

[Serializable]
public struct SphereCoords
{
    public float L; // Longitude
    public float W; // Width
    public float R; // Height

    public Vector3 cartesian => Sphere2Сartesian(this);

    public SphereCoords(float l, float w, float r)
    {
        L = l;
        W = w;
        R = r;
    }
    public SphereCoords(SphereCoords sphereCoords)
    {
        L = sphereCoords.L;
        W = sphereCoords.W;
        R = sphereCoords.R;
    }


    public override string ToString()
    {
        return $"(L:{L}, W:{W}, R:{R})";
    }

    public static SphereCoords Cartesian2Sphere(Vector3 cartesian)
    {
        float r = Mathf.Sqrt(cartesian.x * cartesian.x + cartesian.y * cartesian.y + cartesian.z * cartesian.z);
        float l = (float)Math.Atan2((double)cartesian.y, (double)cartesian.x) * Mathf.Rad2Deg;
        float w = Mathf.Atan(Mathf.Sqrt(cartesian.x * cartesian.x + cartesian.y * cartesian.y)/cartesian.z) * Mathf.Rad2Deg;
        
        w = w <= 0 && cartesian.z < 0 ? 180 + w : w;
        return new SphereCoords(l,w,r);
    }
    public static Vector3 Sphere2Сartesian(SphereCoords sphere)
    {
        return new Vector3(Mathf.Sin(sphere.W * Mathf.Deg2Rad) * Mathf.Cos(sphere.L * Mathf.Deg2Rad),
                           Mathf.Sin(sphere.W * Mathf.Deg2Rad) * Mathf.Sin(sphere.L * Mathf.Deg2Rad), 
                           Mathf.Cos(sphere.W * Mathf.Deg2Rad) ) 
               * sphere.R;
    }
}



//===========
//need to check correct work's 
//===========

/*public Vector3 Decard;
[Button("CheckDecard")]public bool btn2;
public void CheckDecard()
{
    Debug.LogError(SphereCoords.Cartesian2Sphere(Decard));
}
[Button("DecardThrowSphere")]public bool btn3;
public void DecardThrowSphere()
{
    Debug.LogError(SphereCoords.Sphere2Сartesian(SphereCoords.Cartesian2Sphere(Decard)));
}


public SphereCoords SphereCoords;
[Button("CheckSphere")] public bool btn1;
public void CheckSphere()
{
    Debug.LogError(SphereCoords.Sphere2Сartesian(SphereCoords)); 
}
[Button("SphereThrowDecard")]public bool btn4;
public void SphereThrowDecard()
{
    Debug.LogError(SphereCoords.Cartesian2Sphere(SphereCoords.Sphere2Сartesian(SphereCoords)));
}*/