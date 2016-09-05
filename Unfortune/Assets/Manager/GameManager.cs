using UnityEngine;
using System.Collections;
using Manager;
using System;

namespace Manager{
	public class GameManager : MonoBehaviour {

		//Singleton
		private static GameManager _instance = null;

		public static GameManager Instance{

			get{
				return _instance;	
			}
		}//Instance

		void Awake(){
			if(_instance == null){
				_instance = this;

				DontDestroyOnLoad(gameObject);
			}else if(_instance != this){
				Destroy(gameObject);
			}
		}

		//Random
		public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

		//Delegate
		public delegate void ManagerDelgate();
		public event ManagerDelgate UpdateList;	//Updata Delegate

		//Game Section
		public enum GameSection{
			Card_Start,
			Card_Play,
			Card_End,
		};

		//Manager
		public CardManager cardmanager;
		public GameObject PrefabsManagerGO;//need setting
		public PrefabsManager prefabsmanager;
		//public PlayerActions playeractions;

		void Start(){

			ManagerSetting();

			Game_init();
		}

		void Update(){
			
			//Event UpdateList
			if(UpdateList != null){
				UpdateList();
			}
		}

		void Game_init(){
			cardmanager.init();
		}



		void ManagerSetting(){
			//Card Manager
			GameObject cardmanagerGO = new GameObject("CardManager");
			cardmanager = cardmanagerGO.AddComponent<CardManager>();
			cardmanagerGO.transform.SetParent(transform);

			//Prefabs Manager
			GameObject PrefabsM = Instantiate(PrefabsManagerGO);
			PrefabsM.name = "PrefabsManager";
			PrefabsM.transform.SetParent(transform);
			prefabsmanager = PrefabsM.GetComponent<PrefabsManager>();
			prefabsmanager.init();

			//Player Actions
			//playeractions = gameObject.AddComponent<PlayerActions>();
		
		}
	}
}


