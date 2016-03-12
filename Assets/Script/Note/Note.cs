using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Networking;

public class Note : MonoBehaviour
{
	public int Number;
	public float ExactTime;

	private Timer _timer;

	private NoteState _state;

	public Animator Animator;

	// when get related invoked
	private void Invoked()
	{
		if (!GameManager.Playing())
			return;

		var newState = _state.HandleInput(this, new NoteInput(true, _timer.Time));

		// enter new NoteState
		if (newState != null)
		{
			_state = newState;
			_state.Enter(this);
		}
	}

	// When clicked ( mouse down or key storke)
	// called by unity itself
	private void OnMouseDown()
	{
		Invoked();
	}

	// Use this for initialization
	private void Start()
	{
		_timer = GameManager.Instance.Timer;

		Animator = GetComponent<Animator>();

		_state = new DroppingNoteState();
		_state.Enter(this);
		
	}

	// Update is called once per frame
	private void Update()
	{
		if (!GameManager.Playing())
			return;

		var newState = _state.StateUpdate(this);

		// enter new NoteState
		if (newState != null)
		{
			_state = newState;
			_state.Enter(this);
		}
	}
}

