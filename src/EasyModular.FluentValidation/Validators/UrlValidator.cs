using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyModular.FluentValidation
{
    /// <summary>
    /// Url验证
    /// </summary>
    public class UrlValidator : PropertyValidator
    {
        private const string Pattern = @"^(http?)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        private static Regex _regex;

        public UrlValidator() : base("URL地址无效")
        {
            _regex = new Regex(Pattern);
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
                return false;

            return _regex.IsMatch(context.PropertyValue.ToString());
        }
    }
}
