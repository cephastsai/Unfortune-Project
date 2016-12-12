using UnityEngine;
using System.Collections;

public class SpawnLight : MonoBehaviour {

	public int Stauts = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Stauts==1)
		{
			Stauts = 0;
			Instantiate(Resources.Load("LeftLight1"),new Vector3(-6.2f,0.1f,-16), Quaternion.identity);
		}

		if(Stauts==2)
		{
			Stauts = 0;
			Instantiate(Resources.Load("LeftLight2"),new Vector3(-5f,0.1f,-16), Quaternion.identity);
		}

		if(Stauts==3)
		{
			Stauts = 0;
			Instantiate(Resources.Load("RightLight1"),new Vector3(6.2f,0.1f,-16), Quaternion.identity);
		}

		if(Stauts==4)
		{
			Stauts = 0;
			Instantiate(Resources.Load("RightLight2"),new Vector3(5f,0.1f,-16), Quaternion.identity);
		}

	
	}

}
