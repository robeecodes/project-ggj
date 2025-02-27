using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    [SerializeField] private float timeRemaining = 900f;

    public static Timer Instance { get; private set;  }
    
    private GUIStyle _white;
    
    private enum State {
        Running,
        Idle
    }

    private State _state;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        _state = State.Running;
        _white = new GUIStyle() {
            fontSize = 32,
            fontStyle = FontStyle.Bold,
        };
        _white.normal.textColor = Color.white;
    }

    private void Update() {
        if (_state == State.Running) {
            if (timeRemaining > 0f) {
                timeRemaining -= Time.deltaTime;
            }
            else {
                SceneManager.LoadScene("Menu");
                timeRemaining = 0f;
                _state = State.Idle;
            }
        }
    }

    private string FormatTime() {
        string minutes = Mathf.FloorToInt(timeRemaining / 60).ToString(); 
        string seconds = Mathf.FloorToInt(timeRemaining % 60).ToString();

        if (seconds.Length < 2) {
            seconds = $"0{seconds}";
        }
        
        return $"{minutes}:{seconds}";
    }

    private void OnGUI() {
        GUILayout.BeginVertical();
        GUILayout.Space(50);
        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Space(50);
        GUI.contentColor = Color.white;
        GUILayout.Label(FormatTime(), _white);
        GUILayout.EndHorizontal();
    }
}