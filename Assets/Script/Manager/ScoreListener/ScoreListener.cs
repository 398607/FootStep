using UnityEngine;
using System.Collections;

public class ScoreListener : Listener
{
	public float TotalScore = 0f;
	private readonly UpdateScoreSender _updateScoreSender = new UpdateScoreSender();

	public void AddScoreBoard(ScoreBoard scoreBoard)
	{
		_updateScoreSender.AddListener(scoreBoard.UpdateScoreListener);
	}

	public override void GetEvent(ListenEvent listenEvent)
	{
		var scoreEvent = listenEvent as ScoreEvent;
		if (scoreEvent == null)
		{
			Debug.Log("ScoreListener.GetEvent(): null ScoreEvent");
			return;
		}
		TotalScore += scoreEvent.Score;
		_updateScoreSender.SendEvent(new ScoreEvent(TotalScore));
	}
}

