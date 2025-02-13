using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu (menuName = "VariableSO/IntListVariableSO")]
    public class IntListVariableSO : VariableSO<List<int>>
    {
        public int Count => Value.Count;
        
        public void AddValue(int value)
        {
            var arr = Value;
            arr.Add(value);
            Value = arr;
        }
        
        public bool Contains(int value)
        {
            return Value.Contains(value);
        }
        
        public void RemoveValue(int value)
        {
            var arr = Value;
            arr.Remove(value);
            Value = arr;
        }
        
        public void RemoveAt(int index)
        {
            var arr = Value;
            arr.RemoveAt(index);
            Value = arr;
        }
        
        public void Clear()
        {
            Value.Clear();
        }
        
    }
}