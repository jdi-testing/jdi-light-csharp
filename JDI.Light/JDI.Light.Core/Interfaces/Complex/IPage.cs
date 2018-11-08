using JDI.Core.Interfaces.Base;

namespace JDI.Core.Interfaces.Complex
{
    public interface IPage : IComposite
    {
        /**
         * Check that page opened
         */
         void CheckOpened();

        /**
         * Opens url specified for page
         */
        void Open();
    }
}
