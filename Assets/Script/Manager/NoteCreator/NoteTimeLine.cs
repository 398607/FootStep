using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteTimeLineUnit
{
	public float ExactTime;

	public long Value;

	public NoteTimeLineUnit(float exactTime, long value)
	{
		ExactTime = exactTime;
		Value = value;
	}
}

public class NoteTimeLine
{
	public List<NoteTimeLineUnit> TimeLine = new List<NoteTimeLineUnit>();

	// TODO: AddNote() operations muse be sequenced now. Increase its robustness!
	public void AddNote(NoteTimeLineUnit unit)
	{
		TimeLine.Add(unit);
	}

	public void Create(MidiFile midiFile, float delay = 0f)
	{
		TimeLine.Clear();

		// TODO: get this ugly unit test done
		var chunk = midiFile.ChunkList[1];

		var currentTime = 0.0f;
		foreach (var midiEvent in chunk.EventList)
		{
			var deltaTimeMSecond = midiEvent.DeltaTime*midiFile.UsPerQuaterNote/1000f/midiFile.PPQN;
			currentTime += deltaTimeMSecond / 1000f;

			var noteOnEvent = midiEvent as NoteOnMidiEvent;
			if (noteOnEvent == null)
				continue;

			Debug.Log("Note On: " + currentTime);

			AddNote(new NoteTimeLineUnit(currentTime + delay, noteOnEvent.Note));
		}
	}
}
