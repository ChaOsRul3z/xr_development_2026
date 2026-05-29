using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
	public bool AutoUnparentOnAwake = true;

	protected static T _instance;
	protected static object _lock = new object();

	public static bool HasInstance => _instance != null;
	public static T TryGetInstance() => HasInstance ? _instance : null;

    public static T Instance
	{
		get
		{
			lock (_lock)
			{
				if (_instance == null)
				{
					_instance = FindAnyObjectByType<T>();
					if(_instance == null)
					{
						var go = new GameObject(typeof(T).Name + " Auto-Generated");
						_instance = go.AddComponent<T>();
					}
				}

				return _instance;
			}
		}
	}

	protected virtual void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		InitializeSingleton();
	}

	protected virtual void InitializeSingleton()
	{
		if (!Application.isPlaying) return;

		if (AutoUnparentOnAwake)
		{
			transform.SetParent(null);
		}

		if (_instance == null)
		{
			_instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			if (_instance != this)
			{
				Destroy(gameObject);
			}
		}
	}

	public void SwitchScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}