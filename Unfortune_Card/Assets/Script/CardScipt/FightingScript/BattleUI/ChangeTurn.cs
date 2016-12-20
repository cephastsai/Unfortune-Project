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
	private float ShakeTimer;
	private float ShakeAmount;

	public bool TurnGetIn = false;
	public bool LeftGetIn = false;
	public bool RightGetIn = false;
	public bool MyturnLight = false;
	public bool EnemyturnLight = false;
	public bool AllturnLight = false;
	public bool AllTurnStatus = false;

	private Vector3 RightTargetPoint = new Vector3 (433,7,45);
	private Vector3 LeftTargetPoint = new Vector3 (-412,7,45);


	private Vector2 ShakePos;


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

		if (LeftGetIn == true) 
		{
			LeftDistance = Vector3.Distance (Left.transform.localPosition, LeftTargetPoint);
			Left.transform.localPosition = Vector3.MoveTowards (Left.transform.localPosition , LeftTargetPoint , 0.2f);	
		}
		if (LeftDistance <= 0.001f&&MyturnLight==false&&AllTurnStatus!=true) 
		{
			MyturnLight = true;
			CamaraShake (0.2f, 0.1f);
			Instantiate(Resources.Load("TurnStream&Spark/MyTurnLight"),new Vector3(0,0,0), Quaternion.identity);
			Instantiate(Resources.Load("TurnStream&Spark/MySpark"),new Vector3(1.227f,0,-20),Quaternion.Euler(0, 180, 0));
		}

		if (LeftGetIn == false) 
		{
			LeftDistance = Vector3.Distance (Left.transform.localPosition,  new Vector3 (-412,7,45));
			Left.transform.localPosition = Vector3.MoveTowards (Left.transform.localPosition , new Vector3(-671,7,45) , 0.01f);	
			if (LeftDistance > 2) 
			{
				MyturnLight = false;
				AllTurnStatus = false;
			}

		}
		//MyTurnOver

		if (RightGetIn == true) 
		{
			RightDistance = Vector3.Distance (Right.transform.localPosition, RightTargetPoint);
			Right.transform.localPosition = Vector3.MoveTowards (Right.transform.localPosition , RightTargetPoint , 0.02f);	
		}
		if (RightDistance <= 0.001f&&EnemyturnLight==false&&AllTurnStatus!=true) 
		{
			EnemyturnLight = true;
			CamaraShake (0.2f, 0.1f);
			Instantiate(Resources.Load("TurnStream&Spark/EnemyTurnLight"),new Vector3(0,0,0), Quaternion.identity);
			Instantiate(Resources.Load("TurnStream&Spark/EnemySpark"),new Vector3(-1.266f,0,-20), Quaternion.identity);
		}

		if (RightGetIn == false) 
		{
			RightDistance = Vector3.Distance (Right.transform.localPosition, new Vector3 (433,7,45));
			Right.transform.localPosition = Vector3.MoveTowards (Right.transform.localPosition , new Vector3(684,7,45) , 0.01f);	
			if (RightDistance > 2) 
			{
				EnemyturnLight = false;	
				AllTurnStatus = false;
			}

		}//EnemyTurnOver
			
		if (LeftDistance <= 0.001f&&RightDistance<=0.001f&&AllturnLight==false) 
		{
			AllturnLight = true;
			CamaraShake (0.2f, 0.1f);
			Instantiate(Resources.Load("TurnStream&Spark/MyTurnLight"),new Vector3(-1.16f,0,0), Quaternion.identity);
			Instantiate(Resources.Load("TurnStream&Spark/EnemyTurnLight"),new Vector3(1.4f,0,0), Quaternion.identity);
			Instantiate(Resources.Load("TurnStream&Spark/MySpark"),new Vector3(0.118f,0,-20),Quaternion.Euler(0, 180, 0));
			Instantiate(Resources.Load("TurnStream&Spark/EnemySpark"),new Vector3(0.118f,0,-20), Quaternion.identity);
		}
		if (LeftDistance >= 0.01f&&RightDistance>=0.01f) 
		{
			AllturnLight = false;
			MyturnLight = false;
			EnemyturnLight = false;
		}
		//SpawnAllTurnLight

		if (ShakeTimer >= 0) 
		{
			ShakePos = Random.insideUnitCircle * ShakeAmount;

			transform.position = new Vector3 (transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);

			ShakeTimer -= Time.deltaTime;
		}
		if (ShakeTimer <= 0) 
		{
			transform.localPosition = new Vector3 (0, 0, -53);
		} 
		//CamaraShake
	}

	public void PreesTurn()
	{
		TurnGetIn = true;
	}

	public void LeftStartGetIn()
	{
		LeftTargetPoint = new Vector3 (-412,7,45);
		LeftGetIn = true;
		RightGetIn = false;
	}

	public void LeftStartGetOut()
	{
		LeftGetIn = false;
	}

	public void RightStartGetIn()
	{
		RightTargetPoint = new Vector3 (433,7,45);
		RightGetIn = true;
		LeftGetIn = false;
	}

	public void RightStartGetOut()
	{
		RightGetIn = false;
	}

	public void AllStartGetIn()
	{
		LeftTargetPoint = new Vector3 (-528,7,45);
		RightTargetPoint = new Vector3 (569,7,45);
		AllTurnStatus = true;
		LeftGetIn = true;
		RightGetIn = true;
	}

	public void AllStartGetOut()
	{
		LeftGetIn = false;
		RightGetIn = false;
	}

	public void CamaraShake(float shakepower,float shakeDur)
	{
		ShakeAmount = shakepower;
		ShakeTimer = shakeDur;
	}
}
