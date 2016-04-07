using UnityEngine;
using System.Collections;

public class DisappearNoteState : NoteState
{
	public static float Velocity = 20f;
	public static float BreakTime = 0.5f;
	public static float FlyTime = 0.02f;

	private readonly float _enterTime = 0f;
	private float _angle = 0f;
	private bool _flyAway = false;

	public DisappearNoteState(float enterTime, bool FlyAway = false)
	{
		_enterTime = enterTime;
		_flyAway = FlyAway;
	}

	public override void Enter(Note note)
	{
		// note.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0.1f);

		// ramdomized fly-away angle (towards "up")
		_angle = UnityEngine.Random.Range(0, Mathf.PI);

		GameManager.Instance.Notes.Remove(note);
	}

	private float KeepTime()
	{
		return _flyAway ? FlyTime : BreakTime;
	}

	public override NoteState StateUpdate(Note note)
	{
		if (GameManager.GetTime() - _enterTime > KeepTime())
		{
			Disappear(note);
		}

		// fly away
		if (_flyAway)
		{
			note.gameObject.transform.Translate(new Vector3(Mathf.Cos(_angle)*Velocity*Time.deltaTime,
				Mathf.Sin(_angle)*Velocity*Time.deltaTime, 0));
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