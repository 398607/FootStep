using UnityEngine;
using System.Collections;

public class DisappearNoteState : NoteState
{
	public static float Velocity = 20f;
	public static float FlyTime = 5f;

	private readonly float _enterTime = 0f;
	private float _angle = 0f;

	public DisappearNoteState(float enterTime)
	{
		_enterTime = enterTime;
	}

	public override void Enter(Note note)
	{
		note.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0.1f);

		// ramdomized fly-away angle
		_angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
	}

	public override NoteState StateUpdate(Note note)
	{
		if (GameManager.GetTime() - _enterTime > FlyTime)
		{
			Disappear(note);
		}

		// fly away
		note.gameObject.transform.Translate(new Vector3(Mathf.Cos(_angle) * Velocity * Time.deltaTime, Mathf.Sin(_angle) * Velocity * Time.deltaTime, 0));
		return null;
	}

	private void Disappear(Note note)
	{
		// kill Note
		note.gameObject.SetActive(false);
		GameObject.Destroy(note.gameObject);
	}
}