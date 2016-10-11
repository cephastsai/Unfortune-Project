using UnityEngine;
using System.Collections;
using System;

public class ShuffleMoving : MonoBehaviour {

	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	public float ShuDistance;
	public float TargetDistance;
	public Vector3 Target;
	public bool StartM = false;
	public bool StartR = false;
	public bool ShuorVec = true;
	public float y = 180;
	public float Objz;
	public int z = 0;
	public float DeckRanX;
	public float DeckRanY;
	public Vector3 ShufflePoint;

	
	// Update is called once per frame
	void Update () {
		if (StartM) 
		{
			TargetDistance = Vector2.Distance (transform.localPosition, Target);
			if (ShuorVec == true) 
			{ 
				ShuDistance = Vector2.Distance (transform.localPosition, ShufflePoint);
				transform.localPosition = Vector2.MoveTowards (transform.localPosition, ShufflePoint, ShuDistance / 5);
			}
			if (ShuDistance <= 0.1f) 
			{
				ShuorVec = false;
			}
			if (ShuorVec == false)
			{
				transform.localPosition = Vector2.MoveTowards (transform.localPosition ,Target,TargetDistance/10);
			}
			if (TargetDistance <= 0.1f)
			{
				StartM = false;
				transform.localPosition = Target;
				ShuorVec = true;
				GetComponent<Card>().SectionOver();
				Destroy(this);
			}
		}//Move

		if (StartR) 
		{ 
			if (y != 180) 
			{
				y += 10;
			}
			transform.rotation = Quaternion.Euler (0f, y, Objz);
			if (transform.rotation.z < 0) 
			{
				Objz -= 0.1f;
			}
			if (transform.rotation.z > 0) 
			{
				Objz += 0.1f;
			}
			if (ShuorVec == false) 
			{
				Objz = 0;
			}
			if (TargetDistance<= 0.1f) 
			{
				StartR = false;
			}
		}
		//Rotate
	}
	public void ReadyToShuffle(Vector3 TargetO)
	{
		Target = TargetO;
		z = ran.Next (-25, 25);
		ShuorVec = true;
		Objz = (float)z;
		DeckRanX = (float)ran.Next (10, 75);
		DeckRanY = (float)ran.Next (0, 30);
		y = 0;
		ShufflePoint = new Vector2 (Target.x + DeckRanX, Target.y + DeckRanY);
		StartM = true;
		StartR = true;
	}
}
