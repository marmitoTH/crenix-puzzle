using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gear : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public Sprite connectedSprite;
	public Vector2 dragSize = new Vector2(200, 200);

	private Slot m_attachedTo;
	private Slot m_lastAttachment;
	private Slot m_initialSlot;
	
	private RectTransform m_transform;
	private CanvasGroup m_canvasGroup;
	private Canvas m_canvas;
	private Image m_image;
	private Sprite m_defaultSprite;

	private void Awake()
	{
		m_image = GetComponent<Image>();
		m_transform = GetComponent<RectTransform>();
		m_canvas = GetComponentInParent<Canvas>();

		if (!TryGetComponent(out m_canvasGroup))
		{
			m_canvasGroup = gameObject.AddComponent<CanvasGroup>();
		}

		m_defaultSprite = m_image.sprite;
		AttachToParent();
		m_initialSlot = m_attachedTo;
	}

	public void AttachToParent()
	{
		var slot = GetComponentInParent<Slot>();

		if (slot)
		{
			Attach(slot);
		}
	}

	public void Attach(Slot to)
	{
		if (to && !to.inUse)
		{
			m_attachedTo = to;
			m_attachedTo.gear = this;
			m_transform.SetParent(m_attachedTo.transform);
			m_transform.sizeDelta = Vector2.zero;
			m_transform.anchoredPosition = Vector2.zero;
		}
	}

	public void Detach()
	{
		if (m_attachedTo)
		{
			m_lastAttachment = m_attachedTo;
			m_attachedTo.gear = null;
			m_attachedTo = null;
			SetConnected(false);
		}

		m_transform.SetParent(m_canvas.transform);
	}

	public void Restore()
	{
		if (m_lastAttachment)
		{
			Attach(m_lastAttachment);
		}
	}

	public void ResetState()
	{
		if (m_initialSlot)
		{
			Attach(m_initialSlot);
		}
	}

	public void SetConnected(bool connected)
	{
		m_image.sprite = connected ? connectedSprite : m_defaultSprite;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Detach();
		m_transform.position = eventData.position;
		m_transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dragSize.x);
		m_transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dragSize.y);
		m_canvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		m_transform.position += (Vector3)eventData.delta;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		var target = eventData.pointerCurrentRaycast.gameObject;

		if (target && target.TryGetComponent<Slot>(out var slot))
		{
			Attach(slot);
		}
		else
		{
			Restore();
		}

		m_canvasGroup.blocksRaycasts = true;
	}
}
