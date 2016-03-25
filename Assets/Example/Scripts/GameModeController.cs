using UnityEngine;
using System.Collections;

public enum GameMode {NORMAL, EASY}

public static class GameModeController
{
	private static GameMode m_currentMode;

	public static GameMode currentMode {
		get
		{
			if (m_currentMode != (GameMode.EASY|GameMode.NORMAL))
				m_currentMode = GameMode.NORMAL;
			return m_currentMode;
		}
		set
		{
			m_currentMode = value;
		}
	}

	public static void setEasyMode ()
	{
		currentMode = GameMode.EASY;
	}

	public static void resetMode ()
	{
		currentMode = GameMode.NORMAL;
	}
}
