using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scenes
{
	private static readonly Dictionary<SceneEnum, string> sceneNames;

	static Scenes() {
		sceneNames = new Dictionary<SceneEnum, string> ();
		sceneNames[SceneEnum.MainMenu] = "MainMenu";
		sceneNames[SceneEnum.Level1Player1] = "Level1Player1";
		sceneNames[SceneEnum.Level1Player2] = "Level1Player2";
		sceneNames[SceneEnum.Level1Player3] = "Level1Player3";
		sceneNames[SceneEnum.Level2Player1] = "Level2Player1";
		sceneNames[SceneEnum.Level2Player2] = "Level2Player2";
		sceneNames[SceneEnum.Level2Player3] = "Level2Player3";
		sceneNames[SceneEnum.Level3Player1] = "Level3Player1";
		sceneNames[SceneEnum.Level3Player2] = "Level3Player2";
		sceneNames[SceneEnum.Level3Player3] = "Level3Player3";
	}

	public static void LoadSceneByID(SceneEnum targetScene) {
		string targetSceneName;
		if (sceneNames.TryGetValue (targetScene, out targetSceneName))
			SceneManager.LoadScene (targetSceneName);
		else
			Debug.LogError ("Scène inconnue : "+ targetScene.ToString());
	}

	public Scenes ()
	{
	}
}

public enum SceneEnum {
	MainMenu,
	Level1Player1,
	Level1Player2,
	Level1Player3,
	Level2Player1,
	Level2Player2,
	Level2Player3,
	Level3Player1,
	Level3Player2,
	Level3Player3,
}

