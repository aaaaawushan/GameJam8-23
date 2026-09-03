using UnityEngine;

public class EndManager : MonoBehaviour
{
    void Start()
    {
        if (SystemCursorManager.Instance != null)
        {
            SystemCursorManager.Instance.ShowCustomCursor();
        }
        else
        {
            Cursor.visible = true;
        }
        SystemCursorManager.Instance.ShowCustomCursor();
    }
}
