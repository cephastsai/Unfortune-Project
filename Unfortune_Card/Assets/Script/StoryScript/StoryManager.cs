using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System.Linq;

public class StoryManager : MonoBehaviour {
	//Singleton
	private static StoryManager _ins = null;

	public static StoryManager Ins{

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


	//Story List
	public Dictionary<int, Story> StoryList  = new Dictionary<int, Story>();

	public class Story{

		public string Name;

		public List<StoryInfo> StoryInfoList = new List<StoryInfo>();

	}	

	//test
	public string info;

	//json
	private string jsonString;
	private JsonData jsonData;

	//Manager
	public StoryInfoManager SIManager;


	void Start(){
		/*
		info = "[1#][4#][#1]";

		string[] infos = setStory(info);

		foreach(string i in infos){			
			if(i.Contains("#")){
				string tempi = i.Replace("#","");
				print(tempi);
			}

		}*/

		//LoadStoryData();
	}


	public string[] setStory(string data){

		string[] temp = data.Split('[',']');

		return temp;
	}

	public void LoadStoryData(){

		jsonString = File.ReadAllText(Application.dataPath +"/Json/Story.json");
		jsonData = JsonMapper.ToObject(jsonString);

		for(int i=0; i<jsonData.Count; i++){
			Story Nstory = new Story();

			Nstory.Name = jsonData[i][1].ToString();

			for(int j=2; j<jsonData[i].Count; j++){				
				switch(jsonData[i][j][0].ToString()){
				case "1":
					{
						S_Story NSStory = new S_Story();
						//kind
						NSStory.Kind = int.Parse(jsonData[i][j][0].ToString());
						//tag
						NSStory._Tag = new string[jsonData[i][j][1].Count];
						for(int w=0; w<jsonData[i][j][1].Count; w++){							
							NSStory._Tag[w] = jsonData[i][j][1][w].ToString();
						}
						//info
						NSStory._Info = jsonData[i][j][2].ToString();

						//Add List
						Nstory.StoryInfoList.Add(NSStory);
					}
					break;
				case "2":
					{
						S_GetCards NSGetcards = new S_GetCards();
						//kind
						NSGetcards.Kind = int.Parse(jsonData[i][j][0].ToString());
						//tag
						NSGetcards._Tag = new string[jsonData[i][j][1].Count];
						for(int w=0; w<jsonData[i][j][1].Count; w++){							
							NSGetcards._Tag[w] = jsonData[i][j][1][w].ToString();
						}
						//cardID
						NSGetcards._CardID =  int.Parse(jsonData[i][j][2].ToString());

						//Add List
						Nstory.StoryInfoList.Add(NSGetcards);
					}
					break;
				case "3":
					{
						S_Option NSOption = new S_Option();
						//kind
						NSOption.Kind = int.Parse(jsonData[i][j][0].ToString());
						//tag
						NSOption._Tag = new string[jsonData[i][j][1].Count];
						for(int w=0; w<jsonData[i][j][1].Count; w++){							
							NSOption._Tag[w] = jsonData[i][j][1][w].ToString();
						}
						//Option
						for(int w=0; w<jsonData[i][j][2].Count; w++){							
							Option NOption = new Option();
							NOption._AddTag = jsonData[i][j][2][w][0].ToString();
							NOption._OptionInfo = jsonData[i][j][2][w][1].ToString();
						}

						//Add List
						Nstory.StoryInfoList.Add(NSOption);
					}
					break;
				case "4":
					{
						S_Fighting NSFighting = new S_Fighting();
						//kind
						NSFighting.Kind = int.Parse(jsonData[i][j][0].ToString());
						//tag
						NSFighting._Tag = new string[jsonData[i][j][1].Count];
						for(int w=0; w<jsonData[i][j][1].Count; w++){							
							NSFighting._Tag[w] = jsonData[i][j][1][w].ToString();
						}
						//Cards
						NSFighting._Cards = new int[jsonData[i][j][2].Count];
						for(int w=0; w<jsonData[i][j][2].Count; w++){
							NSFighting._Cards[w] = int.Parse(jsonData[i][j][2][w].ToString());
						}

						//Add List
						Nstory.StoryInfoList.Add(NSFighting);
					}
					break;
				}
				//print(Nstory.StoryInfoList.Last().GetType());
			}
			StoryList.Add(int.Parse(jsonData[i][0].ToString()), Nstory);
		}// jasonData

	}//LoadStoryData function

}
