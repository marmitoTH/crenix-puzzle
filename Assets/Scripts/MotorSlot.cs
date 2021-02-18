using UnityEngine;

public class MotorSlot : Slot
{
	public bool anticlockwise;
	public float rotationDegreesPerSecond;

	private bool m_rotate;

	private void Update()
	{
		if (m_rotate)
		{
			RotateGear(Time.deltaTime);
		}
	}

	public void StartRotation()
	{
		if (!m_rotate)
		{
			m_rotate = true;
		}
	}

	public void StopRotation()
	{
		if (m_rotate)
		{
			m_rotate = false;
			RestoreGearRotation();
		}
	}

	private void RotateGear(float deltaTime)
	{
		if (gear)
		{
			var sign = anticlockwise ? -1 : 1;
			var delta = rotationDegreesPerSecond * sign * deltaTime;
			gear.transform.Rotate(Vector3.forward, -delta);
		}
	}

	private void RestoreGearRotation()
	{
		if (gear)
		{
			gear.transform.rotation = Quaternion.identity;
		}
	}

	protected override void OnChange(Gear gear)
	{
		if (gear)
		{
			gear.SetConnected(true);
		}
		
		GameManager.instance.CheckPuzzle();
	}

	protected override void OnGearRemoved(Gear gear)
	{
		RestoreGearRotation();
	}
}
