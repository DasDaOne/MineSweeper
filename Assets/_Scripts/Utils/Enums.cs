using System;

public enum GameDifficulty
{
	Easy,
	Medium,
	Hard,
	Expert
}

public static class EnumExtensions
{
	public static int FieldSize(this GameDifficulty gameDifficulty)
	{
		switch (gameDifficulty)
		{
			case GameDifficulty.Easy:
				return 7;
			case GameDifficulty.Medium:
				return 9;
			case GameDifficulty.Hard:
				return 12;
			case GameDifficulty.Expert:
				return 15;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public static int BombAmount(this GameDifficulty gameDifficulty)
	{
		switch (gameDifficulty)
		{
			case GameDifficulty.Easy:
				return 10;
			case GameDifficulty.Medium:
				return 20;
			case GameDifficulty.Hard:
				return 30;
			case GameDifficulty.Expert:
				return 60;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
