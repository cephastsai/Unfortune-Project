using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

	public class StoryPoint{

		public Vector3 PSposition;
		public string PSName;
		public string PSstory;
		public int[] OptionCardID = new int[5];

		public StoryPoint(Vector3 pos,string Name, string data){
			PSposition = pos;
			PSName = Name;
			PSstory = data;
		}
	}

	public delegate void TreeVisitor<StoryPoint>(StoryPoint nodeData);

	public class StoryTree<StoryPoint>
	{
		public StoryPoint data;
		private LinkedList<StoryTree<StoryPoint>> children;

		public StoryTree(StoryPoint data){
			this.data = data;
			children = new LinkedList<StoryTree<StoryPoint>>();
		}

		public void AddChild(StoryPoint data){
			children.AddFirst(new StoryTree<StoryPoint>(data));
		}

		public StoryTree<StoryPoint> GetChild(int i){
			foreach (StoryTree<StoryPoint> n in children){
				if (--i <= 0){
					return n;
				}					
			}				
			return null;
		}

		public void Traverse(StoryTree<StoryPoint> node, TreeVisitor<StoryPoint> visitor){
			visitor(node.data);
			foreach (StoryTree<StoryPoint> kid in node.children)
				Traverse(kid, visitor);
		}

		public int GetChildCount(){
			return children.Count;
		}
	}

	public StoryTree<StoryPoint> MainST = new StoryTree<StoryPoint>(null);
	public GameObject Player;
	public StoryPoint NowPlayerPosition;

	//Image
	public GameObject Knife;


	public void init(){
		Player = GameObject.Find("Player");
		//temp story setting

		StoryPoint Npoint1 = new StoryPoint(
			new Vector3(-6.31f, -2.15f, -1f),
			"測試點1",
			"韓國電競為何強？KeSPA接受中國採訪給你答案(1)\n\n剛結束的S6四分之一決賽上，來自LCK賽區的三支韓國隊都出線進入四強。LPL賽區的IM雖\n敗猶榮，但剩下的RNG和EDG均在比賽中敗給了自己的對手SKT和ROX Tigers。\n\n不得不說韓國今年的三支參加S6的隊伍都發揮得不錯，似乎說起電競第一大國，大家第一\n個想到的總是韓國。那麼韓國的電競業究竟有多牛逼呢？他們為什麼牛逼呢？"
		);
		MainST.AddChild(Npoint1);

		StoryPoint Npoint2 = new StoryPoint(
			new Vector3(-8.09f, -0.82f, -1f),
			"測試點2",
			"每次看到我的精髓欄裡面有個跳錢\n\n我就覺得好感傷\n\n從S2之後就沒有用過這個經髓了\n\n想當年  龜殼 賢者 三跳錢"
		);
		MainST.AddChild(Npoint2);
	}

	public void SetStoryPiont(){		
		for(int i=0; i<MainST.GetChildCount(); i++){
			//Instantiate(Resources.Load("Prifabs/Knife"));
			GameObject Npiont = (GameObject)Instantiate(Resources.Load("Prifabs/Knife"), MainST.GetChild(i).data.PSposition, Quaternion.identity);
			Npiont.AddComponent<MapStoryPiont>().init();
		}
	}

	void Update(){
		
	}
}
