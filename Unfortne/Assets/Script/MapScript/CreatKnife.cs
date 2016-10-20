using UnityEngine;
using System.Collections;

public class CreatKnife : MonoBehaviour {

	public Vector3 KnifePosition;
	// Use this for initialization
	void Start () {
		KnifePosition =new Vector3(transform.position.x,transform.position.y+10,transform.position.z) ;
		Instantiate(Resources.Load("Prifabs/Knife"),KnifePosition, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void  Creat()
	{
		KnifePosition =new Vector3(transform.position.x,transform.position.y+10,transform.position.z) ;
		Instantiate(Resources.Load("Prifabs/Knife"),KnifePosition, Quaternion.identity);
	}
}
