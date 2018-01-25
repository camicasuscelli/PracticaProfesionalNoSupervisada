using System.Data;

namespace MLM.DataAccess.Interfaces
{
    public interface IBaseDAC
    {
        //esta interfaz
        string ConnectionString { get; set; }
        string Schema { get; set; }
        string Package { get; set; }
        IDbTransaction BeginTransaction();
        void BeginTransaction(IDbTransaction oTransaction);
        void Commit();
        void Rollback();
        void OpenConnection();
        void CloseConnection();
        IDbCommand GetCommand();
        void OpenCommand();
        void CloseCommand();
        IDbDataAdapter CreateDataAdapter(string sql);
        IDbDataAdapter CreateDataAdapter();
    }
}
