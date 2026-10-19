using UnityEngine;
using UnityEngine.EventSystems;

// Этот скрипт вешается на базу джойстика. 
// Он заставляет ручку двигаться и возвращает вектор направления.
public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Настройки джойстика")]
    public RectTransform handle; 
    public float handleRadius = 45f; 

    private Vector2 inputVector; 

    // Свойство, чтобы другие скрипты (например, движение героя) могли читать направление
    public Vector2 Direction
    {
        get { return inputVector; }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)this.transform,
            eventData.position,
            eventData.pressEventCamera,
            out position);

        position = Vector2.ClampMagnitude(position, handleRadius);

        handle.anchoredPosition = position;

        inputVector = position / handleRadius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;
        inputVector = Vector2.zero;
    }
}