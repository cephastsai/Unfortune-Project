using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextTypingEffect : MonoBehaviour
{
	public AnimationCurve alpha = AnimationCurve.Linear(0, 1, 1, 1);
	public AnimationCurve moveX = AnimationCurve.Linear(0, 0, 1, 0);
	public AnimationCurve moveY = AnimationCurve.Linear(0, 0, 1, 0);
	public AnimationCurve size = AnimationCurve.Linear(0, 1, 1, 1);

	// processing letters mesh procedure
	public bool SetVertex(List<UIVertex> vertexList, int index, float value)
	{
		if(index >= vertexList.Count + 6)
			return false;

		// alpha
		float curAlpha = alpha.Evaluate(value);

		for (int i = index; i < index + 6; i++)
		{
			UIVertex uiVertex = vertexList [i];
			uiVertex.color = new Color32(uiVertex.color.r, uiVertex.color.g, uiVertex.color.b, System.Convert.ToByte(curAlpha * 255));
			vertexList [i] = uiVertex;
		}

		// move
		float curMoveX = moveX.Evaluate(value);
		float curMoveY = moveY.Evaluate(value);

		for (int i = index; i < index + 6; i++)
		{
			UIVertex uiVertex = vertexList [i];
			uiVertex.position.x += curMoveX;
			uiVertex.position.y += curMoveY;
			vertexList [i] = uiVertex;
		}

		// size
		float curSize = size.Evaluate(value);

		Vector3 center = (vertexList [index].position + vertexList [index + 2].position) / 2;

		for (int i = index; i < index + 6; i++)
		{
			UIVertex uiVertex = vertexList [i];
			uiVertex.position = (uiVertex.position - center) * curSize + center;
			vertexList [i] = uiVertex;
		}

		return true;
	}
}
