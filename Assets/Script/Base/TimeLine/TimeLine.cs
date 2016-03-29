using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeLine<T>
{
	public List<T> List = new List<T>();
	public int Current = 0;

	public void Add(T unit)
	{
		List.Add(unit);
	}

	public void Move()
	{
		Current++;
	}

	public T Next()
	{
		return List[Current];
	}

	public bool Over()
	{
		return Current >= List.Count;
	}
}