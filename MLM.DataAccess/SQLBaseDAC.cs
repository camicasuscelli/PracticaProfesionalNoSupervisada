using System;
using System.Data;
using MLM.DataAccess.Interfaces;
using System.Data.SqlClient;
using System.Reflection;
using System.Configuration;

namespace MLM.DataAccess
{
    public class SQLBaseDAC : IBaseDAC
    {
        #region --Attributes--

        //carga en la variable el nombre de la clase.
        private static readonly string _classFullName = MethodBase.GetCurrentMethod().DeclaringType.ToString();
        private string _connectionString;
        private string _schema;
        private IDbCommand _oDbCommand;
        private IDbConnection _oDbConnection;
        private IDbTransaction _oDbTransaction;

        #endregion --Attributes--

        #region --Properties--

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public string Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        public string Package
        {
            get { return string.Empty; }
            set { }
        }

        #endregion --Properties--

        #region --Constructors & Destructors--

        public SQLBaseDAC()
        {
            //esta seteando el string de conexión.
            _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        #endregion --Constructors & Destructors--

        #region --Methods--

        #region --Transaction--

        public IDbTransaction BeginTransaction()
        {
            _oDbTransaction = GetConnection().BeginTransaction();
            return _oDbTransaction;
        }

        public void BeginTransaction(IDbTransaction oTransaction)
        {
            _oDbTransaction = oTransaction;
            CloseConnection();
            _oDbConnection = oTransaction.Connection;
        }

        public void Commit()
        {
            if (_oDbTransaction != null)
                _oDbTransaction.Commit();
            _oDbTransaction = null;
            CloseCommand();
        }

        public void Rollback()
        {
            if (_oDbTransaction != null)
                _oDbTransaction.Rollback();
            _oDbTransaction = null;
            CloseCommand();
        }

        public void Rollback(string transactionName)
        {
            if (_oDbTransaction != null)
                _oDbTransaction.Rollback();
            _oDbTransaction = null;
            CloseCommand();
        }

        #endregion --Transaction--

        #region --Connection--

        public void OpenConnection()
        {
            try
            {
                if (_oDbConnection == null)
                    SetConnection();
                else if (_oDbConnection.State == ConnectionState.Closed)
                    _oDbConnection.Open();
            }
            catch (System.Exception e)
            {
                throw new Exception(_classFullName + ".OpenConnection()", e);
            }
        }

        public void CloseConnection()
        {
            if (_oDbConnection != null)
            {
                if (_oDbConnection.State != ConnectionState.Closed)
                {
                    try
                    {
                        _oDbConnection.Close();
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(_classFullName + ".CloseConnection()", e);
                    }
                }
            }
        }

        public IDbCommand GetCommand()
        {
            try
            {
                OpenCommand();
                _oDbCommand.Parameters.Clear();
                return _oDbCommand;
            }
            catch (System.Exception e)
            {
                throw new Exception(_classFullName + ".GetCommand()", e);
            }
        }

        public void OpenCommand()
        {
            if (_oDbCommand == null)
                _oDbCommand = GetConnection().CreateCommand();
            else
            {
                if (_oDbCommand.Connection.State == ConnectionState.Closed)
                {
                    SetConnection();
                    _oDbCommand.Connection = _oDbConnection;
                }
            }
            if (_oDbTransaction != null)
                _oDbCommand.Transaction = _oDbTransaction;
        }

        public void CloseCommand()
        {
            if (_oDbCommand != null)
            {
                _oDbCommand.Dispose();
                _oDbCommand = null;
                //CloseConnection();
            }
        }

        private void SetConnection()
        {
            try
            {
                _oDbConnection = new SqlConnection(_connectionString);
                try
                {
                    _oDbConnection.Open();
                }
                catch (System.Exception ex1)
                {
                    throw new Exception(_classFullName + ".SetConnection()", ex1);
                }
            }
            catch (System.Exception ex2)
            {
                throw new Exception(_classFullName + ".SetConnection()", ex2);
            }
        }

        private IDbConnection GetConnection()
        {
            OpenConnection();
            return _oDbConnection;
        }

        public IDbDataAdapter CreateDataAdapter(string sql)
        {
            try
            {
                return new SqlDataAdapter(sql, _connectionString);
            }
            catch (System.Exception ex)
            {
                throw new Exception(_classFullName + ".CreateDataAdapter(string sql)", ex);
            }
        }

        public IDbDataAdapter CreateDataAdapter()
        {
            try
            {
                return new SqlDataAdapter();
            }
            catch (System.Exception ex)
            {
                throw new Exception(_classFullName + ".CreateDataAdapter()", ex);
            }
        }
        #endregion

        #endregion --Methods--
    }
}
