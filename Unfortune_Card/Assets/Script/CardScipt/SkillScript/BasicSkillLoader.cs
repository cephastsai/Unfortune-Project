using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class BasicSkillLoader : MonoBehaviour {

	public class BasicSkill{
		public int Action;
		public int Attack;
		public int Cards;

		public BasicSkill(int _action, int _attack, int _Cards){
			Action = _action;
			Attack = _attack;
			Cards  = _Cards;
		}

	}

	public Dictionary<int, BasicSkill> BasicSkillList = new Dictionary<int, BasicSkill>();

	//json
	private string jsonString;
	private JsonData jsonData;

	void Start(){
		loading();
	}


	private void loading(){
		jsonString = File.ReadAllText(Application.dataPath +"/Json/BasicSkill.json");
		jsonData = JsonMapper.ToObject(jsonString);

		for(int i=0; i<jsonData.Count; i++){
			BasicSkill NBS = new BasicSkill(
				int.Parse(jsonData[i]["Action"].ToString()),
				int.Parse(jsonData[i]["Attack"].ToString()),
				int.Parse(jsonData[i]["Cards"].ToString())
			);

			BasicSkillList.Add(i, NBS);
		}
	}


	public BasicSkill GetCardBasicSkill(int cardkind){		
		return BasicSkillList[cardkind];
	}
}
