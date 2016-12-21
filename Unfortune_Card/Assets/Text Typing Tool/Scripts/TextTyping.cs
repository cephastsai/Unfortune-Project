using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class TextTyping : BaseMeshEffect
{
	public static TextTypingType[] types;
	public static string[] typeNames;

	public int curTypeID;

	private TextTypingType curType;
	private Text label;
	private float curIndexLetter;
	private List<DC_TextTypingLetter> letters;
	private int curIndex;
	private TextTypingCursor curCursor;	
	private int speedTyping;

	private string textLabel;
	public bool endTyping;

	void Awake()
	{
		label = GetComponent<Text>();

		Init();
	}

	// presets initialization
	public static void InitTypes()
	{
		types = Resources.LoadAll<TextTypingType>("");
		if (types == null || types.Length == 0)
			return;
			
		typeNames = new string[types.Length];
		for (int i=0; i<types.Length; i++)
		{
			types [i].Init();
			typeNames [i] = types [i].m_name;
		}
	}

	void Init()
	{
		if (types == null)
		{
			InitTypes();

			if (types == null)
				return;
		}

		curType = types [curTypeID];
	}

	public void SetType(TextTypingType type)
	{
		curType = type;
	}

	// add the same text to label
	public void SetText()
	{
		SetText(label.text);
	}

	// adding text to label
	public void SetText(string value)
	{
		endTyping = false;

		value = value.Replace("<br>", "\n");
		textLabel = value;
		label.text = value;

		// the formation of a list of letters
		letters = new List<DC_TextTypingLetter>();
		curIndexLetter = 0;

		bool added = true;
		int index = -1;
		int indexVertex = -1;
		foreach (char letter in value)
		{
			indexVertex++;
			if (added && letter == '<')
				added = false;

			if (!added && letter == '>')
			{
				added = true;
				continue;
			}

			if (!added)
				continue;

			index++;

			DC_TextTypingLetter letterData = new DC_TextTypingLetter();
			letterData.letter = letter;
			letterData.index = index;
			letterData.indexVertex = indexVertex;
			letters.Add(letterData);
		}

		curIndex = -1;

		// calculate the current printing speed
		speedTyping = curType.speedTyping + UnityEngine.Random.Range(-curType.speedTypingRandom, curType.speedTypingRandom);

		// forming cursor
		curCursor = transform.GetComponentInChildren<TextTypingCursor>();
		if (curCursor != null)
			Destroy(curCursor.gameObject);

		if (curType.cursor != null)
		{
			curCursor = Instantiate(curType.cursor);
			curCursor.Init(label);
			curCursor.isActive = true;
		}

		UpdateText();
	}

	// procedure instantaneous output of all text
	public void Skip()
	{
		curIndexLetter = letters.Count;

		foreach (DC_TextTypingLetter letter in letters)
		{
			letter.value = 1;
		}

		if (curCursor != null)
		{
			curCursor.isActive = false;
			SetCursorPosition(letters.Count - 1);
		}

		UpdateText();
	}

	void Update()
	{
		if (!Application.isPlaying)
			return;

		if (letters != null && curIndex > letters.Count && !endTyping)
		{
			// end typing
			endTyping = true;
			CallEntry(GetComponent<EventTrigger>());
			return;
		}

		if (label != null && textLabel != label.text)
		{
			// the text has been updated, run the first effect
			SetText(label.text);
		}

		if (letters == null || letters.Count == 0)
			return;

		if (curIndex + 2 >= 0 && curIndex + 2 < letters.Count && letters [curIndex + 2].vertex.position.magnitude == 0)
			// exit if the current letter is not in the display area
			return;

		curIndexLetter += Time.deltaTime * speedTyping / 60f;

		if (curIndex != (int)curIndexLetter)
		{
			// new letter
			curIndex = (int)curIndexLetter;
			speedTyping = curType.speedTyping + UnityEngine.Random.Range(-curType.speedTypingRandom, curType.speedTypingRandom);

			// sound
			if (curIndex < letters.Count)
				curType.PlaySound(letters [curIndex]);

			// cursor
			if (curCursor != null && curIndex + 1 < letters.Count)
			{
				SetCursorPosition(curIndex + 1);
			}

			if (curCursor != null && curIndex + 1 >= letters.Count)
			{
				// end of the text, turn off the cursor
				curCursor.isActive = false;
			}

		}

		bool changed = false;

		// changing the level of effect of letters
		foreach (DC_TextTypingLetter letter in letters)
		{
			if (curIndex >= letter.index && letter.value < 1)
			{
				// changing letters
				changed = true;

				if (curType.timeEffect == 0)
					letter.value = 1;
				else
					letter.value += Time.deltaTime / curType.timeEffect;
				
				if (letter.value > 1)
					letter.value = 1;
			}
		}

		if (changed)
		{
			UpdateText();
		}

	}

	void UpdateText()
	{
		// to update the color change
		Color32 col = label.color;
		label.color = new Color32();
		label.color = col;
	}

	// We set the cursor position
	void SetCursorPosition(int index)
	{
		DC_TextTypingLetter letter = letters [index];
		Vector3 pos = letter.vertex.position;
		
		pos.y = 0;
		UILineInfo lastLine = new UILineInfo();

		IList<UILineInfo> lines = label.cachedTextGenerator.lines;
		foreach (UILineInfo info in lines)
		{
			if (info.startCharIdx > letter.indexVertex)
				break;

			lastLine = info;
			pos.y = lastLine.topY;
		}
		
		pos.y -= lastLine.height / 2 + curCursor.correctHeight;

		if(letter.indexVertex >= 0 && letter.indexVertex < label.cachedTextGenerator.characters.Count)
			pos.x += label.cachedTextGenerator.characters [letter.indexVertex].charWidth / 2;

		curCursor.transform.localPosition = pos;
	}

	// the procedure for updating letters effect
	public override void ModifyMesh(VertexHelper vh)
	{
		if (!IsActive() || letters == null)
		{
			return;
		}

		List<UIVertex> vertexList = new List<UIVertex>();
		vh.GetUIVertexStream(vertexList);

		foreach (DC_TextTypingLetter letter in letters)
		{
			int index = letter.indexVertex * 6;
			if (index >= vertexList.Count)
				break;

			letter.vertex = vertexList [index];

			// assign effect
			curType.effect.SetVertex(vertexList, index, letter.value);
		}

		vh.AddUIVertexTriangleStream(vertexList);
	}

	void CallEntry(EventTrigger trigger)
	{
		if (trigger == null || trigger.triggers == null)
			return;

		foreach (EventTrigger.Entry entry in trigger.triggers)
		{
			if(entry.eventID != EventTriggerType.Submit)
				continue;

			entry.callback.Invoke(null);
		}
	}

}

[System.Serializable]
public class DC_TextTypingLetter
{
	public int index;
	public int indexVertex;
	public char letter;
	public float value;
	public UIVertex vertex;
}

[System.Serializable]
public class DC_Sound
{
	public AudioClip clip;
	public string letters;
}

