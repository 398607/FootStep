using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteInvoker : MonoBehaviour
{
	public int number;
	
	void OnMouseDown()
	{
		// Debug.Log("NoteInvoker number: " + number);

		List<Note>[] notes = GameManager.Instance.Notes;

		Note invoked_note = null;
		var min_y_value = Mathf.Infinity;
		var currentTime = GameManager.GetTime();

		foreach (var note in notes[number + 2])
		{
			if (note.transform.position.y < min_y_value && note.ExactTime + 3f > currentTime)
			{
				min_y_value = note.transform.position.y;
				invoked_note = note;
			}
		}

		if (invoked_note != null)
			invoked_note.Invoked();
	}
}
