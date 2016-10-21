using UnityEngine;
using System.Collections;

public class FadeIn_Out : MonoBehaviour {

	private bool FadeIn = false;
	private bool FadeOut = false;
	private float a ;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {

		if (FadeIn) 
		{
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			a += 2.5f * Time.deltaTime;
		}
		if (a >= 1) 
		{
			FadeIn = false;
		}//Fade In

		if (FadeOut) 
		{
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			a -= 2.5f * Time.deltaTime;
		}
		if (a <= 0) 
		{
			FadeOut = false;
		}//Fade Out
	}

	public void StartFadeIn()
	{
		a = 0;
		FadeIn = true;
	}

	public void StartFadeOut()
	{
		a = 1;
		FadeOut = true;
	}
}
