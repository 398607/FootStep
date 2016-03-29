using UnityEngine;
using System.Collections;

public class ActivateNoteState : NoteState
{
	private SpriteRenderer _noteRenderer = null;
	private static readonly float TimeBeforeActivate = DroppingNoteState.TimeBeforeActivate;
	public static float Velocity = DroppingNoteState.Velocity;

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
		// feature: keep droping when ready to be invoked
		note.gameObject.transform.Translate(new Vector3(0, -Velocity * Time.deltaTime, 0));
		
		var percent = (note.ExactTime - GameManager.GetTime()) / TimeBeforeActivate;

		if (percent >= 0f)
		{
			// useless
			_noteRenderer.color = new Color(percent, percent, percent);
		}
		else if (percent > -1f)
		{
			_noteRenderer.color = new Color(0f, 0f, 0f);
		}
		if (percent <= -1f)
		{
			return new MissNoteState();
		}

		return null;
	}
}
