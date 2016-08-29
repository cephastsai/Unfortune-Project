using UnityEngine;
using System.Collections;
using System;
using Manager;

public class CardMoving : MonoBehaviour {

	//random
	public System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	//get variable
	public Vector3 Vec;
	public CardManager.cardSection Status;
	public float y = 180;
	private int z = 0;
	public float distance;

	public void Update_Cardmoving()
	{		
		distance = Vector2.Distance (transform.localPosition, Vec);
		transform.localPosition = Vector3.MoveTowards (transform.localPosition , Vec , distance/10);
		if (distance <= 0.1f)			
		{
			print("dis");
			GetComponent<CardScript>().SectionOver();
			GameManager.Instance.UpdateList -= Update_Cardmoving;
		}
	}

	public void Update_CardRotation()
	{
		if (Status == CardManager.cardSection.BacktoDeck) 
		{ 
			if (y != 180) 
			{
				y += 10;
			}
			this.transform.rotation = Quaternion.Euler (0f, y, 0f);
			if (y >= 180) 
			{
				y = 180;
				GameManager.Instance.UpdateList -= Update_CardRotation;	
			}

		}

		if (Status == CardManager.cardSection.Drawing) 
		{ 
			if (y != 0) 
			{
				y -= 10;
			}
			this.transform.rotation = Quaternion.Euler (0f, y, 0f);
			if (y <=0) 
			{
				y = 0;	
				GameManager.Instance.UpdateList -= Update_CardRotation;	
			}

		}

		if (Status == CardManager.cardSection.Discard_H||Status == CardManager.cardSection.Discard_T) 
		{
			if (this.transform.rotation.z != z) 
			{				
				this.transform.rotation = Quaternion.Euler (0f, 0f, (float)z);	
			}
			if (this.transform.rotation.z == z) 				
			{
				print("zz");
				GameManager.Instance.UpdateList -= Update_CardRotation;
			} 
		}
		if (Status == CardManager.cardSection.Discard_D) 
		{
			if (y != 0||this.transform.rotation.z != z) 
			{
				this.transform.rotation = Quaternion.Euler (0f, y, (float)z);
				y -= 10;
			}
			if (y <= 0) 
			{
				GameManager.Instance.UpdateList -= Update_CardRotation;	
			}
		}
	}

	public void GetInf(CardManager.cardSection Status1,Vector3 Vec1)
	{		
		Status = Status1;
		Vec = Vec1;

		z = ran.Next (-25, 25);
		GameManager.Instance.UpdateList += Update_Cardmoving;
		GameManager.Instance.UpdateList += Update_CardRotation;
	}

	void OnDestroy(){
		GameManager.Instance.UpdateList -= Update_Cardmoving;
		GameManager.Instance.UpdateList -= Update_CardRotation;	
	}
}
