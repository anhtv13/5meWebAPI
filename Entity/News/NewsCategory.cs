using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.News
{
    public class NewsCategory
    {
        private string m_code;

        public string Code
        {
            get { return m_code; }
            set { m_code = value; }
        }
        private string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public NewsCategory(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }

        public NewsCategory() { }
    }
}
