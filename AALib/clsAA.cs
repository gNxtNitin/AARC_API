using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

//AARCreviewer Wb13UqDjKaBV
//AARCadmin $$J22E34OwwIfO!!  -- Expired:JKVD6lvo5tLt
//AARCoperator d393rfR9#JJ33  -- Expired: iAn6aBEOG40X
//EPICReviewer 7ehfdjwW(d


namespace AALib
{
    public class clsAA
    {
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Initiate Class
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public clsAA()
        {

        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Enumerations
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public enum validDBs { AARC = 0, EPIC = 1 }
        public enum validAccess { READONLY = 0, WRITE = 1 }


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Constants and Private Variables
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region "Constants and Private Variables"
        private SqlConnection oConn = null;
        private validDBs sDB = 0;
        private string sDBAccess = "";
        private string sDBName = "";
        private string sConnStr = "";
        private string sEnv = "";
        #endregion


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Class Properties
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region "Class Properties"

        /// -----------------------------------------------------------------------------
        /// <summary>Environment set to run in.</summary>
        /// <returns>DEV, TST or PRD. </returns>
        /// <remarks>The database server to connect to is set in the CGADBSOURCE environment variable on the residing server.</remarks>
        /// <history>[RINO F]	11/26/2017	Created</history>
        /// -----------------------------------------------------------------------------
        public string envAA
        {
            get
            {
                if (sEnv == "")
                {
                    GetEnvironment();
                }
                return sEnv;
            }
        }






        #endregion


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Private Subs and Functions
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region "Private Subs and Functions"

        private SqlConnection GetDBConn(validDBs iWhichDB, validAccess iDBAccess)
        {
            SqlConnection connSql = null;

            try
            {
                connSql = new SqlConnection(getConnectionString(iWhichDB, iDBAccess));
            }
            catch
            {
            }
            return connSql;

        }

        private string getConnectionString(validDBs iWhichDB, validAccess iDBAccess)
        {
            //string sDBServer = "AARCSQLPROD"; //prod DataBase Server Name
            //string DBPass = "n7arz6Jv4DWeXTLA"; // Prod DB Password
            string sDBServer = "DESKTOP-22D5T9N\\SQLEXPRESS"; //Dev DataBase Server Name
            string DBPass = "gnxt@123"; // Dev DB Password
            string sDBUser = "";
            string sDBPass = "";
            string sDBCatalog = "AARC_Sandbox";
            string sRet = "";
            try
            {
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                // Set Database Server Name based on Environment
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                switch (GetEnvironment())
                {
                    case "DEV":
                        sDBCatalog = "AARC_Sandbox";
                        break;
                    default:
                        sDBCatalog = "AARC_Sandbox";
                        break;
                }

                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                // Set Uid & Pwd based on Which DB & Access level
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                switch (iWhichDB)
                {
                    case validDBs.EPIC:
                        switch (iDBAccess)
                        {
                            case validAccess.READONLY:
                            default:
                                sDBUser = "sa";
                                sDBPass = DBPass;
                                break;
                        }
                        sRet = "data source=" + sDBServer + ";initial catalog=AARC_Sandbox;user id=" + sDBUser + ";password=" + sDBPass + ";persist security info=true";
                        break;
                    case validDBs.AARC:
                    default:
                        switch (iDBAccess)
                        {
                            case validAccess.WRITE:
                                sDBUser = "sa";
                                sDBPass = DBPass;
                                break;
                            case validAccess.READONLY:
                            default:
                                sDBUser = "sa";
                                sDBPass = DBPass;
                                break;
                        }
                        sRet = "data source=" + sDBServer + ";initial catalog=" + sDBCatalog + ";user id=" + sDBUser + ";password=" + sDBPass + ";persist security info=true";
                        break;
                }
            }
            catch
            {
            }
            return sRet;
        }

        private string GetEnvironment()
        {
            string sEnv = "";
            try
            {
                string sValue = "";

                sValue = Environment.GetEnvironmentVariable("AARCENV");
                if (!string.IsNullOrEmpty(sValue))
                {
                    sEnv = sValue;
                }
                else
                {
                    sEnv = "";
                }
            }
            catch
            {
            }
            return sEnv;
        }

        #endregion



        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Public Methods
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region "PUBLIC SUBS/FUNCTIONS"

        /// -----------------------------------------------------------------------------
        /// <summary>AAExecuteReader - Return SQLDataReader</summary>
        /// <param name="iWhichDB">Select DB from list validDBs Enumerator</param>
        /// <param name="sProc">Stored Procdure to execute</param>
        /// <param name="lstParams">Stored Procedure Parameter Collection</param>
        /// <returns>SQLDataReader</returns>
        /// <remarks></remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public System.Data.SqlClient.SqlDataReader AAExecuteReader(validDBs iWhichDB, string sProc, List<SqlParameter> lstParams)
        {
            System.Data.SqlClient.SqlDataReader drRet = default(System.Data.SqlClient.SqlDataReader);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sProc;

            foreach (SqlParameter myParam in lstParams)
            {
                cmd.Parameters.Add(myParam);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(getConnectionString(iWhichDB, validAccess.READONLY)))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    drRet = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return drRet;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>AAExecuteNonQuery - Return number of rows affected</summary>
        /// <param name="iWhichDB">Select DB from list validDBs Enumerator</param>
        /// <param name="sProc">Stored Procdure to execute</param>
        /// <param name="lstParams">Stored Procedure Parameter Collection</param>
        /// <returns>Int - number of rows affected</returns>
        /// <remarks></remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public int AAExecuteNonQuery(validDBs iWhichDB, string sProc, List<SqlParameter> lstParams)
        {
            int iRet = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sProc;
            string connString = getConnectionString(iWhichDB, validAccess.WRITE);

            foreach (SqlParameter myParam in lstParams)
            {
                cmd.Parameters.Add(myParam);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    iRet = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Pass back to caller
                throw;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return iRet;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>AAExecuteTable - Returns DataTable</summary>
        /// <param name="iWhichDB">Select DB from list validDBs Enumerator</param>
        /// <param name="sProc">Stored Procdure to execute</param>
        /// <param name="lstParams">Stored Procedure Parameter Collection</param>
        /// <returns>DataTable</returns>
        /// <remarks></remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public DataTable AAExecuteTable(validDBs iWhichDB, string mySQL, List<SqlParameter> lstParams)
        {
            DataTable dtRet = default(DataTable);
            try
            {
                dtRet = null;
                DataTable myTbl = new DataTable();
                SqlDataAdapter myAdp = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = mySQL;
                foreach (SqlParameter myParam in lstParams)
                {
                    cmd.Parameters.Add(myParam);
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(getConnectionString(iWhichDB, validAccess.READONLY)))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        myAdp.SelectCommand = cmd;
                        myAdp.Fill(myTbl);
                        dtRet = myTbl;
                    }
                }
                catch (Exception ex)
                {
                    dtRet = null;
                    throw;
                }
            }
            catch
            {
                dtRet = null;
                throw;
            }
            return dtRet;

        }

        /// -----------------------------------------------------------------------------
        /// <summary>AAExecuteSet - Returns DataSet</summary>
        /// <param name="iWhichDB">Select DB from list validDBs Enumerator</param>
        /// <param name="sProc">Stored Procdure to execute</param>
        /// <param name="lstParams">Stored Procedure Parameter Collection</param>
        /// <returns>DataSet</returns>
        /// <remarks></remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public DataSet AAExecuteSet(validDBs iWhichDB, string mySQL, List<SqlParameter> lstParams)
        {
            DataSet dsRet = default(DataSet);
            try
            {
                dsRet = null;
                DataSet myDtSet = new DataSet();
                SqlDataAdapter myAdp = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = mySQL;
                foreach (SqlParameter myParam in lstParams)
                {
                    cmd.Parameters.Add(myParam);
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(getConnectionString(iWhichDB, validAccess.READONLY)))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        myAdp.SelectCommand = cmd;
                        myAdp.Fill(myDtSet);
                        dsRet = myDtSet;
                    }
                }
                catch (Exception ex)
                {
                    dsRet = null;
                    throw;
                }
            }
            catch
            {
                dsRet = null;
                throw;
            }
            return dsRet;

        }




        #endregion

    }
}
