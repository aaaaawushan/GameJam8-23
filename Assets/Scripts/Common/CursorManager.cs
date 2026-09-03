using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private RectTransform cursorUI;
    [SerializeField] private Image cursorImage;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite normalCursorSprite;

    public static CursorManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.visible = false;
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval("document.body.style.cursor='none'; var c=document.getElementById('unity-canvas'); if(c) c.style.cursor='none';");
#endif
    }

    void Update()
    {
        Cursor.visible = false;
        cursorUI.position = Mouse.current.position.ReadValue();

        string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (scene == "MainScene" || scene == "BossScene")
        {
           
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.GetComponent<BulletBase>() != null)
            {
                cursorImage.sprite = hoverSprite;
            }
            else
            {
                cursorImage.sprite = defaultSprite;
            }
        }
        else
        {
            
            cursorImage.sprite = normalCursorSprite;
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.visible = false;
#if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalEval("document.body.style.cursor='none'; var c=document.getElementById('unity-canvas'); if(c) c.style.cursor='none';");
#endif
        }
    }
}