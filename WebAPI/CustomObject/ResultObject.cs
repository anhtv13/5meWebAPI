using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5meProjects.CustomObject
{
    /// <summary>
    /// Trả về kết quả xử lí ở tầng manager
    /// nếu ok trả về result = true
    /// nếu fail vì 1 lí do nào đó trả về false kèm msg báo lỗi
    /// </summary>
    public class ResultObject
    {
        private bool m_result;

        public bool Result
        {
            get { return m_result; }
            set { m_result = value; }
        }
        private string m_errorMessage;

        public string ErrorMessage
        {
            get { return m_errorMessage; }
            set { m_errorMessage = value; }
        }

        public ResultObject(){}

        public ResultObject(bool result, string errorMessage)
        {
            this.Result = result;
            this.ErrorMessage = errorMessage;
        }
    }
}
