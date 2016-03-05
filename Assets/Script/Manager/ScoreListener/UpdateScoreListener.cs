using UnityEngine;
using System.Collections;

public class UpdateScoreListener : Listener
{
	private ScoreBoard _scoreBoard = null;

	public UpdateScoreListener(ScoreBoard scoreBoard)
	{
		_scoreBoard = scoreBoard;
	}

	public override void GetEvent(ListenEvent listenEvent)
	{
		var scoreEvent = listenEvent as ScoreEvent;
		if (scoreEvent == null)
			return;
		_scoreBoard.UpdateScore(scoreEvent.Score);
	}
}
