using System;

namespace JDI.Light.Matchers.StringMatchers
{
    public class ContainsStringMatcher : Matcher<string>
    {
        private ContainsStringMatcher(string textOccurence) : base(textOccurence)
        {
        }

        public static ContainsStringMatcher ContainsString(string textOccurence) => new ContainsStringMatcher(textOccurence);

        public override string ActionName => "contains string";

        protected override Func<string, string, bool> Condition => (text, occurence) => text.Contains(occurence);
    }
}
