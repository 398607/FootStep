using UnityEngine;
using System.Collections;

class ScoreEvent : ListenEvent
{
	public readonly float Score;

	public ScoreEvent(float score = 0f)
	{
		Score = score;
	}
}
