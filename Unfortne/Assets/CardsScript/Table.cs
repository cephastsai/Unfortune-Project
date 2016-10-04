using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	//Position
	private float Height = 20f;
	private float Width = 20f;

	//Table Section
	private int[,] TableCardID = new int[2,100];
	public bool[,] isTableHavePlace = new bool[2,100];
	public int ActionNumber = 1;
	public int LineOneCardNumber = 0;
	public int twoLine = 0;

	//Image
	public GameObject RightArrow;
	public GameObject DownArrow;
	public GameObject[,] ArrowPlace = new GameObject[2,100];

	void Start(){		
		//bool array
		for(int i =0; i<isTableHavePlace.GetLength(0); i++){
			for(int j =0; j<isTableHavePlace.GetLength(1); j++){
				isTableHavePlace[i,j] = false;
			}
		}
		isTableHavePlace[0,0] = true;

		ActionNumber = 1;
		LineOneCardNumber = 0;
		twoLine = 0;
	}

	public Vector3 GetTableCardposition(Card playingCard){

		if(ActionNumber ==0){
			return new Vector3(-1f, -1f, -1f);
		}

		ActionNumber--;
		ActionNumber +=playingCard.action;

		switch(playingCard.action){
		case 0:
			{
				if(twoLine > 0){
					for(int i=0; i<LineOneCardNumber; i++){
						if(isTableHavePlace[1,i]){
							isTableHavePlace[1,i] = false;
							TableCardID[1,i] = playingCard.ID;
							twoLine--;
							return new Vector3(i*Width, -1*Height, 0f);
						}
					}
				}else{
					isTableHavePlace[0,LineOneCardNumber] = false;
					TableCardID[0,LineOneCardNumber] = playingCard.ID;
					LineOneCardNumber++;
					return new Vector3((LineOneCardNumber-1)*Width, 0f, 0f);
				}

			}
			break;
		case 1:
			{
				isTableHavePlace[0,LineOneCardNumber] = false;
				isTableHavePlace[0,LineOneCardNumber+1] = true;
				TableCardID[0,LineOneCardNumber] = playingCard.ID;
				LineOneCardNumber++;

				//arrow
				GameObject NRarrow  = Instantiate(RightArrow);
				NRarrow.transform.SetParent(Table.Ins.transform);
				NRarrow.transform.localPosition = new Vector3((LineOneCardNumber-1)*Width + Width/2, 0f, 0f);
				ArrowPlace[0, (LineOneCardNumber-1)] = NRarrow;

				return new Vector3((LineOneCardNumber-1)*Width, 0f, 0f);
			}
			break;
		case 2:
			{
				isTableHavePlace[0,LineOneCardNumber] = false;
				isTableHavePlace[0,LineOneCardNumber+1] = true;
				isTableHavePlace[1,LineOneCardNumber] = true;
				TableCardID[0,LineOneCardNumber] = playingCard.ID;
				LineOneCardNumber++;
				twoLine++;

				//arrow
				GameObject NRarrow  = Instantiate(RightArrow);
				NRarrow.transform.SetParent(Table.Ins.transform);
				NRarrow.transform.localPosition = new Vector3((LineOneCardNumber-1)*Width + Width/2, 0f, 0f);
				ArrowPlace[0, (LineOneCardNumber-1)] = NRarrow;

				GameObject NDarrow  = Instantiate(DownArrow);
				NDarrow.transform.SetParent(Table.Ins.transform);
				NDarrow.transform.localPosition = new Vector3((LineOneCardNumber-1)*Width, -Height/2, 0f);
				ArrowPlace[1, (LineOneCardNumber-1)] = NDarrow;

				return new Vector3((LineOneCardNumber-1)*Width, 0f, 0f);
			}
			break;		
		}
		return new Vector3(-1f, -1f, -1f);
	}

	public void Discard_T_All(CardManager.MainSection Tsec){
		foreach(Card i in TableList){
			i.isSectionOver = false;
			Tsec.CheckLsit.Add(i);
			i.Place = CardManager.cardSection.Discard_T;
			i.Discard();
		}
		TableList.Clear();

		//arrow
		foreach(GameObject j in ArrowPlace){			
			Destroy(j);
		}
	}

	public void initTable(){
		//bool array
		for(int i =0; i<isTableHavePlace.GetLength(0); i++){
			for(int j =0; j<isTableHavePlace.GetLength(1); j++){
				isTableHavePlace[i,j] = false;
			}
		}
		isTableHavePlace[0,0] = true;

		ActionNumber = 1;
		LineOneCardNumber = 0;
		twoLine = 0;
	}
}
