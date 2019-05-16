using System;
using System.Text.RegularExpressions;

namespace JDI.Light.Matchers.StringMatchers
{
    public class RegexMatcher : Matcher<string>
    {
        private RegexMatcher(string regex) : base(regex)
        {
        }

        public static RegexMatcher MatchRegexp(string regex) => new RegexMatcher(regex);

        public override string ActionName => "match regex";

        protected override Func<string, string, bool> Condition => Regex.IsMatch;
    }
}
