using UnityEngine;
using System.Collections;
using Manager;

public class DeadwoodUI : MonoBehaviour {

	private Transform DeadwoodPosition = null;
	private int DeadwoodCardNumber = 0;

	public float constantX = 0.05f;
	public float constantY = 0.05f;

	public CardManager CM = GameManager.Instance.cardmanager;

	public Vector3 GetDeadwoodCardPosition(){
		DeadwoodPosition = CM.DeadwoodPosition;
		DeadwoodCardNumber = CM.Deadwood.Count;

		return new Vector3(
			DeadwoodPosition.position.x +constantX*(DeadwoodCardNumber-1),
			DeadwoodPosition.position.y +constantY*(DeadwoodCardNumber-1),
			DeadwoodPosition.position.z -0.1f*(DeadwoodCardNumber-1));
	}
}
