using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

public class MidiManager
{

	public MidiFileNai CurrentFileNai;
	public byte[] CurrentByteArray;
	public long CurrentPointer;

	public MidiFileNai Parse()
	{
		// TODO: solve this!
		// CurrentByteArray = File.ReadAllBytes("test.mid");
		CurrentPointer = 0;
		CurrentFileNai = new MidiFileNai();

		// MThd header -> midi format
		GetMThdHeader();
		CurrentFileNai.LogInfo();

		for (var i = 1; i <= CurrentFileNai.ChunkCount; i++)
		{
			var chunk = GetMTrkChunk(i);
			if (chunk != null)
				chunk.LogInfo();
		}

		return CurrentFileNai;
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
				CurrentFileNai.Format = MidiFileNai.MidiFormat.Single;
				break;
			case 0x01:
				CurrentFileNai.Format = MidiFileNai.MidiFormat.MutipleSync;
				break;
			case 0x02:
				CurrentFileNai.Format = MidiFileNai.MidiFormat.MutipleNotSync;
				break;
		}

		// chunk count
		CurrentFileNai.ChunkCount = GetValue(2);

		// PPQN
		CurrentFileNai.PPQN = GetValue(2);
		
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
		
		CurrentFileNai.ChunkList.Add(chunk);

		while (!GetEvent(chunk))
		{
		}

		return chunk;
	}

	private bool GetEvent(MidiChunk currentChunk)
	{
		long deltaTime;
		try
		{
			deltaTime = GetVariableLengthValue();
		}
		catch
		{
			return true;
		}
		MidiEventNai newEventNai = null;
	
		long firstByte = GetValue(1);

		switch (firstByte & 0xF0)
		{
			case 0x90:
				newEventNai = new NoteOnMidiEventNai()
				{
					TunnelNumber = firstByte & 0x0F,
					Note = GetValue(1),
					Speed = GetValue(1)
				};
				break;
			case 0x80:
				newEventNai = new NoteOffMidiEventNai()
				{
					TunnelNumber = firstByte & 0x0F,
					Note = GetValue(1),
					Speed = GetValue(1)
				};
				break;
			case 0xC0:
				newEventNai = new ChangeTimbreMidiEventNai()
				{
					TunnelNumber = firstByte & 0x0F,
					Program = GetValue(1)
				};
				break;
			case 0xB0:
				newEventNai = new ChangeVolumeMidiEventNai()
				{
					TunnelNumber = firstByte & 0x0F,
					Type = GetValue(1),
					Size = GetValue(1)
				};
				break;
			case 0xF0:
				if (firstByte != 0xFF)
					break;
				switch (GetValue(1))
				{
					// End of Chunk
					case 0x2F:
						// Debug.Log("End");
						CurrentPointer ++;
						return true;
					// Us Per quater note
					case 0x51:
						CurrentPointer ++;
						CurrentFileNai.UsPerQuaterNote = GetValue(3);
						Debug.Log("UsPerPai: " + CurrentFileNai.UsPerQuaterNote);
						break;
					case 0x58:
						GetValue(5);
						break;
					// default (other control event)
					default:
						// Debug.Log("Control");
						var length = GetValue(1);
						CurrentPointer += length;
						break;
				}
				break;
		}
		if (newEventNai != null)
		{
			newEventNai.DeltaTime = deltaTime;
			// Debug.Log(newEvent.Info());
			currentChunk.EventList.Add(newEventNai);
		}
		return false;
	}
}
