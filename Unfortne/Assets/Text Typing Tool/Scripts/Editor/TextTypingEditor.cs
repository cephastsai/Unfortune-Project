using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TextTyping))]
public class TextTypingEditor : Editor
{
	private TextTyping someClass;
	private int countTypes;

	void Awake()
	{
		someClass = target as TextTyping;
	}

	public override void OnInspectorGUI ()
	{
		TextTyping.InitTypes();

		if (TextTyping.types == null)
			return;

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Type Typing");
		someClass.curTypeID = EditorGUILayout.Popup(someClass.curTypeID, TextTyping.typeNames);
		EditorGUILayout.EndHorizontal();

		someClass.SetType(TextTyping.types[someClass.curTypeID]);
	}
}
