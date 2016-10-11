using UnityEngine;
using System.Collections;

public class Preview : MonoBehaviour {

	public TouchEvent TE;
	public GameObject BigCard;
	public GameObject Maincamera;
	public Vector3 PreviewPoint;
	public bool Creat = false;
	// Use this for initialization
	void Start () {
		Maincamera = GameObject.Find ("Main Camera");
		Camera camera = Maincamera.GetComponent<Camera>();	

		TE = GameObject.Find ("TouchManager").GetComponent<TouchEvent> ();

		if (TE != null) 
		{			
			TE.TEDObjectCR +=CreatBigOne;
			TE.TEDObjectHR +=preview;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void CreatBigOne(Transform j)
	{
		if (j == this.transform) 
		{
			/*BigCard = Instantiate(gameObject);
			Destroy (BigCard.GetComponent<Preview> ());	
			BigCard.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			PreviewPoint = new Vector3 (BigCard.transform.position.x-20, BigCard.transform.position.y+45, 0);
			BigCard.transform.localScale = new Vector3 (5, 5, 1);
			BigCard.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			BigCard.transform.position = PreviewPoint;*/

		}
	}
	public void preview(Transform i)
	{
		if (i == this.transform) {
			//GameManager.Instance.Cardmanager.BM.BrowsingList.Add();
			/*
			if (BigCard == null) 
			{
				BigCard = Instantiate(gameObject);
				Destroy (BigCard.GetComponent<Preview> ());	
				BigCard.transform.localScale = new Vector3 (3, 3, 1);
			}
			if(BigCard != null)
			{
				PreviewPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				PreviewPoint = new Vector3 (PreviewPoint.x-20, PreviewPoint.y+45, -10);
				BigCard.transform.position = PreviewPoint;
			}*/

		}if (i == null) 
		{			
			/*
			if (BigCard != null) 
			{
				Destroy (BigCard);
			}*/
		}
		
	}
	void OnDestroy()
	{
		TE.TEDObjectHR -=preview;
	}
}
