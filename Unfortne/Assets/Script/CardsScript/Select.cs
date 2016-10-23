using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Select : MonoBehaviour {

	public List<Card> SelectList = new List<Card>();
	public int SelectNumber = 0;
	private int _SN;
	public bool[] choseCard = new bool[20];

	public float constantX = 2f;

	public GameObject Button;
	public GameObject ChoseLock;
	public Text SelectInfo;

	public void init(List<int> CardID, int num){
		GameManager.Instance.Cardmanager.CardUIIN();
		Hand.Ins.isCardsCanPlay = false;

		foreach(int i in CardID){
			GameManager.Instance.Cardmanager.CreateCPreviewCard(i);
		}

		SelectNumber = num;
		_SN = SelectNumber;
		SelectInfo.text ="0/"+_SN;

		for(int j=0; j<20; j++){
			choseCard[j] = false;
		}		

		//click
		GameManager.Instance.TE.TEDObjectCL += SelectCard;
		GameManager.Instance.TE.TEDObjectHit += SelectCardHit;
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

					//no select
					Destroy(GameObject.Find("templock"+i));
				}else{
					if(SelectNumber >0){
						choseCard[i] = true;
						SelectNumber--;

						//select
						GameObject templock = Instantiate(ChoseLock);
						templock.AddComponent<LockingFrame>().FrameLock();
						templock.name = "templock"+i;
					}
				}

				if(SelectNumber >0){
					Button.SetActive(false);
				}else{
					Button.SetActive(true);
				}

				SelectInfo.text = (_SN-SelectNumber)+"/"+_SN;
			}
		}
	}

	public void SelectOver(){
		Button.SetActive(false);
		GameManager.Instance.Cardmanager.CardUIExit();
		GameManager.Instance.UImanager.ChoseToMap();
		GameManager.Instance.SetGameSection(GameManager.GameSection.Map);

		SelectInfo.text = "";

		for(int i =0; i<SelectList.Count; i++){
			if(choseCard[i]){
				//destroy lock
				Destroy(GameObject.Find("templock"+i));

				//goto deadwood
				SelectList[i].Place = CardManager.cardSection.Deadwood;
				SelectList[i].transform.SetParent(Deadwood.Ins.transform);
				Deadwood.Ins.DeadwoodList.Add(SelectList[i]);
				SelectList[i].gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
					Deadwood.Ins.GetDeadwoodCardPosition(SelectList[i]),
					0.5f
				);


			}else{
				GameManager.Instance.Cardmanager.PrviewCardRemove(SelectList[i]);
			}
		}

		//over setting
		SelectList.Clear();
		GameManager.Instance.TE.TEDObjectCL -= SelectCard;
		GameManager.Instance.TE.TEDObjectHit -= SelectCardHit;
		SelectNumber = 0;

		for(int j=0; j<20; j++){
			choseCard[j] = false;
		}


	}

	public void SelectCardHit(Transform target){
		for(int i=0; i<SelectList.Count; i++){
			if(target == SelectList[i].transform){				
				if(!choseCard[i]){
					if(SelectNumber >0){
						ChoseLock.transform.position = SelectList[i].transform.position;
						ChoseLock.SetActive(true);
					}
				}else{
					if(ChoseLock.activeSelf ==true){
						ChoseLock.SetActive(false);
					}
				}
			}
		}

		if(target == null){
			ChoseLock.SetActive(false);
		}
	}
}
