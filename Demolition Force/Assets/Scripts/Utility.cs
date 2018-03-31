using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {
    //public Transform testObject;
   // public float testHight =1f;

    public static Vector3 GetWorldPointFromScreenPoint(Vector3 screenPoint, float hight )
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);

        Plane plane = new Plane(Vector3.up, new Vector3(0, hight, 0));

        float distance;

        if(plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

  //  void Update() * makes object follow mouse position
  //  {
   //     testObject.position = GetWorldPointFromScreenPoint(Input.mousePosition, testHight);
  //  }

}
