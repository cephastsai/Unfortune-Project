using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddTexttoCanvas : MonoBehaviour {

	public static AddTexttoCanvas Addtext;

	void Awake(){
		Addtext = this;
	}

	public Text Add(string _name, int _font){		
		Text _Text;

		GameObject _NGameObject = new GameObject(_name);
		//GaeObject setting
		_NGameObject.transform.SetParent(transform);
		_Text =  _NGameObject.AddComponent<Text>();





		return _Text;
	}

}
