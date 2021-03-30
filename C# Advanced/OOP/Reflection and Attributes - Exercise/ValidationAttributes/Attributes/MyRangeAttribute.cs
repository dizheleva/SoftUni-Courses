using System;

namespace ValidationAttributes.Attributes
{
    public class MyRangeAttribute : MyValidationAttribute
    {
        private int _minValue;
        private int _maxValue;

        public MyRangeAttribute(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public override bool IsValid(object objectProperty)
        {
            var convertedObjectProperty = Convert.ToInt32(objectProperty);
            return convertedObjectProperty >= this._minValue && convertedObjectProperty <= this._maxValue;
        }
    }
}
