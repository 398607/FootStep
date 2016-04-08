using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlotDisplay : MonoBehaviour
{

	private Text _contentText;
	private Text _personText;
	private Button _nextButton;

	private PlotUnit _plotUnit = null;
	private int _currentUnitNumber = 0;

	void Start()
	{
	}

	public void Init()
	{
		Text[] twoText = GetComponentsInChildren<Text>();
		foreach (var text in twoText)
		{
			if (text.name == "ContentText")
			{
				_contentText = text;
			}
			if (text.name == "PersonText")
			{
				_personText = text;
			}
		}
		_nextButton = GameObject.Find("NextButton").GetComponent<Button>();
		_nextButton.onClick.AddListener(NextPlotString);

		Debug.Log("PlotUnit Init() Done");
	}

	public void Hide()
	{
		_nextButton.gameObject.SetActive(false);
	}

	public void Seek()
	{
		_nextButton.gameObject.SetActive(true);
	}

	// set (a new) PlotUnit for this plotdisplay
	public void SetPlotUnit(PlotUnit unit)
	{
		_currentUnitNumber = 0;

		_plotUnit = unit;

		SetCurrentPlot(_currentUnitNumber);

		_nextButton.GetComponentInChildren<Text>().text = "Next";
	}

	// set PlotString by number
	void SetCurrentPlot(int number)
	{
		SetCurrentPlot(_plotUnit.List[number]);
	}

	// set PlotString by plotString instance
	void SetCurrentPlot(PlotString plotString)
	{
		_contentText.text = plotString.Content;
		_personText.text = plotString.Person;
	}

	// next PlotString
	public void NextPlotString()
	{
		Debug.Log("Asked NextPlotString()");

		if (_currentUnitNumber + 1 >= _plotUnit.List.Count)
		{
			Debug.Log("ask Die() from PlotDisplay");
			_plotUnit.Die();
			return;
		}

		_currentUnitNumber ++;
		SetCurrentPlot(_currentUnitNumber);

		// final text
		if (_currentUnitNumber + 1 == _plotUnit.List.Count)
			_nextButton.GetComponentInChildren<Text>().text = "End";
	}
}
