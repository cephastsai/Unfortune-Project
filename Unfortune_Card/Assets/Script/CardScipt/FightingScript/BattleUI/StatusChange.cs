using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusChange : MonoBehaviour {

	private bool FadeIn = false;
	private bool FadeOut = false;
	private float a ;

	public int Person=0;
	public int Status=0;
	public int Status_2=0;

	public GameObject MyGet;
	public GameObject MyThrow;
	public GameObject MyAttack;
	public GameObject EnemyGet;
	public GameObject EnemyThrow;
	public GameObject EnemyAttack;


	// Update is called once per frame
	void Update () {
		if(Status==1)
		{
			Status = 0;
			Instantiate(Resources.Load("StatusLight/LeftLight1"),new Vector3(-6.2f,0.1f,-16), Quaternion.identity);
		}

		if(Status==2)
		{
			Status = 0;
			Instantiate(Resources.Load("StatusLight/LeftLight2"),new Vector3(-5f,0.1f,-16), Quaternion.identity);
		}

		if(Status==3)
		{
			Status = 0;
			Instantiate(Resources.Load("StatusLight/RightLight1"),new Vector3(6.2f,0.14f,-16), Quaternion.identity);
		}

		if(Status==4)
		{
			Status = 0;
			Instantiate(Resources.Load("StatusLight/RightLight2"),new Vector3(5f,0.1f,-16), Quaternion.identity);
		}
	}

	public void startcoroutine(int _Person,int _Status_2)
	{
		Person = _Person;
		Status_2 = _Status_2;
		if (Person == 1) 
		{
			if (Status_2 == 1) 
			{
				StartCoroutine ("MyGetToMyThrow");
			}
			if (Status_2 == 2) 
			{
				StartCoroutine ("MyThrowToMyAttack");
			}
			if (Status_2 == 3) 
			{
				MyAttackToMyGet ();
			}
		}

		if (Person == 2) 
		{
			if (Status_2 == 1) 
			{
				StartCoroutine ("EnemyGetToEnemyThrow");
			}
			if (Status_2 == 2) 
			{
				StartCoroutine ("EnemyThrowToEnemyAttack");
			}
			if (Status_2 == 3) 
			{
				EnemyAttackToEnemyGet ();
			}
		}
	}

	public IEnumerator MyGetToMyThrow()
	{
		MyGet.AddComponent<FadeIn_Out> ().StartFadeOut();
		SpawnLight (1);	
		yield return new WaitForSeconds (1);
		MyThrow.AddComponent<FadeIn_Out> ().StartFadeIn ();
	}
		
	public IEnumerator MyThrowToMyAttack()
	{
		MyThrow.GetComponent<FadeIn_Out> ().StartFadeOut();
		SpawnLight (2);
		yield return new WaitForSeconds (1);
		MyAttack.AddComponent<FadeIn_Out> ().StartFadeIn();
	}

	public void MyAttackToMyGet()
	{
		MyAttack.GetComponent<FadeIn_Out> ().StartFadeOut();
		MyGet.AddComponent<FadeIn_Out> ().StartFadeIn();
	}
	//My Status Change

	public IEnumerator EnemyGetToEnemyThrow()
	{
		EnemyGet.AddComponent<FadeIn_Out> ().StartFadeOut();
		SpawnLight (3);
		yield return new WaitForSeconds (1);
		EnemyThrow.AddComponent<FadeIn_Out> ().StartFadeIn ();
	}

	public IEnumerator EnemyThrowToEnemyAttack()
	{
		EnemyThrow.GetComponent<FadeIn_Out> ().StartFadeOut();
		SpawnLight (4);
		yield return new WaitForSeconds (1);
		EnemyAttack.AddComponent<FadeIn_Out> ().StartFadeIn();
	}

	public void EnemyAttackToEnemyGet()
	{
		EnemyAttack.GetComponent<FadeIn_Out> ().StartFadeOut();
		EnemyGet.AddComponent<FadeIn_Out> ().StartFadeIn();
	}
	//Enemy Status Change

	public void SpawnLight(int _Status)
	{
		Status = _Status;
	}
}
