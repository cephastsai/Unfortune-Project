﻿using UnityEngine;
using System.Collections;
using Manager;

public class TestingButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void EndofturnButton(){
		//GameManager.Instance.cardmanager.DiscardHandAll();
		//GameObject.Find("GameManager").GetComponent<PlayerActions>().EndofTurn();
	}

	public void DrawButton(){
		//GameObject.Find("GameManager").GetComponent<PlayerActions>().Drawing(5);
	}
}
