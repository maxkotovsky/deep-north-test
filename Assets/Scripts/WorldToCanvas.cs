using UnityEngine;
using UnityEngine.UI;

public class WorldToCanvas : MonoBehaviour
{
    [SerializeField]
    private Transform _worldLocator;
    [SerializeField]
    private RectTransform _screenContainer;
    [SerializeField]
    private Camera _mainCamera;

    void Update()
    {
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(_worldLocator.position);
        _screenContainer.position = screenPos;
        bool isVisible = screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height;

        if (isVisible)
        {
            _screenContainer.position = screenPos;
            _screenContainer.gameObject.SetActive(true);
        }
        else
        {
            _screenContainer.gameObject.SetActive(false);
        }
    }
}