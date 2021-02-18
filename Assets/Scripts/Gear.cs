using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gear : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public Sprite gearInUse;

	private Canvas m_canvas;
	private RectTransform m_transform;
	private CanvasGroup m_canvasGroup;
	private Transform m_initialParent;
	private GearSlot m_slot;
	private Image m_image;
	private Sprite m_defaultSprite;

	private void Start()
	{
		m_canvas = GetComponentInParent<Canvas>();
		m_transform = GetComponent<RectTransform>();
		m_image = GetComponent<Image>();

		m_defaultSprite = m_image.sprite;
		m_initialParent = m_transform.parent;

		if (!TryGetComponent(out m_canvasGroup))
		{
			m_canvasGroup = gameObject.AddComponent<CanvasGroup>();
		}
	}

	public void Reset()
	{
		if (m_slot)
		{
			m_slot.gear = null;
			m_slot = null;
		}

		m_image.sprite = m_defaultSprite;
		m_transform.SetParent(m_initialParent);
		m_transform.sizeDelta = Vector2.zero;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Reset();
		m_canvasGroup.blocksRaycasts = false;
		m_transform.SetParent(m_canvas.transform);
		m_transform.position = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		m_transform.position += (Vector3)eventData.delta;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		var target = eventData.pointerCurrentRaycast.gameObject;

		if (target && target.TryGetComponent<GearSlot>(out var slot) && !slot.inUse)
		{
			m_slot = slot;
			m_slot.gear = this;
			m_image.sprite = gearInUse;
			m_transform.SetParent(slot.transform);
			m_transform.sizeDelta = Vector2.zero;
		} 
		else
		{
			Reset();
		}

		m_canvasGroup.blocksRaycasts = true;
		m_transform.anchoredPosition = Vector2.zero;
	}
}
