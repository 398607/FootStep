using UnityEngine;
using System.Collections;

public class NoteCreator
{
	// in inspector
	public Note NotePrefab = GameManager.Instance.NotePrefab;

	private readonly NoteTimeLine _timeLine = new NoteTimeLine();

	public static float TimeBeforeCreate = 9.0f / DroppingNoteState.Velocity;

	private Note NewNote(int number, NoteTimeLineUnit unit)
	{
		// TODO: this convert function should be seriously considered.
		var note = GameObject.Instantiate(NotePrefab, new Vector3(((5 + unit.Value)%11)-5, 5, 0), Quaternion.identity) as Note;
		note.ExactTime = unit.ExactTime;
		note.Number = number;
		return note;
	}

	public void LoadMidiFile(MidiFile midiFile, float delay = 0f)
	{
		_timeLine.Create(midiFile, delay);
	}
	
	// Update is called once per frame
	public void Update ()
	{
		if (_timeLine.Over())
			return;

		while (!_timeLine.Over() && GameManager.GetTime() > _timeLine.Next().ExactTime - TimeBeforeCreate)
		{
			// create that note!
			NewNote(_timeLine.Current, _timeLine.Next());
			_timeLine.Move();
		}
	}
}
