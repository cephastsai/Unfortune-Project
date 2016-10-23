using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

	public class StoryPiont{

		public Vector3 SPposition;
		public string SPName;
		public string SPstory;
		public string SPtitle;
		public List<int> OptionCardID = new List<int>();
		public List<int> SelectCardID = new List<int>();
		public int SelectNum;

		public StoryPiont(Vector3 pos,string Name, string data, string title){
			SPposition = pos;
			SPName = Name;
			SPstory = data;
			SPtitle = title;
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
	private bool SPButtonflag = true;

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
			"太陽升起，復仇的計畫即將開始。\n你拿起了你的刀。\n準備前進了，你抬起頭來，卻又被巴了下去。\n\n\"把你的東西準備好，小子!\n帶你該帶的東西，沒用的東西就別帶了。\"",
			"看來我應該要選擇我要帶什麼東西，不知道帶什麼東西比較好~\n\n<color=#EE2C0CFF>[選擇三張卡牌加入棄牌堆]</color>"
		);
		Npiont1.SelectCardID.Add(103);
		Npiont1.SelectCardID.Add(104);
		Npiont1.SelectCardID.Add(105);
		Npiont1.SelectCardID.Add(106);
		Npiont1.SelectCardID.Add(107);
		Npiont1.SelectCardID.Add(109);
		Npiont1.SelectNum = 3;
		MainST.AddChild(Npiont1);

		/*StoryPiont Npiont2 = new StoryPiont(
			new Vector3(-5f, -3f, 91f),
			"測試",
			"太神啦",
			""
		);
		Npiont2.OptionCardID.Add(100);
		Npiont2.OptionCardID.Add(102);
		Npiont2.OptionCardID.Add(101);
		//MainST.GetChild(0).AddChild(Npiont2);
		MainST.AddChild(Npiont2);*/


		StoryPiont Npoint3 = new StoryPiont(
			new Vector3(-5f, -3f, 91f),
			"整裝",
			"\"準備好了嗎?\"身旁的老頭子問道。\n你點了點頭，雖然有很多東西很難取捨，把所有東西戴上卻也不是辦法。\n要是有錢可以買一隻馬匹就好了。\n但想歸想，你還是乖乖的把袋子背了起來。\n你可不想要被這個老頭子在巴頭，沒有錢的窮光蛋還裝的一副貴族的樣子。\n\"你有什麼缺少的嗎?\"老頭子平淡的問你。\n你瞪大的眼睛，看著老頭子。\n",
			"這個老頭子是吃錯藥了嗎?\n不不不，他根本沒錢買藥啊!\n看來我應該要好好想想要怎麼坑他嘿嘿。\n\n<color=#EE2C0CFF>[選擇一張選項卡打出]</color>"
		);
		Npoint3.OptionCardID.Add(108);
		Npoint3.OptionCardID.Add(1001);
		Npoint3.OptionCardID.Add(1001);
		Npoint3.OptionCardID.Add(1001);
		MainST.GetChild(0).AddChild(Npoint3);
		//MainST.AddChild(Npoint3);
	}

	void Update(){
		if(showflag && StoryTT.endTyping){
			StoryButton.SetActive(true);
		}

		/*if(MainST != null){
			print(MainST.data.SPName);
		}*/
	}

	public void SetStoryPiont(){		
		for(int i=0; i<MainST.GetChildCount(); i++){
			//Instantiate(Resources.Load("Prifabs/Knife"));
			GameObject Npiont = (GameObject)Instantiate(Resources.Load("Prifabs/Knife"), MainST.GetChild(i).data.SPposition, Quaternion.identity);
			Npiont.AddComponent<MapStoryPiont>().init(MainST.GetChild(i).data.SPName);
			Npiont.gameObject.name = "StoryPiont"+i;
			Npiont.GetComponent<SpriteRenderer>().sortingOrder = -7;
		}
		SPButtonflag = true;
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
		if(SPButtonflag){
			for(int i=0; i<MainST.GetChildCount(); i++){			
				if(MainST.GetChild(i).data.SPName == Name){
					GameManager.Instance.Mapmanager.Player.GetComponent<MapMove>().ReadyToMove(MainST.GetChild(i).data.SPposition);
					NowPlayerPosition = MainST.GetChild(i).data;
					NowSPnum = i;
				}else{				
					SPtext[i].text ="";
					GameObject.Find("StoryPiont"+i).GetComponent<KnifeFalling>().DestoryOption();
				}				
			}
			MainST = MainST.GetChild(NowSPnum);
			SPButtonflag = false;
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
