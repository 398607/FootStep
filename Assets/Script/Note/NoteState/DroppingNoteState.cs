using UnityEngine;
using System.Collections;

public class DroppingNoteState : NoteState
{
	public static float Velocity = 2f;
	public static float TimeBeforeActivate = 0.5f;
	
	public override NoteState HandleInput(Note note, NoteInput input)
	{
		return input.Clicked ? new MissNoteState() : null;
	}

	public override NoteState StateUpdate(Note note)
	{
		note.gameObject.transform.Translate(new Vector3(0, -Velocity * Time.deltaTime, 0));

		// enter activate state if 
		if (note.ExactTime - GameManager.GetTime() <= TimeBeforeActivate)
		{
			return new ActivateNoteState();
		}

		return null;
	}
}