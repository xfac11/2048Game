using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Value : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text pNumberText;
        [SerializeField] private int mValue = 2;
        private ValueColor pValueColor;
        private void Awake()
        {
            pValueColor = GetComponent<ValueColor>();
        }
        private void OnValidate()
        {
            UpdateText();
        }

        //Credit to: https://stackoverflow.com/a/600306
        private bool IsPowerOfTwo(int x)
        {
            return x != 0 && (x & x - 1) == 0;
        }
        private void UpdateText()
        {
            pNumberText.text = mValue.ToString();
        }
        public void SetValue(int value)
        {
            mValue = value;
            UpdateText();
            pValueColor.UpdateColor(value);
        }
    }
}