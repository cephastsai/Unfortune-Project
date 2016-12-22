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

	//Story Info
	StoryManager.Story TStory;
	public int StoryInfoIndex = 0;
	public List<string> infoTag = new List<string>();



	void Start(){
		StoryManager.Ins.LoadStoryData();
		//init
		StoryInfoIndex = 0;

		//function
		infoTag.Add("tar2");
		infoTag.Add("tag1");
		infoTag.Add("has");
		infoTag.Add("huhu");

		SetPaperID(1);
	}


	public void SetPaperID(int PaperID){
		//Story
		TStory = StoryManager.Ins.StoryList[PaperID];

		//init
		StoryInfoIndex = 0;

		SetPaperInfo();
	}

	public void SetPaperInfo(){
		//Paper info Setting
		if(StoryInfoIndex != TStory.StoryInfoList.Count){
			switch(TStory.StoryInfoList[StoryInfoIndex].Kind){
			case 1:
				GetPaperInfo();
				break;
			case 2:			
				break;
			case 3:
				break;
			case 4:
				break;
			}
			GameManager.Instance.TE.TEDObjectCL += StoryClickBox;
		}
	}


	public void GetPaperInfo(){
		//Paper Object
		GameObject TPaper = Instantiate(storyPaper);
		TPaper.transform.SetParent(StoryUIGO.transform, false);

		string info = "";
		//first Next
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
		TPaper.transform.GetChild(0).GetComponent<Text>().text = info;

		//Typing
		TPaper.transform.GetChild(0).gameObject.AddComponent<TextTyping>();

		//moving
		TPaper.AddComponent<PaperMoving>();
	}


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
			print("hit");
			SetPaperInfo();
			GameManager.Instance.TE.TEDObjectCL -= StoryClickBox;
		}
	}

	public void Option(){
		print("123");
	}
}
