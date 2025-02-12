using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu (menuName = "VariableSO/FloatListVariableSO")]
    public class FloatListVariableSO : VariableSO<List<float>>
    {
        
        public void AddValue(float value)
        {
            var arr = Value;
            arr.Add(value);
            Value = arr;
        }
        
        public void RemoveValue(float value)
        {
            var arr = Value;
            arr.Remove(value);
            Value = arr;
        }
        
    }
}