namespace DAA.Constants.Databases
{
    public class DbFieldsValues
    {
        public class AuditableFields
        {
            public const string PREFIX = "AUT_";

            public const string CREATED_AT = AuditableFields.PREFIX + "CreatedAt";
            public const string UPDATED_AT = AuditableFields.PREFIX + "UpdatedAt";

            public const string CREATED_AT_DEFAULT_VALUE = "GETDATE()";
            public const string UPDATED_AT_DEFAULT_VALUE = "GETDATE()";
        }
    }
}
