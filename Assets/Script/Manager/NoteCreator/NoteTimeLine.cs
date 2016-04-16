using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using NAudio.Midi;
using MidiEvent = NAudio.Midi.MidiEvent;
using NoteEvent = NAudio.Midi.NoteEvent;

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

public class NoteTimeLine : TimeLine<NoteTimeLineUnit>
{

	public List<NoteTimeLineUnit> TimeLine
	{
		get
		{
			return List;
		}
	}

	// TODO: AddNote() operations muse be sequenced now. Increase its robustness!
	public void AddNote(NoteTimeLineUnit unit)
	{
		const double TOLERANCE = 1e-5;
		if (TimeLine.Count > 0 && Math.Abs(unit.ExactTime - TimeLine[TimeLine.Count - 1].ExactTime) > TOLERANCE && unit.Value == TimeLine[TimeLine.Count - 1].Value)
			return;
		// else
		TimeLine.Add(unit);
	}

	public void Create(MidiFileNai midiFileNai, float delay = 0f)
	{
		TimeLine.Clear();

		// TODO: get this ugly unit test done
		
		foreach (var chunk in midiFileNai.ChunkList)
		{
			var currentTime = 0.0f;
			foreach (var midiEvent in chunk.EventList)
			{
				var deltaTimeMSecond = midiEvent.DeltaTime*midiFileNai.UsPerQuaterNote/1000f/midiFileNai.PPQN;
				currentTime += deltaTimeMSecond/1000f;

				var noteOnEvent = midiEvent as NoteOnMidiEventNai;
				if (noteOnEvent == null)
					continue;

				// Debug.Log("Note On: " + currentTime);

				AddNote(new NoteTimeLineUnit(currentTime + delay, noteOnEvent.Note));
			}
		}
	}

	public void Create(MidiFile file, float delay = 0f)
	{
		TimeLine.Clear();
		file.Events.FlattenToOneTrack();
		var list = file.Events.GetTrackEvents(0);
		TempoEvent lastTempoEvent = null;
		float lastTempoEventRealtime = 0;

		foreach (var midiEvent in list)
		{
			float thisEventRealTime = 0f;
			if (lastTempoEvent != null)
				thisEventRealTime = (((float)midiEvent.AbsoluteTime - lastTempoEvent.AbsoluteTime)/file.DeltaTicksPerQuarterNote)*
			                          (float) lastTempoEvent.MicrosecondsPerQuarterNote / 1000000f + lastTempoEventRealtime;

			var tempoEvent = midiEvent as TempoEvent;
			if (tempoEvent != null)
			{
				lastTempoEvent = tempoEvent;
				lastTempoEventRealtime = thisEventRealTime;
			}

			if (MidiEvent.IsNoteOn(midiEvent))
			{
				// Debug.Log(thisEventRealTime);

				var noteOnEvent = midiEvent as NoteOnEvent;

				AddNote(new NoteTimeLineUnit(thisEventRealTime + delay, (noteOnEvent.NoteNumber % 5) - 2));
			}
		}
	}
}
