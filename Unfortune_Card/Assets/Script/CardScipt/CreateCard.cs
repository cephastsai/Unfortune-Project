using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CreateCard : MonoBehaviour {

	public List<Sprite> CardImageList = new List<Sprite>();


	public void init(){
		Sprite[] tempsprite = Resources.LoadAll<Sprite>(@"CardImage/CardImageSprite");
		CardImageList.AddRange(tempsprite);

		//Createcard(2);
	}

	public GameObject Createcard(int cardKind){

		//card top
		GameObject CardTop = new GameObject("card");
		//CardTop.AddComponent<RectTransform>();	
		CardTop.AddComponent<SpriteRenderer>().sprite = CardImageList[cardKind];
		//CardTop.AddComponent<Image>().sprite = CardImageList[cardKind];
		CardTop.transform.localScale = new Vector3(0.1f, 0.1f, 1);

		//card back
		GameObject CardBack = new GameObject("cardback");
		CardBack.AddComponent<SpriteRenderer>().sprite = CardImageList[10];
		CardBack.transform.localScale = new Vector3(0.1f, 0.1f, 1);
		CardBack.transform.SetParent(CardTop.transform);
		CardBack.transform.localPosition = new Vector3(0, 0, 0.005f);

		return CardTop;
	}

}
