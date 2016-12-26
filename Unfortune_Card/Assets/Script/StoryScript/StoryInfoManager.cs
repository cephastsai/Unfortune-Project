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

	//Option
	private Vector2[] OptionPosition = new Vector2[4];
	public GameObject OptionPaper;
	private GameObject[] OptionGOA = new GameObject[4];

	//Fighting UI manager
	public UIManager UImanager;

	void Start(){
		StoryManager.Ins.LoadStoryData();
		//init
		StoryInfoIndex = 0;
		OptionPosition[0] = new Vector2(876, -512);
		OptionPosition[1] = new Vector2(744, -633);
		OptionPosition[2] = new Vector2(588, -761);

		//function
		SetPaperID(1);
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
		if(StoryInfoIndex < TStory.StoryInfoList.Count){
			switch(TStory.StoryInfoList[StoryInfoIndex].Kind){
			case 1:
				//Paper Object
				GameObject TPaper = Instantiate(storyPaper);
				TPaper.transform.SetParent(StoryUIGO.transform, false);
				TPaper.GetComponent<Canvas>().sortingOrder = StoryInfoIndex;
				PaperList.Add(TPaper);
				//moving
				TPaper.AddComponent<PaperMoving>();
				break;
			case 2:			
				break;
			case 3:
				SetOptioninfo();
				break;
			case 4:
				SetFighting();
				break;
			}

		}else{
			print("over");
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
					print(StoryInfoIndex);
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
					CardManager.Ins.GetCardQue.Enqueue(Sgetcard._CardID);
				}else if(TStory.StoryInfoList[StoryInfoIndex].Kind == 3){				
					break;
				}else if(TStory.StoryInfoList[StoryInfoIndex].Kind == 4){
					break;
				}
			}				
				
			StoryInfoIndex++;
		}

		//Typing and set text
		PaperList.Last().transform.GetChild(0).gameObject.AddComponent<TextTyping>().SetText(info);

		//Click
		ClickBox.gameObject.AddComponent<BoxCollider>();
		ClickBox.GetComponent<BoxCollider>().center = new Vector2(1.7f, 0.13f);
		ClickBox.GetComponent<BoxCollider>().size = new Vector2(14.3f, 7.7f);
		GameManager.Instance.TE.TEDObjectCL += StoryClickBox;
	}

	//Set Option
	public void SetOptioninfo(){
		S_Option NSOption = (S_Option)TStory.StoryInfoList[StoryInfoIndex];
		for(int i=0; i<NSOption.OptionList.Count; i++){
			//Option Paper GameObject
			GameObject OptionGO =  Instantiate(OptionPaper);
			OptionGO.transform.SetParent(StoryUIGO.transform, false);
			OptionGO.AddComponent<GameObjectMoving>().SetTergetPostion(OptionPosition[2-i],20f);
			OptionGO.GetComponent<Canvas>().sortingOrder = 13-i;
			OptionGO.GetComponent<OptionInfo>().SetOptionInfo(NSOption.OptionList[i]._OptionInfo,NSOption.OptionList[i]._AddTag);

			//Add List
			OptionGOA[i] = OptionGO;

		}

		StoryInfoIndex++;
	}

	//set Fighting
	public void SetFighting(){
		GameManager.Instance.SetGameSection(GameManager.GameSection.Cards);
		//class
		S_Fighting NSFighting = (S_Fighting)TStory.StoryInfoList[StoryInfoIndex];

		//cards
		List<int> tempcards = new List<int>();
		foreach(int i in NSFighting._Cards){
			tempcards.Add(i);
		}
		EnemyCardManager.Ins.SetEnemyCards(tempcards);

		GameManager.Instance.TE.TEDObjectCL -= StoryClickBox;
		Destroy(ClickBox.GetComponent<BoxCollider>());
	}

	public void FightingEnd(){
		//Click
		ClickBox.gameObject.AddComponent<BoxCollider>();
		ClickBox.GetComponent<BoxCollider>().center = new Vector2(1.7f, 0.13f);
		ClickBox.GetComponent<BoxCollider>().size = new Vector2(14.3f, 7.7f);
		GameManager.Instance.TE.TEDObjectCL += StoryClickBox;

		StoryInfoIndex++;
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
				Destroy(ClickBox.GetComponent<BoxCollider>());
				Arrow.SetActive(false);
			}
		}
	}		


	public void Option(GameObject target){
		//Find Option
		foreach(GameObject i in OptionGOA){
			if(i == null){
				
			}else if(target == i){
				i.transform.localRotation = Quaternion.Euler(0, 0, 0);
				i.AddComponent<PaperMove>().Move(0);

				//OptionText Fade Out(undone)
				i.transform.GetChild(1).GetComponent<Text>().text = "";

				//Add Tag
				infoTag.Add(i.GetComponent<OptionInfo>().tag);

				//Set Info
				PaperList.Add(i);
				GetPaperInfo();
				i.GetComponent<Canvas>().sortingOrder = StoryInfoIndex;
			}else{
				i.AddComponent<MovingAndDestroy>().SetTergetPostion(new Vector3(i.transform.localPosition.x, i.transform.localPosition.y-1000, i.transform.localPosition.z), 20f);
			}

		}			

	}
		
}
