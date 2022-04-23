using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ValueColor : MonoBehaviour
    {
        [SerializeField] List<Color> pColors;
        [SerializeField] Image pImage;
        private Dictionary<int, Color> mColorDictionary = new Dictionary<int, Color>();//Replace this with a ScriptableObject
        private void Awake()
        {
            int power = 1;
            foreach (var item in pColors)
            {
                int powerOfTwo = (int)Mathf.Pow(2, power);
                power++;
                mColorDictionary.Add(powerOfTwo, item);
            }
        }
        public void UpdateColor(int value)
        {
            if (value == 0)
            {
                pImage.color = Color.grey;
                return;
            }
            pImage.color = mColorDictionary[value];
        }
    }
}