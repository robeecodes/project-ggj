using TMPro;
using UnityEngine;
using Avatar = Alteruna.Avatar;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] private Avatar Avatar;
    [SerializeField] private TMP_Text Text;
    void Start() {
        Text.text = Avatar.Possessor.Name;
    }
}
