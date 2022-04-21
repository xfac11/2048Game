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
        [SerializeField] private Image pImage;
        [SerializeField] private int mValue = 2;
        private void OnValidate()
        {
            UpdateText();
            UpdateColor();
        }

        private void UpdateColor()
        {
            pImage.color = mValue != 0 ? Color.white : Color.gray;
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
            UpdateColor();
        }
    }
}