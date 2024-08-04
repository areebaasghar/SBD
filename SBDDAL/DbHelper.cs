using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlClient;

namespace SBDDAL
{
    public class DbHelper
    {

        public static SqlConnection GetConnection()

        {
            return new SqlConnection("Data Source=MUHAMMAD-UMER;Initial Catalog=SBD_db;Integrated Security=True;TrustServerCertificate=True");
        }

    }
}
