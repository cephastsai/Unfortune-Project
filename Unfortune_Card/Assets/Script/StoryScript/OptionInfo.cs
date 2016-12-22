using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionInfo : MonoBehaviour {

	public Text OptionText;
	private bool isHit = false;
	public string tag;


	public void SetOptionInfo(string info, string _tag){
		OptionText.text = info;
		tag = _tag;
		GameManager.Instance.TE.TEDObjectHit += OptionHit;
		GameManager.Instance.TE.TEDObjectCL += OptionChiose;
	}

	public void OptionHit(Transform target){
		
		if(target == transform){
			if(!isHit){
				transform.localPosition += new Vector3(0, 20, 0);
				isHit = true;
			}
		}else if(target == null){			
			if(isHit){
				transform.localPosition += new Vector3(0, -20, 0);
				isHit = false;
			}
		}else{
			if(isHit){
				transform.localPosition += new Vector3(0, -20, 0);
				isHit = false;
			}

		}
	}

	public void OptionChiose(Transform target){	
		if(target == transform){
			StoryManager.Ins.SIManager.Option(gameObject);
		}
		GameManager.Instance.TE.TEDObjectCL -= OptionChiose;
		GameManager.Instance.TE.TEDObjectHit -= OptionHit;
	}		

	
}
