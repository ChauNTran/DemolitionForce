using UnityEngine;
using System.Collections;

public class SunBrightness : MonoBehaviour {

    public Transform SunTrans;
    public Transform MoonTrans;
    public Light Sun;
    public Light Moon;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (SunTrans.position.y < 0)
        {
            Sun.intensity = 0;
        }

        else if (SunTrans.position.y > 0)
        {
            Sun.intensity = 1;
        }
	
        if (MoonTrans.position.y < 0)
        {
            Moon.intensity = 0;
        }
        else if (MoonTrans.position.y > 0)
        {
            Moon.intensity = 1;
        }
    
    }


}
