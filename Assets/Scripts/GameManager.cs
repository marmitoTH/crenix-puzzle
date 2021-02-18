using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
	public UnityEvent onPuzzleCompleted;
	public UnityEvent onPuzzleIncompleted;

	private Gear[] m_gears;
	private MotorSlot[] m_motorSlots;

	private bool m_puzzleCompleted;

	private void Start()
	{
		m_gears = FindObjectsOfType<Gear>();
		m_motorSlots = FindObjectsOfType<MotorSlot>();
	}

	public void ResetPuzzle()
	{
		DetachAll();
		ResetAll();
		CheckPuzzle();
	}

	public void CheckPuzzle()
	{
		foreach (MotorSlot slot in m_motorSlots)
		{
			if (!slot.inUse)
			{
				ResetCompletion();
				return;
			}
		}

		CompletePuzzle();
	}

	private void CompletePuzzle()
	{
		if (!m_puzzleCompleted)
		{
			m_puzzleCompleted = true;
			onPuzzleCompleted.Invoke();
		}
	}

	private void ResetCompletion()
	{
		if (m_puzzleCompleted)
		{
			m_puzzleCompleted = false;
			onPuzzleIncompleted.Invoke();
		}
	}

	private void DetachAll()
	{
		foreach (Gear gear in m_gears)
		{
			gear.Detach();
		}
	}

	private void ResetAll()
	{
		foreach (Gear gear in m_gears)
		{
			gear.ResetState();
		}
	}
}
