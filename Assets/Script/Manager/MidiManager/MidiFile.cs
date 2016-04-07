using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MidiFileNai
{
	public enum MidiFormat
	{
		Single,
		MutipleSync,
		MutipleNotSync
	}
	public MidiFormat Format;

	public long ChunkCount;

	public long PPQN;

	public long UsPerQuaterNote;

	public List<MidiChunk> ChunkList = new List<MidiChunk>();

	public void LogInfo()
	{
		Debug.Log("Midi file Info: " + Format.ToString() + " " + ChunkCount + " " + PPQN);
	}
}
