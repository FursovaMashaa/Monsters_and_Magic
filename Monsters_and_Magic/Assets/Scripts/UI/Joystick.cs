using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler
{
    [Header("Настройки джойстика")]
    public RectTransform handle;
    public float handleRadius = 45f;
    public CanvasGroup canvasGroup;

    private Vector2 inputVector;
    private bool isActive = false;

    public Vector2 Direction { get { return inputVector; } }

    private void Awake()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        handle.anchoredPosition = Vector2.zero;
    }

    private void Update()
    {
        if (!isActive && Input.GetMouseButtonDown(0))
        {
            this.transform.position = Input.mousePosition;

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0.6f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
            isActive = true;

            UpdateJoystickPosition(Input.mousePosition);
        }

        if (isActive && Input.GetMouseButton(0))
        {
            UpdateJoystickPosition(Input.mousePosition);
        }

        if (isActive && Input.GetMouseButtonUp(0))
        {
            ResetJoystick();
        }
    }

    private void UpdateJoystickPosition(Vector2 screenPosition)
    {
        Vector2 position = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)this.transform,
            screenPosition,
            null, 
            out position);

        position = Vector2.ClampMagnitude(position, handleRadius);
        handle.anchoredPosition = position;
        inputVector = position / handleRadius;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    private void ResetJoystick()
    {
        handle.anchoredPosition = Vector2.zero;
        inputVector = Vector2.zero;
        isActive = false;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    private void OnDisable()
    {
        if (isActive)
        {
            ResetJoystick();
        }
    }
}