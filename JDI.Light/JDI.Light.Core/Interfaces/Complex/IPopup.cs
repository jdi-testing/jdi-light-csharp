using JDI.Core.Interfaces.Base;
using JDI.Core.Interfaces.Common;

namespace JDI.Core.Interfaces.Complex
{
    public interface IPopup : IText, IComposite
    {
        /**
         * Click on Button marked with annotation @OkButton or named "okButton"
         */
        //TODO[JDIAction]
         void Ok();

        /**
         * Click on Button marked with annotation @CancelButton or named "cancelButton"
         */
        //TODO[JDIAction]
        void Cancel();

        /**
         * Click on Button marked with annotation @CloseButton or named "closeButton"
         */
        //TODO[JDIAction]
        void Close();
    }
}
