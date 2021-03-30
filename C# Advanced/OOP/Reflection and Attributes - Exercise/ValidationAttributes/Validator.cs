using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ValidationAttributes.Attributes;

namespace ValidationAttributes
{
    public static class Validator
    {
        public static bool IsValid(Object obj)
        {
            Type objType = obj.GetType();
            PropertyInfo[] propertyInfos = objType.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                Object propertyObject = propertyInfo.GetValue(obj);
                List<MyValidationAttribute> myAttributes = propertyInfo.GetCustomAttributes<MyValidationAttribute>().ToList();
                if (myAttributes.Select(myValidationAttribute => myValidationAttribute.IsValid(propertyObject)).Any(isValid => !isValid))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
