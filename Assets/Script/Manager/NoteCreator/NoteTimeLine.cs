using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteTimeLineUnit
{
	public float ExactTime;

	public NoteTimeLineUnit(float exactTime)
	{
		ExactTime = exactTime;
	}
}

public class NoteTimeLine
{
	public List<NoteTimeLineUnit> TimeLine = new List<NoteTimeLineUnit>();

	// TODO: AddNote() operations muse be sequenced now. Increase its robustness!
	public void AddNote(float exactTime)
	{
		TimeLine.Add(new NoteTimeLineUnit(exactTime));
	}
}
