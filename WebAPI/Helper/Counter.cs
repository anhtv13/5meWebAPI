using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Log4net
{
    public class Counter
    {
        private object m_lock = new object();
        private const int m_maxCounter = 20;
        private int m_counter = 0;

        private static Counter m_instance;

        public static Counter Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new Counter();
                return m_instance;
            }
        }

        private Counter() { }

        public int CurrentCount()
        {
            return m_counter;
        }

        public bool CheckCounter()
        {
            lock (m_lock)
            {
                if (m_counter < m_maxCounter)
                {
                    m_counter++;
                    return true;
                }
                return false;
            }
        }

        public void DecreaseCounter()
        {
            lock (m_lock)
            {
                m_counter--;
            }
        }
    }
}
