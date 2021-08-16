using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoGooseGo
{
    [CreateAssetMenu(menuName = "GoGooseGo/Item")]
    public class ItemData : ScriptableObject
    {
        [field : SerializeField]
        public int id { get; private set; }

        [field : SerializeField]
        public new string name { get; private set; }

        [field : SerializeField]
        public string description { get; private set; }

        [field : SerializeField]
        public Sprite sprite { get; private set; }
    }
}