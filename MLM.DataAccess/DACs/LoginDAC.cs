using System;
using System.Data;
using MLM.Common.DTO;
using MLM.Common;
using System.Reflection;

namespace MLM.DataAccess.DACs
{
    public class LoginDAC : SQLBaseDAC
    {
        #region --Attributes--

        private static readonly string _classFullName = MethodBase.GetCurrentMethod().DeclaringType.ToString();

        #endregion --Attributes--

        public LoginDAC()
        {
            base.OpenConnection();
        }

        public Usuario getUser(string user)
        {

            Usuario oUsuario = null;
            IDataReader oReader = null;
            //que hace el using acá?:
            /*
             * LO que se declara entre using(___). Se dispara el dispose.
             * */
            using (IDbCommand oCommand = base.GetCommand())
            {
                //indico el tipo de comando.
                oCommand.CommandType = CommandType.StoredProcedure;
                //indico el nombre del store procedure.
                oCommand.CommandText = "GET_USER";

                //creo un parámetro.
                IDbDataParameter oParam0 = (IDbDataParameter)oCommand.CreateParameter();
                oParam0.ParameterName = "@USER";
                //es string porque en la bd es un varchar.
                oParam0.DbType = DbType.AnsiString;
                oParam0.Value = user;
                oCommand.Parameters.Add(oParam0);

                try
                {
                    oReader = oCommand.ExecuteReader();
                    oUsuario = new Usuario();
                    while (oReader.Read())
                    {
                        //getOrdinal parsea el nombre del campo, es el campo de la tabla.
                        oUsuario.Nombre = oReader[oReader.GetOrdinal("NOMBRE")].ToString();
                        oUsuario.Apellido = oReader[oReader.GetOrdinal("APELLIDO")].ToString();
                        oUsuario.Dni = int.Parse(oReader[oReader.GetOrdinal("DNI")].ToString());
                    }
                    base.CloseCommand();
                }
                catch (System.Exception ex)
                {
                    Logging.Instance.LogError(_classFullName + ".getUser(string user) -> " + ex.Message.ToString(), ex);
                }
                //se ejecuta SIEMPRE, haya error o no.
                finally
                {
                    base.CloseCommand();
                    if (oReader != null)
                    {
                        oReader.Close();
                        //oReader.Dispose();
                        oReader = null;
                    }
                }
            }
            return oUsuario;
        }

        //ahora hay que hacer un add user con parámetros nombre, apellido, dni.
        public void addUser(string nombre, string apellido, int dni)
        {
            using (IDbCommand oCommand = base.GetCommand())
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.CommandText = "ADD_USER";

                //creo parametros para pasarle al storeprocedure

                IDbDataParameter oParam0 = (IDbDataParameter)oCommand.CreateParameter();
                oParam0.ParameterName = "@NAME";
                //es string porque en la bd es un varchar.
                oParam0.DbType = DbType.AnsiString;
                oParam0.Value = nombre;
                oCommand.Parameters.Add(oParam0);

                IDbDataParameter oParam1 = (IDbDataParameter)oCommand.CreateParameter();
                oParam1.ParameterName = "@LASTNAME";
                //es string porque en la bd es un varchar.
                oParam1.DbType = DbType.AnsiString;
                oParam1.Value = apellido;
                oCommand.Parameters.Add(oParam1);

                IDbDataParameter oParam2 = (IDbDataParameter)oCommand.CreateParameter();
                oParam2.ParameterName = "@DNI";
                //es int porque en la bd es un int.
                oParam2.DbType = DbType.Int16;
                oParam2.Value = dni;
                oCommand.Parameters.Add(oParam2);
                try
                {
                    //ejecuto pero no devuelve query o eso entendí equisde.Esto es porque queremos escribir solamente.
                    oCommand.ExecuteNonQuery();
                    //base.CloseCommand();
                }
                catch (System.Exception ex)
                {
                    Logging.Instance.LogError(_classFullName + ".getUser(string user) -> " + ex.Message.ToString(), ex);
                }
                //se ejecuta SIEMPRE, haya error o no.
                finally
                {
                    base.CloseCommand();
                }

            }
        }
    }

    
}
