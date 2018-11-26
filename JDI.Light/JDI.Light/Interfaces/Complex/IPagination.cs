using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Complex
{
    public interface IPagination : IBaseElement
    {
        /**
         * Choose Next page
         */
        //TODO[JDIAction]
        void Next();

        /**
         * Choose Previous page
         */
        //TODO[JDIAction]
        void Previous();

        /**
         * hoose First page
         */
        //TODO[JDIAction]
        void First();

        /**
         * Choose Last page
         */
        //TODO[JDIAction]
        void Last();

        /**
         * @param index Specify page index
         *              Choose page by index
         */
        //TODO[JDIAction]
        void SelectPage(int index);
    }
}