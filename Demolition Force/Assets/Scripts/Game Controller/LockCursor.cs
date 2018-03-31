//	chautran
// purpose: lock cursor and hide it
// where to put: Game Controller object

using UnityEngine;
using System.Collections;

public class LockCursor : MonoBehaviour {


	public void LockAndHide()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void UnlockAndShow()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
