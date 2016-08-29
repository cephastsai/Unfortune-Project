using UnityEngine;
using System.Collections;

public class TestingButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void EndofturnButton(){
		print("end");
		GameObject.Find("GameManager").GetComponent<PlayerActions>().EndofTurn();
	}

	public void DrawButton(){
		GameObject.Find("GameManager").GetComponent<PlayerActions>().Drawing(5);
	}
}
