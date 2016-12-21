using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StoryInfoManager : MonoBehaviour {

	//GameObject
	public GameObject storyPaper;
	public GameObject StoryUIGO;

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

		SetStoryPaper(1);
	}


	public void SetStoryPaper(int PaperID){
		//Story
		TStory = StoryManager.Ins.StoryList[PaperID];

		//Paper info Setting
		switch(TStory.StoryInfoList[StoryInfoIndex].Kind){
		case 1:
			GetPaperInfo();
			break;
		case 2:
			GetPaperInfo();
			break;
		case 3:
			break;
		case 4:
			break;
		}
	}


	public void GetPaperInfo(){
		//Paper Object
		GameObject TPaper = Instantiate(storyPaper);
		TPaper.transform.SetParent(StoryUIGO.transform, false);

		string info = "";
		while(StoryInfoIndex < TStory.StoryInfoList.Count){

			if(isPaperInfoCanShow(StoryInfoIndex)){
				if(TStory.StoryInfoList[StoryInfoIndex].Kind == 1){
					if(isTaghaveNextPaper(StoryInfoIndex)){
						break;
					}else{
						S_Story Sinfo =  (S_Story)TStory.StoryInfoList[StoryInfoIndex];
						info += Sinfo._Info;
					}
				}else if(TStory.StoryInfoList[StoryInfoIndex].Kind == 2){
					
				}else if(TStory.StoryInfoList[StoryInfoIndex].Kind == 3){					
					break;
				}else if(TStory.StoryInfoList[StoryInfoIndex].Kind == 4){
					break;
				}
			}

			print("index: "+StoryInfoIndex);
			StoryInfoIndex++;
		}

		TPaper.transform.GetChild(0).GetComponent<Text>().text = info;
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
}
