using UnityEngine;
using System.Collections;

public class MapStoryPiont : MonoBehaviour {

	public KnifeBling KB;

	public void init(){
		KB = GetComponent<KnifeBling>();

		GameManager.Instance.TE.TEDObjectHit += OptionHit;
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
}
