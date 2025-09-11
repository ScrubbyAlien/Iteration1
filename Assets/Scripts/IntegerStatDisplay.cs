using System;
using TMPro;
using UnityEngine;

public class IntegerStatDisplay : MonoBehaviour
{
    [SerializeField]
    private Stats<int> intStat;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private int minDisplayValue, maxDisplayValue;

    private void Start() {
        intStat.StatChanged += UpdateDisplayText;
        UpdateDisplayText(intStat.ReadStat());
    }

    private void OnDestroy() {
        intStat.StatChanged -= UpdateDisplayText;
    }

    private void UpdateDisplayText(int newValue) {
        text.text = Math.Clamp(newValue, minDisplayValue, maxDisplayValue).ToString();
    }
}