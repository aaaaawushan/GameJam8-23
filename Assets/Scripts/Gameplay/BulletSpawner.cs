using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] dialoguePrefab;
    [SerializeField] private Transform[] spawnPoints;
    public float spawnInterval = 1f;

    private List<Transform> availablePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        availablePos = new List<Transform>(spawnPoints);
        InvokeRepeating("SpawnDialogue", 0f, spawnInterval);
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                BulletBase bullet = hit.collider.GetComponent<BulletBase>();
                if (bullet != null)
                {
                    bullet.OnHit();
                }
            }
        }
    }
    void SpawnDialogue()
    {
        if (availablePos.Count == 0) return;

        int index = Random.Range(0, availablePos.Count);
        Transform point = availablePos[index];
        availablePos.RemoveAt(index);

        GameObject prefab = dialoguePrefab[Random.Range(0, dialoguePrefab.Length)];

        GameObject bullet = Instantiate(prefab, point.position, Quaternion.identity);

        bullet.GetComponent<BulletBase>().SetSpawner(this, point);
    }
    public void ReleasePoint(Transform point)
    {
        availablePos.Add(point);
    }
}
