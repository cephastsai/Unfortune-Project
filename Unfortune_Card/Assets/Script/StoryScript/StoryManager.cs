using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoryManager : MonoBehaviour {

	public Dictionary<int, Story> StoryList  = new Dictionary<int, Story>();

	//test
	public string info;

	void Start(){
		/*
		info = "[1#][4#][#1]";

		string[] infos = setStory(info);

		foreach(string i in infos){			
			if(i.Contains("#")){
				string tempi = i.Replace("#","");
				print(tempi);
			}

		}*/


		Text temp =  AddTexttoCanvas.Addtext.Add("test",20);
		Canvas tempcan = temp.gameObject.AddComponent<Canvas>();
		tempcan.overrideSorting = true;
		tempcan.sortingLayerName = "CardUI";
		temp.text = "fo2efowfeoi";
	}


	public string[] setStory(string data){

		string[] temp = data.Split('[',']');

		return temp;
	}


}
