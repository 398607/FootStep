using UnityEngine;
using System.Collections;

public class DisappearNoteState : NoteState
{
	public static float BreakTime = 0f;

	private readonly float _enterTime = 0f;
	private float _angle = 0f;

	public DisappearNoteState(float enterTime)
	{
		_enterTime = enterTime;
	}

	public override void Enter(Note note)
	{
		GameManager.Instance.Notes[note.Number + 2].Remove(note);
	}

	private float KeepTime()
	{
		return BreakTime;
	}

	public override NoteState StateUpdate(Note note)
	{
		if (GameManager.GetTime() - _enterTime > KeepTime())
		{
			Disappear(note);
		}
		
		return null;
	}

	private void Disappear(Note note)
	{
		// kill Note
		note.gameObject.SetActive(false);
		GameObject.Destroy(note.gameObject);
	}
}