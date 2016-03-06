using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
	public float Time = 0f;
	private bool _functioning = false;

	public bool Functioning
	{
		get
		{
			return _functioning;
		}

		set
		{
			_functioning = value;
		}
	}

	public void StartTimer()
	{
		StartCoroutine("UpdateTime");
		Functioning = false;
	}

	public void Trigger()
	{
		Functioning = !Functioning;
	}

	// Update is called once per frame
	private void Update ()
	{
	}

	private IEnumerator UpdateTime()
	{
		while (true)
		{
			if (Functioning)
				Time += UnityEngine.Time.deltaTime;
			yield return null;
		}
	}
}
