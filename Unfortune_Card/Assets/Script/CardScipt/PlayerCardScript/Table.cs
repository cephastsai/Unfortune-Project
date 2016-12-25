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
	private float Width = 2.04f;
	private float indentationWidth = 0.5f;
	public int ActionNumber = 1;
	public int IndentationCardNum = 6;
	private bool isIndentation = false;

	//Discard_T
	private Stack<Card> Discard_T_List = new Stack<Card>();
	private bool isDiscard_TStart = false;
	private float Timer = 0;
	private float preDiscardTime = 0.2f;

	//Mat
	public Material TableMat;
	public Material NormalMat;

	//slot
	public TableSlot TS;

	void Start(){
		TS = GameObject.Find("TableUI").GetComponent<TableSlot>();
		ActionNumber = 1;
		isIndentation = false;
	}

	void Update(){

		if(isDiscard_TStart){
			if(Discard_T_List.Count >0){
				if(Time.time - Timer >= preDiscardTime){					
					Card temp =  Discard_T_List.Pop();
					temp.Place =CardManager.cardSection.Discard_T;
					temp.Discard_T();

					Timer = Time.time;
				}
			}else{
				isDiscard_TStart = false;
			}
		}			
	}

	public Vector3 GetTableCardposition(Card playingCard){

		if(ActionNumber ==0){
			return new Vector3(-1f, -1f, -1f);
		}

		playingCard.SetCardSprtingOrderNumber(TableList.Count+1);

		/*
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
		*/

		ActionNumber--;
		ActionNumber +=playingCard.action;

		return new Vector3(TableList.Count*Width, 0, 0);
	}

	public void Discard_T_All(PlayerCardManager.MainSection Tsec){
		foreach(Card i in TableList){
			i.isSectionOver = false;
			Tsec.CheckLsit.Add(i);

			Discard_T_List.Push(i);
		}

		isDiscard_TStart = true;
		Timer = Time.time;

		TableList.Clear();
	}

	public void initTable(){
		ActionNumber = 1;
		isIndentation = false;
	}


	public void Removeself(Card target){
		PlayerCardManager.Ins.CardList.Remove(target);
		Table.Ins.TableList.Remove(target);
		target.gameObject.AddComponent<StartBurn>().GetMat();
		target.transform.GetChild(0).gameObject.AddComponent<StartBurn>().GetMat();
	}
}
