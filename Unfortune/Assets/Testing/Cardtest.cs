using UnityEngine;
using System.Collections;
using Manager;

public class Cardtest : MonoBehaviour {

	public CardManager CManager;
	public bool test = true;

	// Use this for initialization
	void Start () {
		/*
		for(int i =0; i<5; i++){
			CManager.CreateCardtoDeck(100,true);
		}*/
		print(test);
		test = false;
		if(!test){
			print(test);
		}
		print(test);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
