using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D defaultSprite;
    [SerializeField] private Texture2D hoverSprite;
    [SerializeField] private Texture2D normalCursorSprite;
    private bool forceDefault = false;

    [SerializeField] private Vector2 hotspot = Vector2.zero;

    void OnEnable()
    {  Debug.Log($"Cursor size: {defaultSprite.width}x{defaultSprite.height}");
            Cursor.SetCursor(defaultSprite, hotspot, CursorMode.Auto);
    
    }

    public void SetDefaultCursor(bool isDefault)
    {
        forceDefault = isDefault;
        if (isDefault)
        {
            Cursor.SetCursor(normalCursorSprite, Vector2.zero, CursorMode.Auto);
        }
    }

    void Update()
    {

        if (forceDefault) return;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            if (hit.collider.GetComponent<BulletBase>() != null)
            {
                Cursor.SetCursor(hoverSprite, hotspot, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(defaultSprite, hotspot, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(defaultSprite, hotspot, CursorMode.Auto);
        }
    }
    void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}