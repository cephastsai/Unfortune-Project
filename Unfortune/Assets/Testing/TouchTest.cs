using UnityEngine;
using System.Collections;

public class TouchTest : MonoBehaviour {

	public TouchEvent TE;

	// Use this for initialization
	void Start () {
		TE.TEDObjectHL += touchCube;
	}
	
	public void touchCube(Transform target){
		print(target);
		if(target == transform){
			print("touchCube");
		}else if(target == null){
			print("touchNothing");
		}
	}
}
