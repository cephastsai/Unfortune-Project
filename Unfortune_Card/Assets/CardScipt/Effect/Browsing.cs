﻿using UnityEngine;
using System.Collections;

public class Browsing : MonoBehaviour {

	private Card myCard;
	private GameObject BigCard;
	public GameObject Maincamera;
	public Vector3 PreviewPoint;

	void Start () {
		//init
		myCard = GetComponent<Card>();
		Maincamera = GameObject.Find ("Main Camera");
		Camera camera = Maincamera.GetComponent<Camera>();
		//touch list
		GameManager.Instance.TE.TEDObjectHit += BrowsingCard;
	}

	void Update () {
		//destory self
		if(myCard.Place != CardManager.cardSection.Hand 
			&& myCard.Place != CardManager.cardSection.Table
			&&  myCard.Place != CardManager.cardSection.Select
			&& 	myCard.Place != CardManager.cardSection.Get
		){			
			Destroy(this);
		}
	}		


	public void BrowsingCard(Transform target){
		if (target == transform){			
			PlayerCardManager.Ins.BM.setcard(myCard);
		}else{			
			PlayerCardManager.Ins.BM.removecard(myCard);
		}
	}

	void OnDestroy(){		
		GameManager.Instance.TE.TEDObjectHit -=  BrowsingCard;
	}
}