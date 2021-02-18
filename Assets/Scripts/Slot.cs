using UnityEngine;

public abstract class Slot : MonoBehaviour
{
	private Gear m_gear;

	public Gear gear
	{
		set
		{
			m_gear = value;
			OnChange(m_gear);
		}
	}

	public bool inUse => m_gear;

	protected abstract void OnChange(Gear gear);
}
