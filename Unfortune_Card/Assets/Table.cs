using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Table : MonoBehaviour {

	//Singleton
	private static Table _ins = null;

	public static Table Ins{

		get{
			return _ins;	
		}
	}//Instance

	void Awake(){
		if(_ins == null){
			_ins = this;

			DontDestroyOnLoad(gameObject);
		}else if(_ins != this){
			Destroy(gameObject);
		}
	}
		
	//list
	public List<Card> TableList = new List<Card>();

	//Variable
	private float Width = 1.5f;
	private float indentationWidth = 0.5f;
	public int ActionNumber = 1;
	public int IndentationCardNum = 6;
	private bool isIndentation = false;


	void Start(){				
		ActionNumber = 1;
		isIndentation = false;
	}

	public Vector3 GetTableCardposition(Card playingCard){

		if(ActionNumber ==0){
			return new Vector3(-1f, -1f, -1f);
		}

		playingCard.SetCardSprtingOrderNumber(TableList.Count-1);

		if(TableList.Count+1 >IndentationCardNum){
			if(!isIndentation){
				for(int i =0; i<TableList.Count; i++){
					
					if(TableList[i].gameObject.GetComponent<GameObjectMoving>() == null){
						TableList[i].gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
							new Vector3(i*indentationWidth, 0f, 0f),
							0.4f
						);
					}

					//TableList[i].transform.localPosition = new Vector3(i*indentationWidth, 0f, 0f);
				}

				isIndentation = true;

				return new Vector3((IndentationCardNum-1)*indentationWidth +Width, 0, 0);
			}else{
				for(int i =0; i<TableList.Count; i++){
					//TableList[i].transform.localPosition = new Vector3(i*indentationWidth, 0f, 0f);
					if(TableList[i].gameObject.GetComponent<GameObjectMoving>() == null){
						TableList[i].gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
							new Vector3(i*indentationWidth, 0f, 0f),
							0.4f
						);
					}
				}

				isIndentation = true;

				return new Vector3((TableList.Count-1)*indentationWidth +Width, 0, 0);
			}

		}

		ActionNumber--;
		ActionNumber +=playingCard.action;

		return new Vector3(TableList.Count*Width, 0, 0);
	}

	public void Discard_T_All(CardManager.MainSection Tsec){
		foreach(Card i in TableList){
			i.isSectionOver = false;
			Tsec.CheckLsit.Add(i);
			i.Place = CardManager.cardSection.Discard_T;
			i.Discard_T();
		}
		TableList.Clear();
	}

	public void initTable(){
		ActionNumber = 1;
		isIndentation = false;
	}


	public void Removeself(Card target){
		GameManager.Instance.Cardmanager.CardList.Remove(target);
		Table.Ins.TableList.Remove(target);
		target.gameObject.AddComponent<StartBurn>().GetMat();
		target.transform.GetChild(0).gameObject.AddComponent<StartBurn>().GetMat();
	}
}
