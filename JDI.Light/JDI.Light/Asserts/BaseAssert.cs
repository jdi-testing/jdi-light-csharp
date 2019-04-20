
namespace JDI.Light.Asserts
{
    public class BaseAssert
    {
        private string _name;
        private string _failElement;

        public BaseAssert(string name, string failElement)
        {
            _name = name;
            _failElement = failElement;
        }

        public BaseAssert(string name)
        {
            _name = name;
            _failElement = name;
        }
    }
}
