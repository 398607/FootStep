using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteInvoker : MonoBehaviour
{
	public int number;
	
	void OnMouseDown()
	{
		Debug.Log("NoteInvoker number: " + number);

		List<Note> notes = GameManager.Instance.Notes;

		Note invoked_note = null;
		float min_y_value = Mathf.Infinity;

		foreach (var note in notes)
		{
			if (note.Number == number && note.transform.position.y < min_y_value)
			{
				min_y_value = note.transform.position.y;
				invoked_note = note;
			}
		}

		if (invoked_note != null)
			invoked_note.Invoked();
	}
}
