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

    private void Update() {
        text.text = Math.Clamp(intStat.ReadStat(), minDisplayValue, maxDisplayValue).ToString();
    }
}