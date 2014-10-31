using UnityEngine;
using UnityEditor;
using IndeegoGames;


[System.Serializable]
public class ObjectProfilerWindow : ObjectProfiler{
	
	[MenuItem("Window/Object Profiler")]
    static void Launch()
    {
		ObjectProfiler window = (ObjectProfilerWindow)EditorWindow.CreateInstance(typeof(ObjectProfilerWindow));
		window.Show();
    }

    override public EditorWindow CreateGraph(string id)
    {
		return (EditorWindow)EditorWindow.CreateInstance(typeof(ObjectGraphWindow));
    }
}
