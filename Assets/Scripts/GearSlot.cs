using UnityEngine;

public class GearSlot : MonoBehaviour
{
	private Gear m_gear;

	public Gear gear
	{
		set
		{
			m_gear = value;
		}
	}

	public bool inUse => m_gear;
}
