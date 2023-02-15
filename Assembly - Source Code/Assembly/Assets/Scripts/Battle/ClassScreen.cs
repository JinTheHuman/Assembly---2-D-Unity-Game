using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassScreen : MonoBehaviour
{	
	ClassMemberUI[] memberSlots;
	List<Student> studentsLs;

	public Text dialogue;

	public void Init()
	{
		memberSlots = GetComponentsInChildren<ClassMemberUI>();
	}

	public void SetClassHUD(List<Student> students)
	{
		// Sets up HUD for each student in team
		studentsLs = students;
		for (int i = 0; i < memberSlots.Length; i++)
		{
			if (i < students.Count)
				memberSlots[i].SetHUD(students[i]);
			else
				memberSlots[i].gameObject.SetActive(false);
		}
	}
	
}
