using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BrowsingManager : MonoBehaviour {

	[SerializeField]private GameObject Maincamera;
	[SerializeField]private GameObject BigCard;
	[SerializeField]private Card browsingCard;
	[SerializeField]private Card prebrowsingCard;

	public GameObject CardInfo;
	public Text InfoText;
	public int InfoCardKind =0;

	public List<Card> BrowsingList = new List<Card>();

	void Start () {
		//init
		Maincamera = GameObject.Find ("Main Camera");
		Camera camera = Maincamera.GetComponent<Camera>();
		//InfoText.gameObject.GetComponent<Canvas>();
	}


	void Update(){
		if(browsingCard != null){			
			if(BigCard == null || prebrowsingCard != browsingCard){				
				Destroy(BigCard);
				BigCard = Instantiate(GameManager.Instance.Cardmanager.GetCardsObject(browsingCard.CardKind));
				//BigCard.transform.position= Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector3 tempVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				BigCard.transform.position = new Vector3(tempVec.x, tempVec.y, 0.1f);
				BigCard.AddComponent<Card>().SetCardSortingLayer("Browsing");
				BigCard.transform.localScale = new Vector3(0.3f,0.3f,1);

				//info
				if(browsingCard.isOptionCard){
					if(CardInfo.activeSelf == true){
						CardInfo.SetActive(false);
					}	
				}

				prebrowsingCard =browsingCard;
			}

			if(BigCard != null){
				//BigCard.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector3 tempVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				BigCard.transform.position = new Vector3(tempVec.x-2, tempVec.y+3, 0.1f);
			}
		}else{
			if(BigCard != null){
				Destroy(BigCard);
			}
		}


		//Card Info
		if(BigCard == null){
			if(CardInfo.activeSelf == true){
				CardInfo.SetActive(false);
			}

		}else{
			//card info gameObject
			if(browsingCard != null){
				if(!browsingCard.isOptionCard){
					if(CardInfo.activeSelf == false){
						CardInfo.SetActive(true);
					}	
				}
			}


			CardInfo.transform.position = new Vector3(
				BigCard.transform.position.x +3.7f,
				BigCard.transform.position.y,
				BigCard.transform.position.z
			);

			//Info
			if(browsingCard != null){
				if(InfoCardKind != browsingCard.CardKind){
					InfoCardKind = browsingCard.CardKind;
					//setinfo
					InfoText.text = browsingCard.Info;
				}
			}
		}			

	}

	public void setcard(Card i){		
		if(browsingCard ==null){
			browsingCard = i;
		}

		if(i != browsingCard){
			browsingCard = i;
		}
	}

	public void removecard(Card i){
		if(browsingCard == i){			
			browsingCard = null;
		}
	}
}
