using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu (menuName = "VariableSO/FloatArrVariableSO")]
    public class FloatArrVariableSO : VariableSO<float[]>
    {
        
        public void AddValue(float value)
        {
            var arr = Value;
            var newArr = new float[arr.Length + 1];
            for (int i = 0; i < arr.Length; i++)
            {
                newArr[i] = arr[i];
            }

            newArr[arr.Length] = value;
            Value = newArr;
        }
    }
}