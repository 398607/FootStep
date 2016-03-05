using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;

public class TimerScreen : MonoBehaviour
{
	private Timer _timer;

	// Use this for initialization
	void Start ()
	{
		// bulid timer
		_timer = GameManager.Instance.Timer;
		StartCoroutine("UpdateTime");
	}

	private IEnumerator UpdateTime()
	{
		while (true)
		{
			gameObject.GetComponentInParent<Text>().text = _timer.Time.ToString(CultureInfo.CurrentCulture);
			yield return null;
		}
	}
}
