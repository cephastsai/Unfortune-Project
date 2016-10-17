using UnityEngine;
using System.Collections;

public class Browsing : MonoBehaviour {

	private Card myCard;
	private GameObject BigCard;
	public GameObject Maincamera;
	public Vector3 PreviewPoint;

	void Start () {
		//init
		myCard = GetComponent<Card>();
		Maincamera = GameObject.Find ("Main Camera");
		Camera camera = Maincamera.GetComponent<Camera>();
		//touch list
		GameManager.Instance.TE.TEDObjectHR += BrowsingCard;
	}

	void Update () {
		//destory self
		if(myCard.Place != CardManager.cardSection.Hand && myCard.Place != CardManager.cardSection.Table){			
			Destroy(this);
		}
	}

	public void CreateBigCard(Transform target){
		if(target == transform){	
			/*
			BigCard = Instantiate(gameObject);
			Destroy(BigCard.GetComponent<Browsing>());
			BigCard.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			PreviewPoint = new Vector3 (BigCard.transform.position.x, BigCard.transform.position.y, 0);
			BigCard.transform.localScale = new Vector3 (3, 3, 1);
			BigCard.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			BigCard.transform.position = PreviewPoint;*/

			//print(BigCard.GetComponent<SpriteRenderer>().bounds.size.x);
		}
	}


	public void BrowsingCard(Transform target){
		if (target == transform){			
			GameManager.Instance.Cardmanager.BM.setcard(myCard);
		}else{			
			GameManager.Instance.Cardmanager.BM.removecard(myCard);
		}
	}

	void OnDestroy(){
		GameManager.Instance.TE.TEDObjectCR -= CreateBigCard;
		GameManager.Instance.TE.TEDObjectHR -=  BrowsingCard;
	}
}
