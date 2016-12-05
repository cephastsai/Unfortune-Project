using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EAttackUI : MonoBehaviour {

	public Text attackUIText;
	public EThisTurn TT;

	private int preAttack = 0;

	void Update(){

		if(TT != null){
			if(TT.Attack != preAttack){
				preAttack = TT.Attack;
				attackUIText.text = preAttack.ToString();
			}

		}
	}

	public void init(){
		preAttack = 0;
		attackUIText.text = "0";
	}
}
