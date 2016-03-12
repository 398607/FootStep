using UnityEngine;
using System.Collections;

public class InvokedNoteState : NoteState
{
	private readonly NoteInput _enterInput;
	private static readonly float TimeBeforeActivate = DroppingNoteState.TimeBeforeActivate;
	
	public InvokedNoteState(NoteInput input)
	{
		_enterInput = input;
	}

	public override void Enter(Note note)
	{
		var score = (1f - Mathf.Abs(_enterInput.ClickTime - note.ExactTime)/TimeBeforeActivate);
		GameManager.ScoreListener().GetEvent(new ScoreEvent(score));
	}

	public override NoteState StateUpdate(Note note)
	{
		return new DisappearNoteState(GameManager.GetTime(), true);
	}
}