using UnityEngine;
using System.Collections;

public abstract class NoteState
{
	public virtual void Enter(Note note)
	{
		return;
	}

	public virtual NoteState StateUpdate(Note note)
	{
		return null;
	}

	public virtual NoteState HandleInput(Note note, NoteInput input)
	{
		return null;
	}
}