namespace CandytorAPI
{
    public delegate void OnCanFrameFunc(CanMessage Msg);

    public class CandyAPI
    {
        object LockObj = new object();

        public event OnCanFrameFunc OnCanFrameReceived;

        public void ReceiveFrame(CanMessage Msg)
        {
            lock (LockObj)
            {
                OnCanFrameReceived?.Invoke(Msg);
            }
        }

        public void RunThread()
        {
            Thread T = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    ReceiveFrame(new CanMessage(0x666, [0x1, 0x2, 0x3], DateTime.Now));

                }
            });

            T.IsBackground = true;
            T.Start();
        }
    }
}
