class CTest : Object
{
    // 쓰레드의 메시지를 표시하기 위한 delegate
    public delegate void ThreadMessage(object sender, string strMsg);
    public event ThreadMessage? OnThreadMessage = null;

    private Thread m_Thread;// 쓰레드
    private bool m_bFlag = false; // 쓰레드 종료 플래그

    private int m_iNo;
    public CTest(int iNo)
    {
        m_iNo = iNo;
        m_Thread = new Thread(new ThreadStart(Run));
    }
    ~CTest() { }

    public void StartThread()
    {
        m_bFlag = true;
        if (m_Thread.IsAlive == false)
        {
            m_Thread.Start();
        }
    }

    public void StopThread()
    {
         m_bFlag = false;
        if (m_Thread.IsAlive == true)
        {
            m_Thread.Join();
        }

    }

    public new string ToString()
    {
        return $"{m_iNo}번 객체";
    }

    private void Run()
    {
        //if (this.in != null)
        {
            OnThreadMessage?.Invoke(this, "Start Thread");

            for (int i = 0; i < 10; i++)
            {
                OnThreadMessage?.Invoke(this, $"index: {i}");
                Thread.Sleep(1000);
                if (m_bFlag == false)
                {
                    OnThreadMessage?.Invoke(this, $"m_bFlag is {m_bFlag}");
                    break;
                }
            }
            
            OnThreadMessage?.Invoke(this, "End Thread");
        }
    }
}