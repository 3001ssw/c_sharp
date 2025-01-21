using System;
using System.Diagnostics;
using System.Threading;

class CTest
{
    // 쓰레드의 메시지를 표시하기 위한 delegate
    public delegate void ThreadMessage(object sender, string strMsg);
    public event ThreadMessage? OnThreadMessage = null;

    private Thread m_Thread;// 쓰레드
    private bool m_bFlag = false; // 쓰레드 종료 플래그

    private int m_iNo;

    public bool FLAG
    {
        get
        {
            return m_bFlag;
        }
        set
        {
            m_bFlag = value;
        }
    }

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
            m_Thread.Join();  // UI와 별도로 백그라운드에서 대기
        }

    }

    public new string ToString()
    {
        return $"{m_iNo}번 객체";
    }

    private void Run()
    {
        try
        {
            //OnThreadMessage?.Invoke(this, "Start Thread");

            for (int i = 0; i < 10; i++)
            {
                if (m_bFlag == false)
                    break;
                OnThreadMessage?.Invoke(this, $"index: {i}");
                Random random = new Random();
                int iSleep = random.Next(100, 999);
                Thread.Sleep(iSleep);
            }

            //OnThreadMessage?.Invoke(this, "End Thread");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }
}