using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HpSetting : MonoBehaviour {

	//Singleton
	private static HpSetting _ins = null;

	public static HpSetting Ins{

		get{
			return _ins;	
		}
	}//Instance

	void Awake(){
		if(_ins == null){
			_ins = this;

			DontDestroyOnLoad(gameObject);
		}else if(_ins != this){
			Destroy(gameObject);
		}
	}

	public List<Card> HpList = new List<Card>();

	public void init(GameObject Ncard){
		Ncard.transform.SetParent(transform);
		Ncard.transform.localPosition = new Vector3(0, 0, 0);
		HpList.Add(Ncard.GetComponent<Card>());
	}


	public void Setting(int Settlement){

		if(Settlement >0){

		}else if(Settlement <0){
			HpList.Last().gameObject.AddComponent<HpMoving>();
			FightingManager.Ins.isHpSettingOver = true;
		}
	}		
}