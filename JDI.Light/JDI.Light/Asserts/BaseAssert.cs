
namespace JDI.Light.Asserts
{
    public class BaseAssert
    {
        public string Name;
        public string FailElement;

        public BaseAssert(string name, string failElement)
        {
            Name = name;
            FailElement = failElement;
        }

        public BaseAssert(string name)
        {
            Name = name;
            FailElement = name;
        }
    }
}
