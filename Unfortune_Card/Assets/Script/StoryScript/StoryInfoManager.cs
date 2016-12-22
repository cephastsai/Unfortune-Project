using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StoryInfoManager : MonoBehaviour {

	//GameObject
	public GameObject storyPaper;
	public GameObject StoryUIGO;
	public Transform ClickBox;
	public GameObject Arrow;

	//Story Info
	StoryManager.Story TStory;
	public int StoryInfoIndex = 0;
	public List<string> infoTag = new List<string>();

	//paper
	public List<GameObject> PaperList = new List<GameObject>();

	//Fighting UI manager
	public UIManager UImanager;

	void Start(){
		StoryManager.Ins.LoadStoryData();
		//init
		StoryInfoIndex = 0;

		//function
		infoTag.Add("tar2");
		infoTag.Add("tag1");
		infoTag.Add("has");
		infoTag.Add("huhu");

		UImanager.BattleBegin();
		//SetPaperID(1);
	}


	//init in story beganing
	public void SetPaperID(int PaperID){
		//Story
		TStory = StoryManager.Ins.StoryList[PaperID];

		//init
		StoryInfoIndex = 0;

		SetPaperInfo();
	}

	//Every Paper beganing 
	public void SetPaperInfo(){
		//Paper info Setting
		if(StoryInfoIndex != TStory.StoryInfoList.Count){
			switch(TStory.StoryInfoList[StoryInfoIndex].Kind){
			case 1:
				//Paper Object
				GameObject TPaper = Instantiate(storyPaper);
				TPaper.transform.SetParent(StoryUIGO.transform, false);
				PaperList.Add(TPaper);
				//moving
				TPaper.AddComponent<PaperMoving>();						
				break;
			case 2:			
				break;
			case 3:
				break;
			case 4:
				break;
			}

		}
	}

	//Set Paper text
	public void GetPaperInfo(){		
		string info = "";
		//First Paper is Next Paper
		if(isTaghaveNextPaper(StoryInfoIndex)){
			S_Story Sinfo =  (S_Story)TStory.StoryInfoList[StoryInfoIndex];
			info += Sinfo._Info;
			StoryInfoIndex++;
		}
		//whlie
		while(StoryInfoIndex < TStory.StoryInfoList.Count){	
			if(isPaperInfoCanShow(StoryInfoIndex)){
				if(TStory.StoryInfoList[StoryInfoIndex].Kind == 1){					
					if(isTaghaveNextPaper(StoryInfoIndex)){
						print("Next");
						break;
					}else{
						S_Story Sinfo =  (S_Story)TStory.StoryInfoList[StoryInfoIndex];
						info += Sinfo._Info;
					}
				}else if(TStory.StoryInfoList[StoryInfoIndex].Kind == 2){
					S_GetCards Sgetcard = (S_GetCards)TStory.StoryInfoList[StoryInfoIndex];
					info +="獲得" +Sgetcard._CardID.ToString()+"\n";
				}else if(TStory.StoryInfoList[StoryInfoIndex].Kind == 3){				
					break;
				}else if(TStory.StoryInfoList[StoryInfoIndex].Kind == 4){
					break;
				}
			}
				
			StoryInfoIndex++;
		}
		print("End: "+StoryInfoIndex);

		//Typing and set text
		PaperList.Last().transform.GetChild(0).gameObject.AddComponent<TextTyping>().SetText(info);

		//Click
		GameManager.Instance.TE.TEDObjectCL += StoryClickBox;
	}



	//Other function
	private bool isPaperInfoCanShow(int index){
		int tagnum = 0;
		foreach(string tag in infoTag){
			foreach(string _tag in TStory.StoryInfoList[index]._Tag){
				if(tag == _tag){
					tagnum++;
				}
			}
		}

		if(isTaghaveNextPaper(index)){
			tagnum++;
		}

		if(tagnum == TStory.StoryInfoList[index]._Tag.Count()){
			return true;
		}else{
			return false;
		}
	}

	private bool isTaghaveNextPaper(int index){
		foreach(string _tag in TStory.StoryInfoList[index]._Tag){			
			if(_tag == "NextPaper"){				
				return true;
			}
		}	
		return false;
	}

	public void StoryClickBox(Transform target){
		if(target == ClickBox){
			if(!PaperList.Last().transform.GetChild(0).GetComponent<TextTyping>().endTyping){
				PaperList.Last().transform.GetChild(0).GetComponent<TextTyping>().Skip();
				Arrow.SetActive(true);
			}else{
				SetPaperInfo();
				GameManager.Instance.TE.TEDObjectCL -= StoryClickBox;
				Arrow.SetActive(false);
			}
		}
	}		


	public void Option(){
		print("123");
	}
}
