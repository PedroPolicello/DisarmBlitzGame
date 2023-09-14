using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private bool isDragging = false;

    [SerializeField] private List<RectTransform> targetObjects;
    [SerializeField] private float snapTolerance;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        originalPosition = rectTransform.position;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        

        RectTransform nearestTarget = GetNearestTarget();
        if (nearestTarget != null)
        {
            rectTransform.position = nearestTarget.position;
        }
        else
        {
            rectTransform.position = originalPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        isDragging = false;
    }

    private void Update()
    {
        
        if (isDragging && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            OnEndDrag(pointerData);
        }
    }

    private RectTransform GetNearestTarget()
    {
        float minDistance = float.MaxValue;
        RectTransform nearestTarget = null;

        foreach (RectTransform targetObject in targetObjects)
        {
            
            float distance = Vector2.Distance(rectTransform.position, targetObject.position);

            
            if (distance <= snapTolerance && distance < minDistance)
            {
                minDistance = distance;
                nearestTarget = targetObject;
            }
        }

        return nearestTarget;
    }
}
