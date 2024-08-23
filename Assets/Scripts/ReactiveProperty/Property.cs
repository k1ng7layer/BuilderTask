using System;

namespace ReactiveProperty
{
    public class Property<T>
    {
        public T Value { get; set; }

        public void SetValue(T newValue)
        {
            Value = newValue;
            ValueChanged ?.Invoke(newValue);
        }

        public event Action<T> ValueChanged;
    }
}