public class InventorySlot : Slot
{
	protected override void OnChange(Gear gear)
	{
		if (gear)
		{
			gear.SetConnected(false);
		}
	}
}
