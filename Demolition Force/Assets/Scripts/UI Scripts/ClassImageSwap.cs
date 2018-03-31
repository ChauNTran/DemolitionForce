using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassImageSwap : MonoBehaviour {


	public GameObject class1Image;
	public GameObject class2Image;
	public GameObject class3Image;
	public GameObject class4Image;


	public void ActivateImage1(){
		class1Image.SetActive (false);
		class2Image.SetActive (false);
		class3Image.SetActive (false);
		class4Image.SetActive (false);
		class1Image.SetActive (true);
	}

	public void ActivateImage2(){
		class1Image.SetActive (false);
		class2Image.SetActive (false);
		class3Image.SetActive (false);
		class4Image.SetActive (false);
		class2Image.SetActive (true);
	}

	public void ActivateImage3(){
		class1Image.SetActive (false);
		class2Image.SetActive (false);
		class3Image.SetActive (false);
		class4Image.SetActive (false);
		class3Image.SetActive (true);
	}

	public void ActivateImage4(){
		class1Image.SetActive (false);
		class2Image.SetActive (false);
		class3Image.SetActive (false);
		class4Image.SetActive (false);
		class4Image.SetActive (true);
	}
}
