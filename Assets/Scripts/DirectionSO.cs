using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Direction", menuName = "ScriptableObjects/Create Direction", order = 52)]
    public class DirectionSO : ScriptableObject
    {
        [SerializeField] Direction mDirection;
        public Direction Direction { get { return mDirection; } }
    }
}

