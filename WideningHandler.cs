namespace ClassLibrary4
{
    public class WideningHandler<TInput, TOutput> : Handles<TInput> 
        where TInput : Message 
        where TOutput : TInput
    {
        private readonly Handles<TOutput> handler;

        public WideningHandler(Handles<TOutput> handler)
        {
            this.handler = handler;
        }

        public void Handle(TInput message)
        {
            handler.Handle((TOutput)message);
        }
    }
}