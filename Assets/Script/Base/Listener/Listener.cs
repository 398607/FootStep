using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Listener
{
	public Listener()
	{
	}

	public virtual void GetEvent(ListenEvent listenEvent)
	{
	}
}

