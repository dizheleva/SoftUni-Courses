namespace ValidationAttributes.Attributes
{
    public class MyRequiredAttribute : MyValidationAttribute
    {
        public override bool IsValid(object objectProperty)
        {
            return objectProperty != null;
        }
    }
}
