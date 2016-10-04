using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Demo : MonoBehaviour
{
	public static int curScene;

	public TextTyping textTyping;

	public void Repeat()
	{
		// assign text to display
		textTyping.SetText();
	}

	public void Skip()
	{
		// show the whole text
		textTyping.Skip();
	}

	public void Prev()
	{
		// previous scene
		curScene--;

		if (curScene < 0)
			curScene = 5;

		Application.LoadLevel(curScene);
	}

	public void Next()
	{
		// next scene
		curScene++;
		
		if (curScene >= 6)
			curScene = 0;
		
		Application.LoadLevel(curScene);
	}

}
