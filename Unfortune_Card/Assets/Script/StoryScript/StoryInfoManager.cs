using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StoryInfoManager : MonoBehaviour {

	//GameObject
	public GameObject storyPaper;
	public GameObject StoryUIGO;


	void Start(){		
	}


	public void SetStoryPaper(int PaperID){
		//Story
		StoryManager.Story TStory = StoryManager.Ins.StoryList[PaperID];

		//Paper Object
		GameObject TPaper = Instantiate(storyPaper);
		TPaper.transform.SetParent(StoryUIGO.transform, false);

		//Paper info Setting

	}

}
