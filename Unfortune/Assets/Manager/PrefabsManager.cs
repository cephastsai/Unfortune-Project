using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Manager{
	public class PrefabsManager : MonoBehaviour {

		public List<GameObject> PrefabsUIList =new List<GameObject>();

		public Dictionary<int, GameObject> PrefabsList = new Dictionary<int, GameObject>();

		public void init(){

			//Cards
			foreach(GameObject i in PrefabsUIList){
				string tempstring = i.name;
				int tempID = Convert.ToInt32(tempstring);
				//print("ID:"+tempID);
				PrefabsList.Add(tempID, i);
			}
		}

	}
}