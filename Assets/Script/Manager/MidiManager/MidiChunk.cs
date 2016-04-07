using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MidiChunk
{
	public int Number;
	public long Count;

	public List<MidiEventNai> EventList = new List<MidiEventNai>();

	public void LogInfo()
	{
		Debug.Log("Midi Chunk Info: " + Number + " " + Count);
	}
}
