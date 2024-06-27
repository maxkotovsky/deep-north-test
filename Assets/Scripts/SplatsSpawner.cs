using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplatsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _imageSplat;
    [SerializeField]
    private GameObject _blizzard;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private float _spawnInterval = 1.0f;
    [SerializeField]
    private float _fadeOutDuration = 2.0f;

    private Coroutine _spawnCoroutine;
    private RectTransform _canvasRect;

    void Start()
    {
        _canvasRect = _canvas.GetComponent<RectTransform>();
    }

    IEnumerator SpawnImages()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            SpawnImage();
            yield return new WaitForSeconds(_spawnInterval);
        }

    }

    void SpawnImage()
    {
        GameObject newImage = Instantiate(_imageSplat, _canvas.transform);

        RectTransform imageRectTransform = newImage.GetComponent<RectTransform>();
        Vector2 randomPosition = GetRandomPosition();
        Vector3 randomScale = GetRandomScale();
        Quaternion randomRoatationZ = GetRandomRotation();
        imageRectTransform.anchoredPosition = randomPosition;
        imageRectTransform.transform.localScale = randomScale;
        imageRectTransform.transform.rotation = randomRoatationZ;

        StartCoroutine(FadeOutAndDestroy(newImage));
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(_canvasRect.rect.width * -1, _canvasRect.rect.width);
        float y = Random.Range(_canvasRect.rect.height * -1, _canvasRect.rect.height);
        return new Vector2(x / 2, y / 2);
    }

    Vector3 GetRandomScale()
    {
        float randomScale = Random.Range(1, 4);
        return new Vector3(randomScale, randomScale, 1);
    }

    Quaternion GetRandomRotation()
    {
        float randomRoatationZ = Random.Range(0f, 360f);
        return Quaternion.Euler(0, 0, randomRoatationZ);
    }

    IEnumerator FadeOutAndDestroy(GameObject image)
    {
        Image img = image.GetComponent<Image>();
        Color originalColor = img.color;
        float elapsedTime = 0f;

        while (elapsedTime < _fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0.3f, 0f, elapsedTime / _fadeOutDuration);
            img.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        img.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        Destroy(image);
    }

    void Update()
    {
        if (_blizzard.activeSelf)
        {
            if (_spawnCoroutine == null)
            {
                _spawnCoroutine = StartCoroutine(SpawnImages());
            }
        }
        else
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }
    }
}

