using UnityEngine;
using System.Collections;
using u3dExtensions;

public class LevelLoader : MonoBehaviour {

	IPromise<string> m_finishLoad;
	IEnumerator Start()
	{
		yield return null;
		m_finishLoad.Fulfill(Application.loadedLevelName);
		Debug.Log("============"+Application.loadedLevelName+"===============");
		Destroy(gameObject);
	}

	public static IFuture<string> LoadLevel(string lvlName)
	{
		Application.LoadLevel(lvlName);

		var go = new GameObject("_levelLoader");

		var lvlLoader = go.AddComponent<LevelLoader>();

		DontDestroyOnLoad(lvlLoader);
		lvlLoader.m_finishLoad = new Promise<string>();

		return lvlLoader.m_finishLoad.Future;
	}
}
