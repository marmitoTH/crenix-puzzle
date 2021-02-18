using UnityEngine;

public class GearSlot : MonoBehaviour
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

	protected virtual void OnChange(Gear gear) { }
}
