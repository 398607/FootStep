using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Cecil.Cil;


public class MidiManager
{

	public MidiFile CurrentFile;
	public byte[] CurrentByteArray;
	public long CurrentPointer;

	public MidiFile Parse()
	{
		
		CurrentByteArray = File.ReadAllBytes("test.mid");
		CurrentPointer = 0;
		CurrentFile = new MidiFile();

		// MThd header -> midi format
		GetMThdHeader();
		CurrentFile.LogInfo();

		for (var i = 1; i <= CurrentFile.ChunkCount; i++)
		{
			var chunk = GetMTrkChunk(i);
			if (chunk != null)
				chunk.LogInfo();
		}

		return CurrentFile;
	}

	// get int value from _currentByteArray[CurrentPointer, CurrentPointer + length)
	private long GetValue(int length)
	{
		var baseValue = 1;
		var totalValue = 0;
		for (var i = CurrentPointer + length - 1; i >= CurrentPointer; i--)
		{
			totalValue += baseValue * CurrentByteArray[i];
			baseValue *= 0x100;
		}
		CurrentPointer += length;
		return totalValue;
	}

	// get a Variable Length Quantity (starts at CurrentPointer)
	private long GetVariableLengthValue()
	{
		var value = GetValue(1);
		if ((value & 0x80) == 0)
			return value;

		long newByte = 0;
		value &= 0x7F;

		do
		{
			newByte = GetValue(1);
			value = (value << 7) + (newByte & 0x7F);
		} while ((newByte & 0x80) != 0);

		// Debug.Log(value);

		return value;
	}

	// if starts with correct symbolVariable Length Quantities 
	private bool StartWith(byte[] expect)
	{
		if (CurrentPointer + expect.Length >= CurrentByteArray.Length)
			return false;
		return !expect.Where((t, i) => CurrentByteArray[CurrentPointer + i] != t).Any();
	}

	private bool GetMThdHeader()
	{
		// MThd
		if (!StartWith(new byte[] {0x4d, 0x54, 0x68, 0x64}))
			return false;

		CurrentPointer += 9;
		switch (GetValue(1))
		{
			case 0x00:
				CurrentFile.Format = MidiFile.MidiFormat.Single;
				break;
			case 0x01:
				CurrentFile.Format = MidiFile.MidiFormat.MutipleSync;
				break;
			case 0x02:
				CurrentFile.Format = MidiFile.MidiFormat.MutipleNotSync;
				break;
		}

		// chunk count
		CurrentFile.ChunkCount = GetValue(2);

		// PPQN
		CurrentFile.PPQN = GetValue(2);
		
		return true;
	}

	private MidiChunk GetMTrkChunk(int number)
	{
		// MTrk
		if (!StartWith(new byte[] {0x4d, 0x54, 0x72, 0x6b}))
			return null;

		CurrentPointer += 4;

		var chunk = new MidiChunk
		{
			Number = number,
			Count = GetValue(4)
		};
		
		CurrentFile.ChunkList.Add(chunk);

		while (!GetEvent(chunk))
		{
		}

		return chunk;
	}

	private bool GetEvent(MidiChunk currentChunk)
	{
		var deltaTime = GetVariableLengthValue();
		MidiEvent newEvent = null;

		long firstByte = GetValue(1);

		switch (firstByte & 0xF0)
		{
		case 0x90:
			newEvent = new NoteOnMidiEvent()
			{
				TunnelNumber = firstByte & 0x0F,
				Note = GetValue(1),
				Speed = GetValue(1)
			};
			break;
		case 0x80:
			newEvent = new NoteOffMidiEvent()
			{
				TunnelNumber = firstByte & 0x0F,
				Note = GetValue(1),
				Speed = GetValue(1)
			};
			break;
		case 0xC0:
			newEvent = new ChangeTimbreMidiEvent()
			{
				TunnelNumber = firstByte & 0x0F,
				Program = GetValue(1)
			};
			break;
		case 0xB0:
			newEvent = new ChangeVolumeMidiEvent()
			{
				TunnelNumber = firstByte & 0x0F,
				Type = GetValue(1),
				Size = GetValue(1)
			};
			break;
		case 0xF0:
			if (firstByte != 0xFF)
				break;
			switch (GetValue(1)) // End of Chunk
			{
			case 0x2F:
				Debug.Log("End");
				CurrentPointer ++;
				return true;
			case 0x51:
				CurrentPointer ++;
				CurrentFile.UsPerQuaterNote = GetValue(3);
				Debug.Log("UsPerPai: " + CurrentFile.UsPerQuaterNote);
				break;
			default:
				Debug.Log("Control");
				var length = GetValue(1);
				CurrentPointer += length;
				break;
			}
			break;
		}
		if (newEvent != null)
		{
			newEvent.DeltaTime = deltaTime;
			Debug.Log(newEvent.Info());
			currentChunk.EventList.Add(newEvent);
		}
		return false;
	}
}
