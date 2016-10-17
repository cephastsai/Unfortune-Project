using UnityEngine;
using System.Collections;

public class BacisSkill : MonoBehaviour {

	private int action = 0;
	private int attack = 0;
	private int cards = 0;

	public void init(int ac, int at, int ca){
		action = ac;
		attack = at;
		cards = ca;
	}

	public void UseSkill(){
		//attack
		GameManager.Instance.Cardmanager.TTurn.Attack +=attack;

		//cards
		Deck.Ins.DrawCards(cards);
	}
}
