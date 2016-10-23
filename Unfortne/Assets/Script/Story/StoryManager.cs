using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {

	public Text StoryText;
	public GameObject Log;

	void Start () {

		//init 
		StoryText = GameObject.Find("StoryText").GetComponent<Text>();

	}

	public void StoryTextTyping(string Info){
		StoryText.text = Info;
		StoryText.gameObject.GetComponent<TextTyping>().SetText();
		GameManager.Instance.TE.TEDObjectCL += SkipTest;
	}

	//testing
	public void SetTextInfo(string Info){
		StoryText.text = Info;
	}

	public void testingstory(){
		SetTextInfo("之前看了黃金羅盤的電影，覺得還蠻喜歡的，所以很認真的找了出另外兩部曲出來看，發現小說比電影好看不知多少倍( Gee~真是小說拍成電影不可避免之罪衍)！");
		StoryText.gameObject.GetComponent<TextTyping>().SetText();
	}

	public void SkipTest(Transform target){	
		if(target == Log.transform){			
			StoryText.gameObject.GetComponent<TextTyping>().Skip();
			GameManager.Instance.TE.TEDObjectCL -= SkipTest;
		}
	}
}
