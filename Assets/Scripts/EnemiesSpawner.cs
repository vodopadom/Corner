using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public RectTransform spawnPoint;       // UI точка справа
    public float verticalRange = 300f;      // Диапазон по Y
    public GameObject prefab;              // Префаб
    public float spawnInterval = 1f;       // Интервал
    public float moveSpeed = 100f;         // Скорость

    private float _timer;

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            _timer = 0f;
            SpawnAndMoveObject();
        }
    }

    private void SpawnAndMoveObject()
    {
        GameObject obj = Instantiate(prefab, spawnPoint.parent); // Внутри Canvas
        RectTransform objRect = obj.GetComponent<RectTransform>();
        objRect.pivot = new Vector2(0.5f, 0.5f);

        // Генерируем случайное смещение по Y
        float offsetY = Random.Range(-verticalRange / 2f, verticalRange / 2f);
        Vector3 worldPos = spawnPoint.position + new Vector3(0f, offsetY, 0f);

        // Переводим в локальные координаты для RectTransform
        objRect.anchoredPosition = ((RectTransform)spawnPoint.parent).InverseTransformPoint(worldPos);

        StartCoroutine(MoveLeftAndDestroy(objRect));
    }

    private IEnumerator MoveLeftAndDestroy(RectTransform rect)
    {
        float leftLimit = -((RectTransform)spawnPoint.parent).rect.width / 2f - rect.rect.width;

        while (rect != null && rect.anchoredPosition.x > leftLimit)
        {
            rect.anchoredPosition += Vector2.left * (moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (rect != null)
            Destroy(rect.gameObject);
    }
    
    
}