using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;

namespace JDI.Light.Elements.Complex.Table
{
    public class Line
    {
        private Dictionary<string, string> _data;
        private List<UIElement> _elements;
        private List<string> _headers;
        private List<string> _list;

        public Line(List<string> list, List<string> headers)
        {
            _list = list;
            _headers = headers;
        }

        public Line(List<UIElement> elements, List<string> headers)
        {
            _elements = elements;
            _headers = headers;
            _data = ZipTwoLists(_headers, _elements.Select(el => el.Text).ToList());
        }

        private Dictionary<string, string> ZipTwoLists(IReadOnlyCollection<string> keys, IReadOnlyCollection<string> values)
        {
            if (keys == null || values == null || keys.Count != values.Count)
                throw new ArgumentException("Keys or values are null or has not equal count.");
            var dictionary = new Dictionary<string, string>();
            try
            {
                keys.Zip(values, (f, s) =>
                {
                    dictionary.Add(f, s);
                    return dictionary;
                });
                return dictionary;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Keys collection contains some duplicate values." + e.Message);
            }
        }

        public static Line InitLine(List<string> list, List<string> headers)
        {
            var line = new Line(list, headers);
            return line;
        }

        private Dictionary<string, string> GetData()
        {
            return _data;
        }

        private List<string> GetList(int minAmount)
        {
            return _list != null && _list.Count >= minAmount ? _list
                : (List<string>)GetData().Select(pair => pair.Value);
        }

        public string GetValue()
        {
            return string.Join(";", GetList(0));
        }
    }
}
