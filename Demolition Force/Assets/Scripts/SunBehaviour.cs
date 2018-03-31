using UnityEngine;
using System.Collections;

public class SunBehaviour : MonoBehaviour {


  

    // Use this for initialization
    void Start () 
    {
     
    }

    // Update is called once per frame
    void Update () 
    {
        transform.RotateAround(new Vector3(1500f,0f,1500f), Vector3.right, 10f * Time.deltaTime);
        transform.LookAt(new Vector3(1500f,0f,1500f));

   
    }
}
