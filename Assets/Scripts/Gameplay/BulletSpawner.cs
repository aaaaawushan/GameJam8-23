using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] dialoguePrefab;
    [SerializeField] private Transform[] spawnPoints;
    public float spawnInterval = 1f;

    [Header("extra spawn")]
    [SerializeField] private Transform[] extraPoints1;  
    [SerializeField] private Transform[] extraPoints2;  
    [SerializeField] private float unlockTime1 = 35f;
    [SerializeField] private float unlockTime2 = 60f;

    private List<Transform> availablePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        availablePos = new List<Transform>(spawnPoints);
        InvokeRepeating("SpawnDialogue", 0f, spawnInterval);
        StartCoroutine(UnlockExtraPoints());
    }

    IEnumerator UnlockExtraPoints()
    {
        yield return new WaitForSeconds(unlockTime1);
        foreach (var point in extraPoints1)
        {
            availablePos.Add(point);
        }

        yield return new WaitForSeconds(unlockTime2 - unlockTime1);
        foreach (var point in extraPoints2)
        {
            availablePos.Add(point);
        }
    }

    void Update()
    {
        if (MainPause.Instance.IsPaused) return;
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
                    AudioManager.Instance?.PlayHitSFX();
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