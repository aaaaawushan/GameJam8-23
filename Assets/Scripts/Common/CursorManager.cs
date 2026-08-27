using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private RectTransform cursorUI;
    [SerializeField] private Image cursorImage;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite hoverSprite;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
      
        cursorUI.position = Mouse.current.position.ReadValue();

    
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
}