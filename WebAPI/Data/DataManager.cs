using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5meProjects.Data
{
    public class DataManager
    {
        private DataManager m_Intance;

        public DataManager Intance
        {
            get
            {
                if (m_Intance == null)
                    m_Intance = new DataManager();
                return m_Intance;
            }
        }

        private DataManager()
        {

        }


    }
}
