using UnityEngine;
using UnityEngine.Events;

public class GameListener : MonoBehaviour
{
	public UnityEvent onGameCompleted;
	public UnityEvent onGameNotCompleted;

	private GameManager m_manager;

	private void Start()
	{
		m_manager = GameManager.instance;

		if (m_manager)
		{
			m_manager.onPuzzleCompleted.AddListener(() => OnGameCompleted());
			m_manager.onPuzzleIncompleted.AddListener(() => OnGameNotCompleted());
		}
	}

	protected virtual void OnGameCompleted()
	{
		onGameCompleted.Invoke();
	}

	protected virtual void OnGameNotCompleted()
	{
		onGameNotCompleted.Invoke();
	}
}
