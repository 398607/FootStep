using UnityEngine;
using System.Collections;

public class ActivateNoteState : NoteState
{
	private SpriteRenderer _noteRenderer = null;
	private static readonly float TimeBeforeActivate = DroppingNoteState.TimeBeforeActivate;

	public override void Enter(Note note)
	{
		_noteRenderer = note.GetComponentInChildren<SpriteRenderer>();
	}

	public override NoteState HandleInput(Note note, NoteInput input)
	{
		return input.Clicked ? new InvokedNoteState(input) : null;
	}

	public override NoteState StateUpdate(Note note)
	{
		// TODO: go to Miss if not clicked for too long
		var percent = (note.ExactTime - GameManager.GetTime()) / TimeBeforeActivate;

		if (percent >= 0f)
		{
			_noteRenderer.color = new Color(percent, percent, percent);
		}
		else if (percent > -1f)
		{
			_noteRenderer.color = new Color(0f, 0f, 0f, percent + 1);
		}
		else // if (percent <= -1f)
		{
			return new MissNoteState();
		}

		return null;
	}
}
