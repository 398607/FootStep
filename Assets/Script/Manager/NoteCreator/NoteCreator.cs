using UnityEngine;
using System.Collections;

public class NoteCreator : MonoBehaviour
{
	// in inspector
	public Note NotePrefab;

	private NoteTimeLine _timeLine = new NoteTimeLine();
	private int _nextNote = 0;

	public static float TimeBeforeCreate = 10.0f / DroppingNoteState.Velocity;

	private Note NewNote(int number, float exactTime)
	{
		Note note = Instantiate(NotePrefab, new Vector3(Random.Range(-5, 5), 5, 0), Quaternion.identity) as Note;
		note.ExactTime = exactTime;
		note.Number = number;
		return note;
	}

	// Use this for initialization
	void Start () {

		// TODO : destroy this ugly unitTest!
		for (int i = 0; i < 50; i++)
		{
			_timeLine.AddNote(6 + i + Random.Range(0f, 1f));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_nextNote >= _timeLine.TimeLine.Count)
			return;

		while (_nextNote < _timeLine.TimeLine.Count && GameManager.GetTime() > _timeLine.TimeLine[_nextNote].ExactTime - TimeBeforeCreate)
		{
			// create that note!
			NewNote(_nextNote, _timeLine.TimeLine[_nextNote].ExactTime);
			_nextNote++;
		}
	}
}
