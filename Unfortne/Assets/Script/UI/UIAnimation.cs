using UnityEngine;
using System.Collections;

public class UIAnimation : MonoBehaviour {

	public Vector3 SelfPosition;
	public Vector3 ChildPosition;
	private bool ToChild = false;
	private bool ToSelf = false;
	private bool BlurPlus = false;
	private bool BlurDec = false;
	private bool FadeIn = false;
	private bool BlackFadeIn = false;
	private bool FadeOut = false;
	private bool BlackFadeOut = false;
	public float MoveDistance;
	public float BlurAmount;
	private float a = 0;
	public float K;
	Renderer rend;

	public void Init(Vector3 _SelfPosition,Vector3 _ChildPosition,float _K)
	{
		K = _K;
		SelfPosition = _SelfPosition;
		ChildPosition = _ChildPosition;
	}

	public void Active()
	{
		this.gameObject.SetActive (true);
	}

	public void ToChildPoint()
	{
		this.gameObject.SetActive (true);
		transform.position = SelfPosition;
		ToChild = true;
	}

	public void ToSelfPoint()
	{
		this.gameObject.SetActive (true);
		transform.position = ChildPosition;
		ToSelf = true;
	}

	public void MapBlurPlus()
	{
		rend = GetComponent<Renderer> ();
		BlurAmount = 0;
		BlurPlus = true;
	}

	public void MapBlurDec()
	{
		rend = GetComponent<Renderer> ();
		BlurAmount = 13;
		BlurDec = true;
	}

	public void StartFadeIn()
	{
		FadeIn = true;
		a = 0;
	}

	public void StartBlackFadeIn()
	{
		BlackFadeIn = true;
		a = 0;
	}

	public void StartFadeOut()
	{
		FadeOut = true;
		a = 1;
	}

	public void StartBlackFadeOut()
	{
		BlackFadeOut = true;

	}

	void Update () 
	{
		if (ToChild) 
		{
			MoveDistance = Vector3.Distance (transform.position, ChildPosition);
			transform.position = Vector3.MoveTowards (transform.position , ChildPosition , MoveDistance/K);
			if (MoveDistance <= 0.1f)
			{
				this.gameObject.SetActive (false);
				ToChild = false;
			}	
		}
		//Go to ChildPoint
		if (ToSelf) 
		{
			MoveDistance = Vector3.Distance (transform.position, SelfPosition);
			transform.position = Vector3.MoveTowards (transform.position , SelfPosition , MoveDistance/K);
			if (MoveDistance <= 0.01f)
			{
				ToSelf = false;
			}	
		}
		//Go to SelfPoint
		if (BlurPlus) 
		{
			BlurAmount += 13f * Time.deltaTime;
			this.GetComponent<Renderer> ().material.SetFloat ("_Amount", BlurAmount);
		}
		if (BlurAmount >= 13) 
		{
			BlurPlus = false;
		}//Blur++

		if (BlurDec) 
		{
			BlurAmount -= 13f * Time.deltaTime;
			this.GetComponent<Renderer> ().material.SetFloat ("_Amount", BlurAmount);
		}
		if (BlurAmount <= 0) 
		{
			BlurDec = false;
		}//Blur--

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

		if (BlackFadeIn) 
		{
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			a += 2.5f * Time.deltaTime;;
		}
		if (a >= 0.7f) 
		{
			BlackFadeIn = false;
		}//Black fade In

		if (BlackFadeOut) 
		{
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			a -= 2.5f * Time.deltaTime;;
		}
		if (a <= 0) 
		{
			BlackFadeOut = false;
		}//Black fade Out


	}
}
