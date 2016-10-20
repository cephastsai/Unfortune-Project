using UnityEngine;
using System.Collections;

public class MapStoryPiont : MonoBehaviour {

	public KnifeBling KB;
	public string SPName;

	public void init(string name){
		KB = GetComponent<KnifeBling>();
		SPName = name;

		GameManager.Instance.TE.TEDObjectHit += OptionHit;
		GameManager.Instance.TE.TEDObjectCL += OptionClick;
	}


	public void OptionHit(Transform target){
		if(target == transform){
			KB.StartBling();
		}else if(target == null){
			KB.StopBling();
		}else{
			KB.StopBling();
		}
	}

	public void OptionClick(Transform target){
		if(target == transform){
			GameManager.Instance.Mapmanager.PlayerMove(SPName);
		}
	}

	void OnDestroy(){
		GameManager.Instance.TE.TEDObjectHit -= OptionHit;
		GameManager.Instance.TE.TEDObjectCL -= OptionClick;
	}
}
