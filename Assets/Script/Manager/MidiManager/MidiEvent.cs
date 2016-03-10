using UnityEngine;
using System.Collections;


public class MidiEvent
{
	public long DeltaTime;

	public virtual string Info()
	{
		return "DeltaTime: " + DeltaTime;
	}
}

public class WithTunnelMidiEvent : MidiEvent
{
	public long TunnelNumber;

	public override string Info()
	{
		return base.Info() + " TunnelNumber: " + TunnelNumber;
	}
}

public class NoteOnMidiEvent : WithTunnelMidiEvent
{
	public long Note;
	public long Speed;

	public override string Info()
	{
		return base.Info() + " OnNote: " + Note + " Speed: " + Speed;
	}
}

public class NoteOffMidiEvent : WithTunnelMidiEvent
{
	public long Note;
	public long Speed;

	public override string Info()
	{
		return base.Info() + " OffNote: " + Note + " Speed: " + Speed;
	}
}

public class ChangeTimbreMidiEvent : WithTunnelMidiEvent
{
	public long Program;

	public override string Info()
	{
		return base.Info() + " Program: " + Program;
	}
}

public class ChangeVolumeMidiEvent : WithTunnelMidiEvent
{
	public long Type; // 0x07 or 0x39
	public long Size;

	public override string Info()
	{
		return base.Info() + " Type: " + Type + " Size: " + Size;
	}
}