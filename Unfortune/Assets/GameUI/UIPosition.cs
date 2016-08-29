using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameUI{
	public class UIPosition : MonoBehaviour {
		//Singleton
		private static UIPosition _instance = null;

		public static UIPosition Instance{

			get{

				if(_instance == null){
					GameObject UIpostion = new GameObject("GameManager");

					_instance = UIpostion.AddComponent<UIPosition>();

					DontDestroyOnLoad( UIpostion);

					return _instance;
				}else{
					return _instance;
				}

			}
		}//Instance


		public Dictionary<string, Transform> UITransform = new Dictionary<string, Transform>();

		void Start(){
			
			for(int i=0; i<transform.childCount; i++){
				//print(transform.GetChild(i).name);
				UITransform.Add(transform.GetChild(i).name, transform.GetChild(i));
				//print(UITransform[transform.GetChild(i).name]);
			}

			//print(UITransform[transform.GetChild(0).name]);
		}
	}
}


