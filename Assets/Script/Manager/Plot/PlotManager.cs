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

	private PlotDisplay _plotDisplay = null;

	public void Perform()
	{
		// trigger -> pause
		GameManager.Trigger();

		// grab power from button
		GameManager.TriggerStartButtonActive();

		// call PlotDisplay
		GameManager.PlotDisplay.gameObject.SetActive(true);
		GameManager.PlotDisplay.Seek();
		GameManager.PlotDisplay.SetPlotUnit(this);
	}

	public void Die()
	{
		// trigger -> play
		GameManager.Trigger();

		// give back power
		GameManager.TriggerStartButtonActive();

		Debug.Log("PlotUnit.Die()");
		GameManager.PlotDisplay.gameObject.SetActive(false);
		GameManager.PlotDisplay.Hide();
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
			ExactTime = 2.0f
		};
		unit.List.Add(new PlotString() {Person = "纸团", Content = "欢迎来到我的游戏！当音符中心与白线中心在同一高度时，点击音符可以得到最高的得分！"});
		unit.List.Add(new PlotString() {Person = "纸团", Content = "现在仍然是Demo V1.0阶段，请体谅可能出现的各种bug……"});
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
 