using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SystemCursorManager : MonoBehaviour
{
    public static SystemCursorManager Instance;

    [SerializeField] private RectTransform cursorUI;  
    [SerializeField] private float cursorSize = 16f;   

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
       
        cursorUI.sizeDelta = new Vector2(cursorSize, cursorSize);
    }
    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Cursor.visible = false;
        if (cursorUI.gameObject.activeSelf)
        {
            cursorUI.position = Mouse.current.position.ReadValue();
        }
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            if (FindAnyObjectByType<CursorManager>() != null) return;

            ShowCustomCursor();
        }
    }

    public void ShowCustomCursor()
    {
        if (cursorUI != null)
        {
            cursorUI.gameObject.SetActive(true);
        }
     //   Cursor.visible = true;
       // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void HideCursor()
    {
        if (cursorUI != null)
        {
            cursorUI.gameObject.SetActive(false);
        }
        Cursor.visible = false;
    }
}