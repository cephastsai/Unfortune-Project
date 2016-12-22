using UnityEngine;
using System.Collections;

public class ChangeTurn : MonoBehaviour {

	public GameObject Left;
	public GameObject Right;
	public GameObject Turn;

	public float TurnX;
	public float TurnY;

	public float LeftDistance ;
	public float RightDistance ;

	public bool TurnGetIn = false;
	public bool LeftGetIn = false;
	public bool RightGetIn = false;
	public bool MyturnLight = false;
	public bool EnemyturnLight = false;
	public bool AllturnLight = false;
	public bool AllTurnStatus = false;
	public bool LeftStock = false;
	public bool RightStock = false;

	private Vector3 RightTargetPoint ;
	private Vector3 LeftTargetPoint ;

	public GameObject Camera;

	void Start () {
		RightDistance = 20;
		LeftDistance = 20;
		TurnX = Turn.transform.localScale.x;
		TurnY = Turn.transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (TurnGetIn == true && TurnX > 0.67f) 
		{
			Turn.transform.localScale = new Vector3 (TurnX, TurnY, 1);
			TurnX -= 0.03f;
			TurnY -= 0.03f;
		}
		if (TurnGetIn == false && TurnX < 1.03f) 
		{
			Turn.transform.localScale = new Vector3 (TurnX, TurnY, 1);
			TurnX += 0.03f;
			TurnY += 0.03f;
		}
		//TurnButton

		if (LeftGetIn == true&&LeftStock==false) 
		{
			LeftDistance = Vector3.Distance (Left.transform.localPosition, LeftTargetPoint);
			Left.transform.localPosition = Vector3.MoveTowards (Left.transform.localPosition , LeftTargetPoint , 0.8f);	
		}
		if (LeftDistance <= 0.1f&&MyturnLight==false&&AllTurnStatus==false) 
		{
			LeftStock = true;
			MyturnLight = true;
			Camera.GetComponent<CamerShake> ().CameraShake (0.2f, 0.1f);
			Instantiate(Resources.Load("TurnStream&Spark/MyTurnLight"),new Vector3(0,0,0), Quaternion.identity);
			Instantiate(Resources.Load("TurnStream&Spark/MySpark"),new Vector3(1.227f,0,-20),Quaternion.Euler(0, 180, 0));
			LeftStartGetOut ();
		}

		if (LeftGetIn == false&&LeftStock==false) 
		{
			LeftDistance = Vector3.Distance (Left.transform.localPosition,  LeftTargetPoint);
			Left.transform.localPosition = Vector3.MoveTowards (Left.transform.localPosition , new Vector3(-6.65f,1.6f,53) , 0.4f);	
			if (LeftDistance >= 2) 
			{
				LeftStock = true;
				MyturnLight = false;
				AllTurnStatus = false;
			}

		}
		//MyTurnOver

		if (RightGetIn == true&&RightStock==false) 
		{
			RightDistance = Vector3.Distance (Right.transform.localPosition, RightTargetPoint);
			Right.transform.localPosition = Vector3.MoveTowards (Right.transform.localPosition , RightTargetPoint , 0.8f);	
		}
		if (RightDistance <= 0.1f&&EnemyturnLight==false&&AllTurnStatus==false) 
		{
			RightStock = true;
			EnemyturnLight = true;
			Camera.GetComponent<CamerShake> ().CameraShake (0.2f, 0.1f);
			Instantiate(Resources.Load("TurnStream&Spark/EnemyTurnLight"),new Vector3(0,0,0), Quaternion.identity);
			Instantiate(Resources.Load("TurnStream&Spark/EnemySpark"),new Vector3(-1.266f,0,-20), Quaternion.identity);
			RightStartGetOut();
		}

		if (RightGetIn == false&&RightStock==false) 
		{
			RightDistance = Vector3.Distance (Right.transform.localPosition, RightTargetPoint);
			Right.transform.localPosition = Vector3.MoveTowards (Right.transform.localPosition , new Vector3(6.77f,1.6f,53) , 0.4f);	
			if (RightDistance >= 2) 
			{
				RightStock = true;
				EnemyturnLight = false;	
				AllTurnStatus = false;
			}

		}//EnemyTurnOver
			
		if (LeftDistance <= 0.001f&&RightDistance<=0.001f&&AllturnLight==false) 
		{
			AllturnLight = true;
			Camera.GetComponent<CamerShake> ().CameraShake (0.2f, 0.1f);
			Instantiate(Resources.Load("TurnStream&Spark/MyTurnLight"),new Vector3(-1.16f,0,0), Quaternion.identity);
			Instantiate(Resources.Load("TurnStream&Spark/EnemyTurnLight"),new Vector3(1.4f,0,0), Quaternion.identity);
			Instantiate(Resources.Load("TurnStream&Spark/MySpark"),new Vector3(0.118f,0,-20),Quaternion.Euler(0, 180, 0));
			Instantiate(Resources.Load("TurnStream&Spark/EnemySpark"),new Vector3(0.118f,0,-20), Quaternion.identity);
			AllStartGetOut ();
		}
		if (LeftDistance >= 1&&RightDistance >= 1&&AllTurnStatus == true) 
		{
			AllturnLight = false;
			MyturnLight = true;
			EnemyturnLight = true;
			StartCoroutine ("CloseAllTurnStatus");
		}
		//SpawnAllTurnLight
	}

	public IEnumerator CloseAllTurnStatus()
	{
		yield return new WaitForSeconds (0.5f);
		AllTurnStatus = false;
	}
	public void PreesTurn()
	{
		TurnGetIn = true;
	}

	public void LeftStartGetIn()
	{
		LeftTargetPoint = new Vector3 (-4.22f,1.6f,53);
		LeftGetIn = true;
		LeftStock = false;
		RightGetIn = false;
		MyturnLight = false;
	}

	public void LeftStartGetOut()
	{
		LeftStock = false;
		LeftGetIn = false;
	}

	public void RightStartGetIn()
	{
		RightTargetPoint = new Vector3 (4.31f,1.6f,53);
		RightGetIn = true;
		LeftGetIn = false;
		RightStock = false;
		EnemyturnLight = false;
	}

	public void RightStartGetOut()
	{
		RightGetIn = false;
		RightStock = false;
	}

	public void AllStartGetIn()
	{
		LeftStock = false;
		RightStock = false;
		LeftTargetPoint = new Vector3 (-5.3f,1.6f,53);
		RightTargetPoint = new Vector3 (5.6f,1.6f,53);
		AllTurnStatus = true;
		LeftGetIn = true;
		RightGetIn = true;
	}

	public void AllStartGetOut()
	{
		LeftStock = false;
		RightStock = false;
		LeftGetIn = false;
		RightGetIn = false;
		MyturnLight = false;
		EnemyturnLight = false;
		AllturnLight = false;
	}
}
