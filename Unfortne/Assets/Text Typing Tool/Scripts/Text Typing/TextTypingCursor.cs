using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTypingCursor : MonoBehaviour
{
	public bool useCustomColor;
	public Color customColor = Color.black;
	public string symbols;
	public float animSpeed = 1;
	public bool random;
	public bool isActiveAlways;
	public int correctHeight = 0;

	[HideInInspector]
	public bool isActive;

	[HideInInspector]
	public RectTransform rt;

	private Text label;
	private int indexSymbol;
	private float timer;

	public void Init(Text labelTarget)
	{
		transform.SetParent(labelTarget.transform);
		transform.localScale = Vector3.one;
		name = "Cursor";

		rt = GetComponent<RectTransform>();

		GameObject go = new GameObject();
		go.transform.parent = transform;
		go.transform.localScale = Vector3.one;
		label = go.AddComponent<Text>();
		label.font = labelTarget.font;
		label.fontSize = labelTarget.fontSize;
		label.fontStyle = labelTarget.fontStyle;
		label.alignment = TextAnchor.MiddleCenter;
		if(useCustomColor)
			label.color = customColor;
		else
			label.color = labelTarget.color;

		label.rectTransform.anchoredPosition3D = Vector3.zero;
	}

	void Update()
	{
		if (!isActiveAlways && (!isActive || animSpeed == 0 || symbols.Length == 0))
		{
			label.text = "";
			return;
		}

		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			timer += 1 / animSpeed;

			if(random)
			{
				// take a random character from the list
				indexSymbol = Random.Range(0, symbols.Length);
			}
			else
			{
				// take the next character from the list
				indexSymbol++;
				if(indexSymbol >= symbols.Length)
					indexSymbol = 0;
			}

			label.text = symbols[indexSymbol].ToString();
		}

	}
}
