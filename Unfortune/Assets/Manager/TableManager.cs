using UnityEngine;
using System.Collections;
using Manager;

namespace Manager{
	public class TableManager : MonoBehaviour {

		//Position
		private Vector3 TablePos;
		private float Height = 30f;
		private float Width = 20f;

		//Table Section
		private int[,] TableCardID = new int[2,100];
		public bool[,] isTableHavePlace = new bool[2,100];
		public int ActionNumber = 1;
		public int LineOneCardNumber = 0;
		public int twoLine = 0;

		public void init(){
			TablePos = GameManager.Instance.cardmanager.TablePosition.localPosition;

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
								return new Vector3(i*Width, 1*Height, 0f);
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
					return new Vector3((LineOneCardNumber-1)*Width, 0f, 0f);
				}
				break;		
			}
			return new Vector3(-1f, -1f, -1f);
		}

	}
}


