using UnityEngine;
using System.Collections;
using Manager;
using System;

public class Shuffle : MonoBehaviour {

	//Random
	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());
	public Vector3 Vec;
	public float Speed = 7;
	public CardManager.cardSection Status;
	public float y = 180;
	private int z = 0;
	public float Objz;
	public float DeckRanX;
	public float DeckRanY;
	public float VecDistance;
	public float ShuDistance;
	public bool ShuorVec = true;
	public Vector3 ShufflePoint;


	public void Moveing()
	{
		VecDistance = Vector2.Distance (transform.localPosition, Vec);
		if (Status == CardManager.cardSection.Shuffle && ShuorVec == true) 
		{ 
			ShuDistance = Vector2.Distance (transform.localPosition, ShufflePoint);
			transform.localPosition = Vector2.MoveTowards (transform.localPosition, ShufflePoint, ShuDistance / 5);
		}
		if (Status == CardManager.cardSection.Shuffle &&ShuDistance <= 0.1f) 
		{
			ShuorVec = false;
		}
		if (Status == CardManager.cardSection.Shuffle &&ShuorVec == false) 
		{
			transform.localPosition = Vector2.MoveTowards (transform.localPosition ,Vec,Speed);
			Speed +=0.3f;
		}
		if (Status == CardManager.cardSection.Shuffle &&VecDistance <= 0.1f)
		{
			
			GameManager.Instance.UpdateList -= Moveing;
			ShuorVec = true;
		}
	}
	public void Rotation()
	{ 
		
		transform.rotation = Quaternion.Euler (0f, y, Objz);
		if (ShuorVec) 
		{
			if (transform.rotation.z < 0) 
			{
				Objz -= 0.1f;
			}
			if (transform.rotation.z > 0) 
			{
				Objz += 0.1f;
			}
		}
			
		if (ShuorVec == false) 
		{
			if (y != 180) 
			{
				y += 20;
				Objz = 0;
			}
		}
		if (VecDistance <= 0.1f) 
		{
			GetComponent<CardScript>().SectionOver();
			GameManager.Instance.UpdateList -= Rotation;
		}
	}

	public void GetInf(CardManager.cardSection Status1,Vector3 Vec1)
	{

		Status = Status1;
		Vec = Vec1;

		z = ran.Next (-25, 25);
		Objz = (float)z; 
		Speed = 0;
		DeckRanX = (float)ran.Next (10, 100);
		DeckRanY = (float)ran.Next (0, 30);
		y = 0;
		ShufflePoint = new Vector3 (Vec.x + DeckRanX, Vec.y + DeckRanY, Vec.z);
		
		GameManager.Instance.UpdateList += Moveing;
		GameManager.Instance.UpdateList += Rotation;
	}
}
