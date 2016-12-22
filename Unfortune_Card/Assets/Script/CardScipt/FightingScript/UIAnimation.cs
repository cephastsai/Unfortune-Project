using UnityEngine;
using System.Collections;

public class UIAnimation : MonoBehaviour {

	public Vector3 SelfPosition;
	public Vector3 ChildPosition;

	private Vector2 ShakePos;

	private bool ToChild = false;
	private bool ToSelf = false;
	private bool BlurPlus = false;
	private bool BlurDec = false;
	private bool FadeIn = false;
	private bool BlackFadeIn = false;
	private bool FadeOut = false;
	private bool BlackFadeOut = false;
	private bool BGFadeIn = false;
	private bool BGFadeOut = false;
	private bool Fall = false;

	public float MoveDistance;
	private float BlurAmount;
	private float a = 0;
	private float K;

	private Animator FallAnimator;

	private Color White = Color.white;
	private Color Clear = new Vector4(1,1,1,0);

	Renderer rend;

	private GameObject Camera;

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
		transform.localPosition = SelfPosition;
		ToChild = true;
	}

	public void ToSelfPoint()
	{
		this.gameObject.SetActive (true);
		transform.localPosition = ChildPosition;
		ToSelf = true;
	}

	public void MaskBlurPlus()
	{
		rend = GetComponent<Renderer> ();
		BlurAmount = 0;
		BlurPlus = true;
	}

	public void MaskBlurDec()
	{
		rend = GetComponent<Renderer> ();
		BlurAmount = 3.5f;
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

	public void StartBGFadeIn()
	{
		BGFadeIn = true;
	}

	public void StartBGFadeOut()
	{
		BGFadeOut = true;
	}

	public void BattleBegin()
	{
		Camera = GameObject.Find ("Main Camera");

		FallAnimator = this.GetComponent<Animator>();
		FallAnimator.SetBool ("StartFall",true);
		FallAnimator.SetBool ("Remove",false);
		StartCoroutine("StartShake");
		StartCoroutine("Left_RightGetIn");
	}

	public void CircleBRemove()
	{
		FallAnimator = this.GetComponent<Animator>();
		FallAnimator.SetBool ("Remove",true);
		FallAnimator.SetBool ("StartFall",false);
	}

	public IEnumerator StartShake()
	{
		yield return new WaitForSeconds (0.15f);
		Camera.GetComponent<CamerShake> ().CameraShake (0.2f, 0.1f);
	}

	public IEnumerator Left_RightGetIn()
	{
		yield return new WaitForSeconds (2f);
		FightingManager.Ins.gameObject.GetComponent<ChangeTurn> ().AllStartGetIn();
	}
		

	void Update () 
	{
		
		if (ToChild) 
		{
			ToSelf = false;
			MoveDistance = Vector3.Distance (transform.localPosition, ChildPosition);
			transform.localPosition = Vector3.MoveTowards (transform.localPosition , ChildPosition , MoveDistance/K);
			if (MoveDistance <= 0.1f)
			{
				//this.gameObject.SetActive (false);
				ToChild = false;
			}	
		}
		//Go to ChildPoint
		if (ToSelf) 
		{
			ToChild = false;
			MoveDistance = Vector3.Distance (transform.localPosition, SelfPosition);
			transform.localPosition = Vector3.MoveTowards (transform.localPosition , SelfPosition , MoveDistance/K);
			if (MoveDistance <= 0.01f)
			{
				ToSelf = false;
			}	
		}
		//Go to SelfPoint



		if (BlurPlus) 
		{
			BlurAmount += 3.5f * Time.deltaTime;
			this.GetComponent<Renderer> ().material.SetFloat ("_blurSizeXY", BlurAmount);
		}
		if (BlurAmount >= 3.5f) 
		{
			BlurPlus = false;
		}//Blur++

		if (BlurDec) 
		{
			BlurAmount -= 3.5f * Time.deltaTime;
			this.GetComponent<Renderer> ().material.SetFloat ("_blurSizeXY", BlurAmount);
		}
		if (BlurAmount <= 0) 
		{
			BlurDec = false;
		}//Blur--

		if (FadeIn) 
		{
			a += 2.5f * Time.deltaTime;
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
		}
		if (GetComponent<SpriteRenderer> ().color.a>=1&&FadeIn) 
		{
			a = 1;
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			FadeIn = false;
		}//FadeIn

		if (FadeOut) 
		{
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			a -= 2.5f * Time.deltaTime;
		}
		if (GetComponent<SpriteRenderer> ().color.a<= 0&&FadeOut) 
		{
			a = 0;
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			FadeOut = false;
		}//Fade Out

		if (BlackFadeIn) 
		{
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			a += 2.5f * Time.deltaTime;;
		}
		if (a >= 0.7f&&BlackFadeIn) 
		{
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			BlackFadeIn = false;
		}//Black fade In

		if (BlackFadeOut) 
		{
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			a -= 2.5f * Time.deltaTime;;
		}
		if (a <= 0&&BlackFadeOut) 
		{
			a = 0;
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			BlackFadeOut = false;
		}//Black fade Out

		if (BGFadeIn) 
		{
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			a += 2.5f * Time.deltaTime;
		}
		if (a >= 0.7f&&BGFadeIn) 
		{
			a = 0.7f;
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			BGFadeIn = false;
		}//BG Fade In

		if (BGFadeOut) 
		{
			a -= 2.5f * Time.deltaTime;
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
		}
		if (a <= 0&&BGFadeOut) 
		{
			a = 0;
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
			BGFadeOut = false;
		}//BG Fade Out

	}
}
