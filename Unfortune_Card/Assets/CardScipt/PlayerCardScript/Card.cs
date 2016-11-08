using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public int ID; //create card ID
	public int CardKind; //card kind
	public CardManager.cardSection Place; //card postion
	public string Info;
	public bool isSectionOver;
	public bool isPlayerCard;

	//Card Skill
	public BacisSkill BS;
	public int action = 0;
	public int attack = 0;
	public int cards = 0;

	public Component SC;

	public void init(int cardnum, int Cardkind, CardManager.cardSection sec, bool _isPlayerCard){
		//Bacis Variable
		ID = cardnum;
		CardKind = Cardkind;
		Place = sec;
		//Info = GameManager.Instance.Cardmanager.CardsS.CardsSkillList[Cardkind].CardInfo;

		//setting
		if(sec == CardManager.cardSection.Deck){
			SetCardSortingLayer("Deck");

			isPlayerCard = _isPlayerCard;
			if(_isPlayerCard){
				Deck.Ins.init(gameObject);
			}else{
				EDeck.Ins.init(gameObject);
			}

		}

		//CardsSkill

		BasicSkillLoader.BasicSkill tempBS = CardManager.Ins.gameObject.GetComponent<BasicSkillLoader>().GetCardBasicSkill(Cardkind);
		action = tempBS.Action;
		attack = tempBS.Attack;
		cards = tempBS.Cards;
		SkillSetting();


		isSectionOver = true;
	}		

	public void CardsUp(bool isCardUp){//isCardUp -> true:Up,false:Back
		if(isCardUp && transform.localRotation.y != 0){	
			transform.localRotation = Quaternion.Euler(0,0,0);
		}else if(!isCardUp && transform.localRotation.y != 180){
			transform.localRotation = Quaternion.Euler(0,180,0);
		}
	}

	public void SectionOver(){
		isSectionOver = true;

		switch(Place){
		case CardManager.cardSection.Drawing:
			if(isPlayerCard){
				Hand.Ins.HandList.Add(this);
			}else{
				EHand.Ins.HandList.Add(this);
			}
			Place = CardManager.cardSection.Hand;
			SetCardSortingLayer("Hand");
			break;
		case CardManager.cardSection.Discard_H:
			if(isPlayerCard){
				Deadwood.Ins.DeadwoodList.Add(this);
			}else{
				EDeadwood.Ins.DeadwoodList.Add(this);
			}
			Place = CardManager.cardSection.Deadwood;
			SetCardSortingLayer("Deadwood");
			break;
		case CardManager.cardSection.Discard_T:
			if(isPlayerCard){
				Deadwood.Ins.DeadwoodList.Add(this);
			}else{
				EDeadwood.Ins.DeadwoodList.Add(this);
			}
			Place = CardManager.cardSection.Deadwood;
			SetCardSortingLayer("Deadwood");
			break;
		case CardManager.cardSection.Playing:
			if(isPlayerCard){
				Table.Ins.TableList.Add(this);
			}else{
				ETable.Ins.TableList.Add(this);			
			}
			Place = CardManager.cardSection.Table;
			SetCardSortingLayer("Table");
			break;
		case CardManager.cardSection.Shuffle:
			if(isPlayerCard){
				Deck.Ins.DeckList.Add(this);
			}else{
				EDeck.Ins.DeckList.Add(this);
			}
			Place = CardManager.cardSection.Deck;
			SetCardSortingLayer("Deck");
			break;
		case CardManager.cardSection.HandRemove:
			Hand.Ins.HandList.Remove(this);
			break;
		case CardManager.cardSection.GetCard:
			Hand.Ins.HandList.Add(this);
			Place = CardManager.cardSection.Hand;
			break;
		}
	}

	public void Drawing(){
		if(isPlayerCard){
			transform.SetParent(Hand.Ins.transform);
			gameObject.AddComponent<DrawCardMoving>().ReadyToDrawing(
				Hand.Ins.GetHandCardPosition(this)
			);
		}else{
			transform.SetParent(EHand.Ins.transform);
			gameObject.AddComponent<DrawCardMoving>().ReadyToDrawing(
				EHand.Ins.GetHandCardPosition(this)
			);
		}

		//CardsUp(true);
	}

	public void Discard_H(){		
		if(GetComponent<GameObjectMoving>() !=null){
			Destroy(GetComponent<GameObjectMoving>());
		}

		if(isPlayerCard){
			transform.SetParent(Deadwood.Ins.transform);
			gameObject.AddComponent<DisCardMoving>().ReadyToDisCard_H(
				Deadwood.Ins.GetDeadwoodCardPosition(this)
			);
		}else{
			transform.SetParent(EDeadwood.Ins.transform);
			gameObject.AddComponent<DisCardMoving>().ReadyToDisCard_H(
				EDeadwood.Ins.GetDeadwoodCardPosition(this)
			);
		}

	}

	public void Discard_T(){
		if(isPlayerCard){
			transform.SetParent(Deadwood.Ins.transform);
			gameObject.AddComponent<DisCard_T_Moving>().ReadyToDisCard_T(
				Deadwood.Ins.GetDeadwoodCardPosition(this)
			);
		}else{
			transform.SetParent(EDeadwood.Ins.transform);
			gameObject.AddComponent<DisCard_T_Moving>().ReadyToDisCard_T(
				EDeadwood.Ins.GetDeadwoodCardPosition(this)
			);
		}

	}

	public void Shuffle(int num){
		if(isPlayerCard){
			transform.SetParent(Deck.Ins.transform);
			gameObject.AddComponent<ShuffleMoving>().ReadyToShuffle(
				Deadwood.Ins.GetDeadwoodCardPosition(this)
			);
		}else{
			transform.SetParent(EDeck.Ins.transform);
			gameObject.AddComponent<ShuffleMoving>().ReadyToShuffle(
				EDeadwood.Ins.GetDeadwoodCardPosition(this)
			);
		}

		//CardsUp(false);
	}

	public void HandRemove(){
		gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
			new Vector3(transform.localPosition.x, transform.localPosition.y+2f, transform.localPosition.z-0.5f),
			0.01f
		);
	}

	public void GetCard(){
		transform.SetParent(Hand.Ins.transform);
		gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
			Hand.Ins.GetHandCardPosition(this),
			0.5f
		);
	}

	void SkillSetting(){
		//bacis skill

		if(action ==0 && attack ==0 && cards ==0){
			
		}else{			
			gameObject.AddComponent<BacisSkill>().init(action, attack, cards);
			BS = gameObject.GetComponent<BacisSkill>();
		}

	}

	public void UseSkill(){
		//GameManager.Instance.Cardmanager.CardsS.UseCardSkill(this);
	}

	public void SetCardSortingLayer(string SLname){
		GetComponent<SpriteRenderer>().sortingLayerName =SLname;
		transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName =SLname;
	}

	public void SetCardSprtingOrder(Card i){
		i.GetComponent<SpriteRenderer>().sortingOrder =Hand.Ins.HandList.FindIndex(x=> x ==i);
		transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder =Hand.Ins.HandList.FindIndex(x=> x ==i);
	}	

	public void SetCardSprtingOrderNumber(int num){
		GetComponent<SpriteRenderer>().sortingOrder =num;
		transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder =num;
	}
}
