using UnityEngine;
using System.Collections;


public class MidiEventNai
{
	public long DeltaTime;

	public virtual string Info()
	{
		return "DeltaTime: " + DeltaTime;
	}
}

public class WithTunnelMidiEventNai : MidiEventNai
{
	public long TunnelNumber;

	public override string Info()
	{
		return base.Info() + " TunnelNumber: " + TunnelNumber;
	}
}

public class NoteOnMidiEventNai : WithTunnelMidiEventNai
{
	public long Note;
	public long Speed;

	public override string Info()
	{
		return base.Info() + " OnNote: " + Note + " Speed: " + Speed;
	}
}

public class NoteOffMidiEventNai : WithTunnelMidiEventNai
{
	public long Note;
	public long Speed;

	public override string Info()
	{
		return base.Info() + " OffNote: " + Note + " Speed: " + Speed;
	}
}

public class ChangeTimbreMidiEventNai : WithTunnelMidiEventNai
{
	public long Program;

	public override string Info()
	{
		return base.Info() + " Program: " + Program;
	}
}

public class ChangeVolumeMidiEventNai : WithTunnelMidiEventNai
{
	public long Type; // 0x07 or 0x39
	public long Size;

	public override string Info()
	{
		return base.Info() + " Type: " + Type + " Size: " + Size;
	}
}