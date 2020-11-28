using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PetminderApp.Validation
{
    public static class ValidationFunctions
    {
        public static string ValidateCreateObject(object model)
        {
            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                if (type == typeof(int))
                {

                }
            }

            return "";
        }
    }
}
