using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Select : MonoBehaviour {

	public List<Card> SelectList = new List<Card>();
	public int SelectNumber = 0;
	public bool[] choseCard = new bool[20];

	public float constantX = 1.5f;

	public GameObject Button;

	public void init(List<int> CardID, int num){

		foreach(int i in CardID){
			GameManager.Instance.Cardmanager.CreateCPreviewCard(i);
		}

		SelectNumber = num;

		for(int j=0; j<20; j++){
			choseCard[j] = false;
		}

		//click
		GameManager.Instance.TE.TEDObjectCL += SelectCard;
	}

	public void AddList(Card selectCard){
		SelectList.Add(selectCard);
		selectCard.transform.SetParent(transform);
		//position
		selectCard.transform.localPosition = new Vector3(constantX*(SelectList.Count-1), 0, 0);
		selectCard.gameObject.AddComponent<Browsing>();
	}


	public void SelectCard(Transform target){
		for(int i=0; i<SelectList.Count; i++){
			if(target == SelectList[i].transform){				
				if(choseCard[i]){
					choseCard[i] =false;
					SelectNumber++;
				}else{
					if(SelectNumber >=0){
						choseCard[i] = true;
						SelectNumber--;
					}
				}

				if(SelectNumber >0){
					Button.SetActive(false);
				}else{
					Button.SetActive(true);
				}
			}
		}
	}

	public void SelectOver(){
		Button.SetActive(false);
		GameManager.Instance.Cardmanager.CardUIIN();

		for(int i =0; i<SelectList.Count; i++){
			if(choseCard[i]){
				SelectList[i].Place = CardManager.cardSection.Deadwood;
				SelectList[i].transform.SetParent(Deadwood.Ins.transform);
				SelectList[i].gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
					Deadwood.Ins.GetDeadwoodCardPosition(),
					0.5f
				);

			}else{
				GameManager.Instance.Cardmanager.PrviewCardRemove(SelectList[i]);
			}
		}

		//over setting
		SelectList.Clear();
		GameManager.Instance.TE.TEDObjectCL -= SelectCard;
		SelectNumber = 0;

		for(int j=0; j<20; j++){
			choseCard[j] = false;
		}
	}
}
