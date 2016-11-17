using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class CardsInfoLoader : MonoBehaviour {

	private List<string> CardsInfoList = new List<string>();

	//json
	private string jsonString;
	private JsonData jsonData;

	public void loading(){

		jsonString = File.ReadAllText(Application.dataPath +"/Json/CardsInfoJson.json");
		jsonData = JsonMapper.ToObject(jsonString);

		for(int i=0; i<jsonData.Count; i++){			
			CardsInfoList.Add(jsonData[i].ToString());
		}			
	}


	public string GetCardsInfo(int _cardkind){		
		return CardsInfoList[_cardkind];
	}
}
