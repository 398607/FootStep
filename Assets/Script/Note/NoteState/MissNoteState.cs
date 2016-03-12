using UnityEngine;
using System.Collections;

public class MissNoteState : NoteState
{
	public override void Enter(Note note)
	{
		GameManager.ScoreListener().GetEvent(new ScoreEvent(-1f));
	}

	public override NoteState StateUpdate(Note note)
	{
		note.Animator.SetInteger("Disappear", 1);
		return new DisappearNoteState(GameManager.GetTime());
	}
}
