using CandytorAPI;
using System.ComponentModel;

namespace Candytor
{
    public partial class MainForm : Form
    {
        BindingList<CanFrame> AllFramesList = new BindingList<CanFrame>();
        BindingSource AllFramesSource = new BindingSource();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AllFramesList.Clear();
            AllFramesSource.DataSource = AllFramesList;
            AllFramesView.DataSource = AllFramesSource;

            Program.API.OnCanFrameReceived += (Msg) =>
                {
                    Invoke(() =>
                    {
                        API_OnCanFrameReceived(Msg);
                    });
                };
        }

        CanFrame GetFrame(uint CanID)
        {
            foreach (CanFrame F in AllFramesList)
            {
                if (F.ID == CanID)
                    return F;
            }

            CanFrame NewFrame = new CanFrame(CanID);
            AllFramesList.Add(NewFrame);

            return NewFrame;
        }

        private void API_OnCanFrameReceived(CanMessage Msg)
        {
            CanFrame F = GetFrame(Msg.ID);
            F.Count++;

            AllFramesSource.ResetBindings(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.API.RunThread();
        }
    }
}
