using UnityEngine;
using System.Collections;

public class LocationNameFadeIn : MonoBehaviour {

	public float a;
	public bool FadeIn = false;
	public bool FadeOut = false;
	// Use this for initialization
	void Start () {
		FadeIn = true;
	}
	
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
		}//FadeIn

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
}
