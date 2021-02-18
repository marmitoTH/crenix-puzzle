using UnityEngine;

public abstract class Slot : MonoBehaviour
{
	private Gear m_gear;

	public Gear gear
	{
		get
		{
			return m_gear;
		}

		set
		{
			if (!m_gear && value)
			{
				OnGearAdded(value);
			}

			if (m_gear && !value)
			{
				OnGearRemoved(value);
			}

			m_gear = value;
			OnChange(value);
		}
	}

	public bool inUse => m_gear;

	protected abstract void OnChange(Gear gear);

	protected virtual void OnGearAdded(Gear gear) { }

	protected virtual void OnGearRemoved(Gear gear) { }
}
