using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlotString
{
	public string Person;
	public string Content;
}

public class PlotUnit
{
	public float ExactTime;
	public List<PlotString> List = new List<PlotString>();

	public void Perform()
	{
		// grab power from button
		GameManager.TriggerStartButtonActive();

		// trigger -> pause
		GameManager.Trigger();
		foreach (var plotString in List)
		{
			Debug.Log(plotString.Person + ": " + plotString.Content);
		}

		// give back power
		GameManager.TriggerStartButtonActive();
	}
}

public class PlotTimeLine : TimeLine<PlotUnit>
{
}

public class PlotManager
{
	public PlotTimeLine Line = new PlotTimeLine();

	public PlotManager()
	{
		PlotUnit unit = new PlotUnit()
		{
			ExactTime = 3.0f
		};
		unit.List.Add(new PlotString() {Person = "Nagizero", Content = "欢迎来到我的游戏！当音符中心与白线中心在同一高度时，点击音符可以得到最高的得分！"});
		Line.Add(unit);
	}

	public void Update()
	{
		if (Line.Over())
			return;
		if (Line.Next().ExactTime <= GameManager.GetTime())
		{
			Line.Next().Perform();
			Line.Move();
		}
	}
}
 