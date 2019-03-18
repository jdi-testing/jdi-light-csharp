using System;
using System.Collections.Generic;
using JDI.Light.Elements.Base;

namespace JDI.Light.Elements.Complex.Table
{
    public class Line
    {
        private Func<SortedList<string, string>> _dataMap;
        private SortedList<string, string> _data;
        private List<UIElement> _elements;
        private List<string> _headers;
        private List<string> _list;

        public Line(List<string> list, List<string> headers)
        {
            _list = list;
            _headers = headers;
            //_dataMap => new SortedList<string, string>(_headers, );
        }

        public Line(List<UIElement> elements, List<string> headers)
        {
            _elements = elements;
            _headers = headers;
            //_dataMap => new SortedList<string, string>(_headers, );
        }

        public static Line InitLine(List<string> list, List<string> headers)
        {
            var line = new Line(list, headers);
            return line;
        }

        /*private List<string> GetList(int minAmount)
        {
            return _list != null && _list.Count >= minAmount ? _list : ;
        }*/
    }
}
