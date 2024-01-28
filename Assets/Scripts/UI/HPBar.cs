using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alteruna;
using AlterunaFPS;
using Avatar = Alteruna.Avatar;

public class HPBar : MonoBehaviour {
    [SerializeField] private Image hpBar;
    [SerializeField] private Image barBorder;
    [SerializeField] private Avatar Avatar;

    private float _maxHealth;
    private float _health;

    private Dictionary<string, Color32> _colours;

    private void Awake() {
        _colours = new Dictionary<string, Color32> {
            { "healthy", new Color32(70, 207, 34, 255) },
            { "healthyBorder", new Color32(18, 100, 31, 255) },
            { "danger", new Color32(207, 24, 32, 255) },
            { "dangerBorder", new Color32(83, 11, 13, 255) }
        };
    }

    private void Start() {
        _maxHealth = Avatar.GetComponent<PlayerController>().MaxHealth;
    }

    private void Update() {
        _health = Avatar.GetComponent<PlayerController>().GetHealth().HealthPoints;

        hpBar.fillAmount = _health / _maxHealth;
        
        Debug.Log(_health / _maxHealth);
        hpBar.color = hpBar.fillAmount > 0.25f ? _colours["healthy"] : _colours["danger"];
        barBorder.color = hpBar.fillAmount > 0.25f ? _colours["healthyBorder"] : _colours["dangerBorder"];
    }
}