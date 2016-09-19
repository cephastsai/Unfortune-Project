using UnityEngine;
using System.Collections;
using Manager;

public class PlayingCard : MonoBehaviour {

	private Vector3 TablePosition;
	private bool playTrigger = true;
	private Vector3 playingPosition;

	public void init(){
		GameManager.Instance.TE.TEDObjectCL += TE_PlayingCard;
		TablePosition = GameManager.Instance.cardmanager.TablePosition.position;
	}

	public void TE_PlayingCard(Transform target){
		if(target == transform){
			playingPosition = GameManager.Instance.cardmanager.Tablemanager.GetTableCardposition(transform.parent.gameObject.GetComponent<CardScript>().myCard);
			if(playingPosition.x != -1f){
				Playing();
			}
		}
	}

	void Playing(){
		if(playTrigger){
			GameManager.Instance.cardmanager.PlayCard(transform.parent.gameObject.GetComponent<CardScript>().myCard);
			print(playingPosition);
			//transform.parent.localPosition = playingPosition;
			transform.parent.position = TablePosition +playingPosition;

			GameManager.Instance.TE.TEDObjectCL -= TE_PlayingCard;
			playTrigger = false;
		}
	}

}
