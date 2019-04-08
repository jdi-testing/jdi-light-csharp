using System;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class JDropDown : Attribute
    {
        private readonly string _root;
        private readonly string _value;
        private readonly string _list;
        private readonly string _expand;

        public JDropDown(string root, string value, string list, string expand)
        {
            _root = root;
            _value = value;
            _list = list;
            _expand = expand;
        }

        public string Root() => _root;

        public string Value() => _value;

        public string List() => _list;

        public string Expand() => _expand;
    }
}