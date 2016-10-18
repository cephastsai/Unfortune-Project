using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

	public class StoryPiont{

		public Vector3 SPposition;
		public string SPName;
		public string SPstory;
		public int[] OptionCardID = new int[5];

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

		StoryPiont Npoint1 = new StoryPiont(
			new Vector3(-6.31f, -2.15f, -1f),
			"測試點1",
			"韓國電競為何強？KeSPA接受中國採訪給你答案(1)\n\n剛結束的S6四分之一決賽上，來自LCK賽區的三支韓國隊都出線進入四強。LPL賽區的IM雖\n敗猶榮，但剩下的RNG和EDG均在比賽中敗給了自己的對手SKT和ROX Tigers。"
		);
		MainST.AddChild(Npoint1);

		StoryPiont Npoint2 = new StoryPiont(
			new Vector3(-8.09f, -0.82f, -1f),
			"測試點2",
			"每次看到我的精髓欄裡面有個跳錢\n\n我就覺得好感傷\n\n從S2之後就沒有用過這個經髓了\n\n想當年  龜殼 賢者 三跳錢"
		);
		MainST.AddChild(Npoint2);
	}

	void Update(){
		if(showflag && StoryTT.endTyping){
			StoryButton.SetActive(true);
		}
	}

	public void SetStoryPiont(){		
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
		GameManager.Instance.Storymanager.StoryTextTyping(MainST.GetChild(NowSPnum).data.SPstory);
		showflag = true;
	}

	public void CuntinueButton(){
		StoryButton.SetActive(false);
		GameManager.Instance.Storymanager.StoryTextTyping("");

		//set GameSection
		GameManager.Instance.SetGameSection(GameManager.GameSection.Cards);
	}
}
