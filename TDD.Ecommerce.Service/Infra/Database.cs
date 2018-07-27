using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace TDD.Ecommerce.Service.Infra
{

    public interface IConnectionDB
    {
        void ExecuteNonQuery(string comando);
        DataRow ExecuteDataRow(string comando);
        DataTable ExecuteDataTable(string comando);
        DataSet ExecuteDataSet(string comando);

        int Somar(int a, int b);
    }

    public class ConnectionDB : IConnectionDB
    {
        /// <summary>
        /// conexao com o banco de dados
        /// </summary>
        public Database db;
        /// <summary>
        /// dbconnection de conexao com o banco de dados
        /// </summary>
        public DbConnection cnn;

        public ConnectionDB()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            DatabaseFactory.SetDatabaseProviderFactory(factory, false);
            db = DatabaseFactory.CreateDatabase();
            cnn = db.CreateConnection();
        }

        public int Somar(int a, int b)
        {
            return -1;
        }

        #region [ METODOS DE BAIXO NÍVEL ]
        /// <summary>
        /// Executa uma query no BD.
        /// </summary>
        /// <param name="comando"></param>
        public void ExecuteNonQuery(string comando)
        {
            if (string.IsNullOrEmpty(comando))
                return;

            DbCommand cmd = db.GetSqlStringCommand(comando);
            cmd.Connection = cnn;
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Executa uma query no BD.
        /// </summary>
        /// <param name="comando"></param>
        /// <returns></returns>
        public object ExecuteScalar(string comando)
        {
            if (string.IsNullOrEmpty(comando))
                return null;

            DbCommand cmd = db.GetSqlStringCommand(comando);
            cmd.Connection = cnn;
            try
            {
                return db.ExecuteScalar(cmd);
            }
            catch { return null; }
        }

        /// <summary>
        /// Executa uma query que retorna multiplas linhas
        /// </summary>
        /// <param name="comando"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string comando)
        {
            if (string.IsNullOrEmpty(comando))
                return null;

            DbCommand cmd = db.GetSqlStringCommand(comando);
            cmd.Connection = cnn;
            return db.ExecuteReader(cmd);
        }


        /// <summary>
        /// Método responsável por executar uma consulta e retornar um conjunto de dados
        /// a partir da query informada como parametro
        /// </summary>
        /// <param name="comando">query informada como parametro</param>
        /// <returns>um datarow da primeira linha da query retornada como parametro</returns>
        public DataRow ExecuteDataRow(string comando)
        {
            if (string.IsNullOrEmpty(comando))
                return null;

            DbCommand cmd = db.GetSqlStringCommand(comando);
            cmd.Connection = cnn;
            DataSet ds = db.ExecuteDataSet(cmd);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0];
            else
                return null;
        }

        public DataTable ExecuteDataTable(string comando)
        {
            if (string.IsNullOrEmpty(comando))
                return null;

            DbCommand cmd = db.GetSqlStringCommand(comando);
            cmd.Connection = cnn;
            DataSet ds = db.ExecuteDataSet(cmd);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        public DataSet ExecuteDataSet(string comando)
        {
            if (string.IsNullOrEmpty(comando))
                return null;

            DbCommand cmd = db.GetSqlStringCommand(comando);
            cmd.Connection = cnn;
            DataSet ds = db.ExecuteDataSet(cmd);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }

        #endregion
    }


    //public class ConnectionDBFake : IConnectionDB
    //{
    //    public DataRow ExecuteDataRow(string comando)
    //    {

    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("ID_DESCONTO");
    //        dt.Columns.Add("ST_DESCRICAO");
    //        dt.Columns.Add("NM_VALOR_COMPRA");
    //        dt.Columns.Add("NM_TAXA_DESCONTO");

    //        DataRow valorDescontoRow = dt.NewRow();

    //        valorDescontoRow["ID_DESCONTO"] = 10;
    //        valorDescontoRow["ST_DESCRICAO"] = "CompraBasica";
    //        valorDescontoRow["NM_VALOR_COMPRA"] = 500;
    //        valorDescontoRow["NM_TAXA_DESCONTO"] = 0.90;

    //        return valorDescontoRow;
    //    }

    //    public DataSet ExecuteDataSet(string comando)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public DataTable ExecuteDataTable(string comando)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void ExecuteNonQuery(string comando)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public int Somar(int a, int b)
    //    {
    //        return 0;
    //    }
    //}
}
