using Microsoft.JSInterop;

namespace Blazor.Documents.Pages.JSInteropEx
{
    public class MessageUpdateInvokeHelper
    {
        private Action action;

        public MessageUpdateInvokeHelper(Action action)
        {
            this.action = action;
        }

        [JSInvokable("Blazoraa")]
        public void UpdateMessageCaller()
        {
            action.Invoke();
        }
    }
}
