using UnityEngine;
using System.Collections;

public class NoteCreator : MonoBehaviour
{
	// in inspector
	public Note NotePrefab;

	private readonly NoteTimeLine _timeLine = new NoteTimeLine();
	private int _nextNote = 0;

	public static float TimeBeforeCreate = 10.0f / DroppingNoteState.Velocity;

	private Note NewNote(int number, NoteTimeLineUnit unit)
	{
		// TODO: this convert function should be seriously considered.
		var note = Instantiate(NotePrefab, new Vector3(((5 + unit.Value)%11)-5, 5, 0), Quaternion.identity) as Note;
		note.ExactTime = unit.ExactTime;
		note.Number = number;
		return note;
	}

	public void LoadMidiFile(MidiFile midiFile, float delay = 0f)
	{
		_timeLine.Create(midiFile, delay);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_nextNote >= _timeLine.TimeLine.Count)
			return;

		while (_nextNote < _timeLine.TimeLine.Count && GameManager.GetTime() > _timeLine.TimeLine[_nextNote].ExactTime - TimeBeforeCreate)
		{
			// create that note!
			NewNote(_nextNote, _timeLine.TimeLine[_nextNote]);
			_nextNote++;
		}
	}
}
