using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryInfoManager : MonoBehaviour {

	public Text Etext;
	public class Info{

		public Info(){
			
		}
	}

	void Start(){
		Etext.text = "fjfjfjfjf\njfjfjf\nfjfj";
		AddText("fjfjfjfjf\njfjfjf\nfjfj");
	}

	public Text AddText(string data){
		
		//Text Ntext = AddTexttoCanvas.Addtext.Add("Ntext", 40);
		Text Ntext = Instantiate(Etext);
		Ntext.transform.SetParent(transform);
		Ntext.rectTransform.sizeDelta = new Vector2(10, LineofString(data)*0.6f);
		Ntext.rectTransform.localPosition = new Vector2(0, 1900);
		Ntext.text = data;
	


		return Ntext;
	}


	private int LineofString(string data){
		string[] temp = data.Split('\n');
		return temp.GetLength(0)-1;	
	}
}
