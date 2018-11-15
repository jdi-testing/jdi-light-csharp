namespace JDI.Core.Interfaces.Complex
{
    public interface IPage
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