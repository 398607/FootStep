using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ListenEventSender
{
	private readonly List<Listener> _listenerList = new List<Listener>();

	public void SendEvent(ListenEvent listenEvent)
	{
		foreach (var listener in _listenerList)
		{
			listener.GetEvent(listenEvent);
		}
	}

	public void AddListener(Listener listener)
	{
		_listenerList.Add(listener);
	}

}
