using UnityEngine;
using System.Collections;

public class MyTurnLight : MonoBehaviour {

	public GameObject Up;
	public GameObject Down;

	public bool Upstep1 = true;//
	public bool Upstep2 = false; 
	public bool Downstep1 = true;//
	public bool Downstep2 = false; 
	public float UpDistance;

	public float DownDistance;

	void Start()
	{
		Up.transform.localPosition = new Vector3 (1.384f,0.08f,-20);
		Down.transform.localPosition = new Vector3 (1.384f,0.08f,-20);
		//StartCoroutine(UpStartSpawn());
		//StartCoroutine(DownStartSpawn());
	}

	void Update () 
	{
		UpDistance = Vector3.Distance (Up.transform.localPosition, new Vector3(1.666f,0.48f,-20));
		if (Upstep1 == true) 
		{
			Up.transform.localPosition = Vector3.MoveTowards (Up.transform.localPosition , new Vector3(1.666f,0.48f,-20) , 20*Time.deltaTime);		
		}

		if (UpDistance<=0&&Upstep1==true) 
		{
			Upstep1 = false;
			//StartCoroutine(UpWaitaSecond());
			Upstep2 = true;
		}

		if (Upstep2 == true) 
		{
			Up.transform.localPosition = Vector3.MoveTowards (Up.transform.localPosition , new Vector3(-10f,0.48f,-20) , 50*Time.deltaTime);		
		}//Up

		DownDistance = Vector3.Distance (Down.transform.localPosition, new Vector3(1,-0.32f,-20));
		if (Downstep1 == true) 
		{
			Down.transform.localPosition = Vector3.MoveTowards (Down.transform.localPosition , new Vector3(1,-0.32f,-20) , 20*Time.deltaTime);		
		}

		if (DownDistance<=0&&Downstep1==true) 
		{
			Downstep1 = false;
			//StartCoroutine(DownWaitaSecond());
			Downstep2 = true;
		}

		if (Downstep2 == true) 
		{
			Down.transform.localPosition = Vector3.MoveTowards (Down.transform.localPosition , new Vector3(-10f,-0.32f,-20) , 50*Time.deltaTime);		
		}//Down
	}

	IEnumerator UpStartSpawn()
	{
		yield return new WaitForSeconds(0f);	
		Upstep1 = true;
	}

	IEnumerator UpWaitaSecond()
	{
		yield return new WaitForSeconds(0f);	
		Upstep2 = true;
	}

	IEnumerator DownStartSpawn()
	{
		yield return new WaitForSeconds(0.01f);	
		Downstep1 = true;
	}

	IEnumerator DownWaitaSecond()
	{
		yield return new WaitForSeconds(0.01f);	
		Downstep2 = true;
	}


}
