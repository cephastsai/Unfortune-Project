using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

	public class StoryPiont{

		public Vector3 SPposition;
		public string SPName;
		public string SPstory;
		public List<int> OptionCardID = new List<int>();
		public List<int> SelectCardID = new List<int>();
		public int SelectNum;

		public StoryPiont(Vector3 pos,string Name, string data){
			SPposition = pos;
			SPName = Name;
			SPstory = data;
		}
	}

	public delegate void TreeVisitor<StoryPoint>(StoryPoint nodeData);

	public class StoryTree<StoryPiont>
	{
		public StoryPiont data;
		private LinkedList<StoryTree<StoryPiont>> children;

		public StoryTree(StoryPiont data){
			this.data = data;
			children = new LinkedList<StoryTree<StoryPiont>>();
		}

		public void AddChild(StoryPiont data){
			children.AddFirst(new StoryTree<StoryPiont>(data));
		}

		public StoryTree<StoryPiont> GetChild(int i){
			foreach (StoryTree<StoryPiont> n in children){
				if (--i < 0){
					return n;
				}					
			}				
			return null;
		}

		public void Traverse(StoryTree<StoryPiont> node, TreeVisitor<StoryPiont> visitor){
			visitor(node.data);
			foreach (StoryTree<StoryPiont> kid in node.children)
				Traverse(kid, visitor);
		}

		public int GetChildCount(){
			return children.Count;
		}
	}

	public Text[] SPtext = new Text[5];
	public GameObject StoryButton;
	public TextTyping StoryTT;
	public bool showflag = false;

	public StoryTree<StoryPiont> MainST = new StoryTree<StoryPiont>(null);
	public GameObject Player;
	public StoryPiont NowPlayerPosition;
	private int NowSPnum;

	//Image
	public GameObject Knife;


	public void init(){
		Player = GameObject.Find("Player");
		StoryTT = GameObject.Find("StoryText").GetComponent<TextTyping>();
		//StoryButton = GameObject.Find("StoryCuntinue");
		//temp story setting

		StoryPiont Npiont1 = new StoryPiont(
			new Vector3(-6.31f, -2.15f, 91f),
			"黎明之時",
			"太陽升起，復仇的計畫即將開始。\n你拿起了你的刀。\n準備前進了，你抬起頭來，卻又被巴了下去。\n\n\"把你的東西準備好，小子!\n帶你該帶的東西，沒用的東西就別帶了。\""
		);
		Npiont1.SelectCardID.Add(102);
		Npiont1.SelectCardID.Add(101);
		Npiont1.SelectCardID.Add(100);
		Npiont1.SelectCardID.Add(101);
		Npiont1.SelectNum = 3;
		MainST.AddChild(Npiont1);

		StoryPiont Npiont2 = new StoryPiont(
			new Vector3(-7f, -3f, 91f),
			"測試",
			"太神啦"
		);
		MainST.GetChild(0).AddChild(Npiont2);

		/*
		StoryPiont Npoint2 = new StoryPiont(
			new Vector3(-8.09f, -0.82f, 91f),
			"測試點2",
			"錢"
		);
		MainST.AddChild(Npoint2);*/
	}

	void Update(){
		if(showflag && StoryTT.endTyping){
			StoryButton.SetActive(true);
		}
	}

	public void SetStoryPiont(){
		print(MainST.GetChildCount());
		for(int i=0; i<MainST.GetChildCount(); i++){
			//Instantiate(Resources.Load("Prifabs/Knife"));
			GameObject Npiont = (GameObject)Instantiate(Resources.Load("Prifabs/Knife"), MainST.GetChild(i).data.SPposition, Quaternion.identity);
			Npiont.AddComponent<MapStoryPiont>().init(MainST.GetChild(i).data.SPName);
			Npiont.gameObject.name = "StoryPiont"+i;	
		}
	}

	public void ShowText(string name){
		for(int i=0; i<MainST.GetChildCount(); i++){
			if(MainST.GetChild(i).data.SPName == name){
				//text
				SPtext[i].text = MainST.GetChild(i).data.SPName;
				SPtext[i].rectTransform.position = MainST.GetChild(i).data.SPposition;
			}
		}
	} 

	public void PlayerMove(string Name){
		for(int i=0; i<MainST.GetChildCount(); i++){			
			if(MainST.GetChild(i).data.SPName == Name){
				GameManager.Instance.Mapmanager.Player.GetComponent<MapMove>().ReadyToMove(MainST.GetChild(i).data.SPposition);
				NowPlayerPosition = MainST.GetChild(i).data;
				NowSPnum = i;
				MainST = MainST.GetChild(i);
			}else{				
				SPtext[i].text ="";
				GameObject.Find("StoryPiont"+i).GetComponent<KnifeFalling>().DestoryOption();
			}				
		}
	}

	public void StartOption(){		
		//Mark Destroy
		SPtext[NowSPnum].text ="";
		GameObject.Find("StoryPiont" +NowSPnum).GetComponent<KnifeFalling>().DestoryOption();

		//transitions
		GameManager.Instance.UImanager.MapToStory();
		GameManager.Instance.Storymanager.StoryTextTyping(MainST.data.SPstory);
		showflag = true;
	}

	public void CuntinueButton(){
		StoryButton.SetActive(false);
		GameManager.Instance.Storymanager.StoryTextTyping("");

		//set GameSection
		GameManager.Instance.SetGameSection(GameManager.GameSection.Cards);
	}
}
