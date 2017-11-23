using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Drawing;

namespace rota_praia1
{
    public class DAO
    {

        #region conexao

            OleDbConnection conexao;

            private string LocalDoArquivoBD
            {
                get
                {
                    return System.Environment.CurrentDirectory + "\\dados.accdb\"";
                }
            }
           
            private string SQLConexao
            {
                get
                {
                    return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"" + this.LocalDoArquivoBD + ";Persist Security Info=False;";
                }
            }

            public OleDbConnection Conectar(bool abrir = true)
            {
                conexao = new OleDbConnection(SQLConexao);
                if (abrir)
                    Abrir();
                return conexao;
            }

            public void Abrir()
            {
                Abrir(conexao);
            }

            public void Abrir(OleDbConnection conn)
            {
                conn.Open();
            }

            public void FecharConexao()
            {
                conexao.Close();
            }

            private DataTable LerTabela(string sql, bool manterConexaoAberta = true)
            {
                if (conexao == null)
                    Conectar();

                OleDbCommand command = new OleDbCommand(sql, conexao);
                DataTable dt = new DataTable();
                try
                {
                    if (command.Connection.State == ConnectionState.Closed)
                        conexao.Open();

                    OleDbDataAdapter aDa = new OleDbDataAdapter(command);
                    aDa.Fill(dt);

                    if (!manterConexaoAberta)
                        conexao.Close();
                }
                catch (OleDbException) { }

                return dt;
            }

            private void ExecutarComando(OleDbCommand command, bool manterConexaoAberta = true)
            {
                try
                {
                    if (command.Connection == null)
                    {
                        Conectar();
                        command.Connection = conexao;
                    }
                    if (command.Connection.State == ConnectionState.Closed)
                        command.Connection.Open();

                    int i = command.ExecuteNonQuery();

                    if (!manterConexaoAberta)
                        command.Connection.Close();
                }
                catch (OleDbException)
                {
                }
            }

            //

            public int LerNovoId(string tabela, string campo = "Id")
            {
                string sql = "select max(" + campo + ") + 1 FROM " + tabela;
                string valor = LerTabela(sql).Rows[0][0].ToString();
                if (valor != "")
                    return Int32.Parse(valor);
                else
                    return 1;
            }

        #endregion

        #region selects

            #region Grupos cadastrados

                public DataTable LerGrupos_DataTable()
                {
                    string sql = "SELECT * FROM Grupo " +
                                 " WHERE 1=1";

                    sql += " ORDER BY Id";
                    return LerTabela(sql);
                }

                public List<Grupo> LerGrupos()
                {
                    DataTable dt = LerGrupos_DataTable();
                    List<Grupo> grupos = new List<Grupo>();

                    grupos = (
                               from row in dt.AsEnumerable()
                               select new Grupo
                               {
                                   Id = (int)row["Id"],
                                   Data = DateTime.Parse(row["Data"].ToString()),
                                   Titulo = row["Titulo"].ToString(),
                                   Descricao = row["Descricao"].ToString(),
                               }
                            ).ToList();

                    return grupos;
                }

            #endregion

            #region Acfts cadastradas para o grupo

                private DataTable LerAcfts_DataTable(Grupo grupo)
                {
                    string sql = "SELECT * FROM Acft " + 
                                 " WHERE 1=1";
                    if (grupo != null)
                        sql += "   AND Id_Grupo = " + grupo.Id;
                    
                    sql += " ORDER BY Id";
                    return LerTabela(sql);
                }

                public List<Acft> LerAcfts(Grupo grupo)
                {
                    DataTable dt = LerAcfts_DataTable(grupo);
                    List<Acft> acfts = new List<Acft>();

                    acfts = (
                               from row in dt.AsEnumerable()
                               select new Acft
                               {
                                   donoGrupo = grupo,
                                   Id = (int)row["ID"],
                                   Matricula = row["Matricula"].ToString(),
                                   Tipo = row["Tipo"].ToString(),
                                   Velocidade = (int)row["Velocidade"],
                                   Altitude = (int)row["Altitude"],
                                   
                                   //CorFonteMatricula = Color.FromName(row["CorFonteMatricula"].ToString()),
                                   //CorFonteAltitude = Color.FromName(row["CorFonteAltitude"].ToString()),
                                   //CorLinhas = Color.FromName(row["CorLinhas"].ToString()),
                                   //CorPerimetro = Color.FromName(row["CorPerimetro"].ToString()),

                                   CorFonteMatricula = Cores.toColor(row["CorFonteMatricula"].ToString()),
                                   CorFonteAltitude = Cores.toColor(row["CorFonteAltitude"].ToString()),
                                   CorLinhas = Cores.toColor(row["CorLinhas"].ToString()),
                                   CorPerimetro = Cores.toColor(row["CorPerimetro"].ToString()),

                                   MostrarMatricula = (bool)row["MostrarMatricula"],
                                   MostrarRumo = (bool)row["MostrarRumo"],
                                   MostrarPerimetro = (bool)row["MostrarPerimetro"]
                               }
                       
                            ).ToList();

                    List<Local> locais = new List<Local>();
                    foreach (var acft in acfts)
                        acft.locais = LerLocais(grupo, acft);

                    return acfts;
                }

            #endregion

            #region Locais da acft

                private DataTable LerLocais_DataTable(Grupo grupo, Acft acft)
                {
                    string sql = "SELECT * FROM Locais " + 
                                 " WHERE 1=1";

                    if (grupo != null)
                        sql += " AND Id_Grupo = " + grupo.Id;

                    if (acft != null)
                        sql += " AND Id_Acft = " + acft.Id;
                    else
                        sql += " AND Id_Acft IS NULL ";

                    sql += " ORDER BY Ordem";

                    return LerTabela(sql);
                }
                
                public List<Local> LerLocais(Grupo grupo, Acft acft)
                {
                    DataTable dt = LerLocais_DataTable(grupo, acft);
                    List<Local> locais = new List<Local>();

                    locais = (
                                from row in dt.AsEnumerable()
                                select new Local()
                                {
                                    donoGrupo = grupo,
                                    donoAcft = acft,
                                    Nome = row["Nome"].ToString(),
                                    Tipo = row["Tipo"].ToString(),
                                    Altitude = (int)row["Altitude"],
                                    X = (int)row["X"],
                                    Y = (int)row["Y"]
                                }
                            ).ToList();

                    return locais;
                }

            #endregion

        #endregion

        #region inserts cadastro básico

            public void InserirAlterarGrupo(List<Grupo> grupos, bool manterConexaoAberta = true)
            {
                if (grupos != null)
                    foreach (var grupo in grupos)
                        InserirAlterarGrupo(grupo, manterConexaoAberta);
            }

            public void InserirAlterarGrupo(Grupo grupo, bool manterConexaoAberta = true)
            {
                OleDbCommand command = null;
                string sql = "";

                if (grupo.Id == 0)
                {
                    sql += " INSERT INTO Grupo                    ";
                    sql += " (                                    ";
                    sql += "   Id, Data, Titulo, Descricao        ";
                    sql += " )                                    ";
                    sql += " VALUES                               ";
                    sql += " (                                    ";
                    sql += "   @Id, @Data, @Titulo, @Descricao    ";
                    sql += " )                                    ";

                    command = new OleDbCommand(sql, conexao);
                    
                    grupo.Id = LerNovoId("Grupo");

                    command.Parameters.AddWithValue("@Id", grupo.Id);
                    command.Parameters.AddWithValue("@Data", grupo.Data);
                    command.Parameters.AddWithValue("@Titulo", grupo.Titulo);
                    command.Parameters.AddWithValue("@Descricao", grupo.Descricao);
                }
                else
                {
                    sql += "UPDATE Grupo SET                       ";
                    sql += "       Data              = @Data,      ";
                    sql += "       Titulo            = @Titulo,    ";
                    sql += "       Descricao         = @Descricao  ";
                    
                    sql += " WHERE Id = @Id ";

                    command = new OleDbCommand(sql, conexao);

                    command.Parameters.AddWithValue("@Data", grupo.Data);
                    command.Parameters.AddWithValue("@Titulo", grupo.Titulo);
                    command.Parameters.AddWithValue("@Descricao", grupo.Descricao);
                    //where
                    command.Parameters.AddWithValue("@Id", grupo.Id);
                }

                ExecutarComando(command, manterConexaoAberta);
            }

            public void InserirAlterarAcft(List<Acft> acfts, bool manterConexaoAberta = true)
            {
                if (acfts != null)
                    foreach (var acft in acfts)
                        InserirAlterarAcft(acft, manterConexaoAberta);
            }

            public void InserirAlterarAcft(Acft acft, bool manterConexaoAberta = true)
            {
                OleDbCommand command = null;
                string sql = "";

                if (acft.Id == 0)
                {
                    sql += " INSERT INTO Acft                                                    ";
                    sql += " (                                                                   ";
                    sql += "   Id_Grupo, Id, Matricula, Tipo, Velocidade, Altitude,              ";
                    sql += "   CorFonteMatricula, CorFonteAltitude, CorLinhas, CorPerimetro,     ";
                    sql += "   MostrarMatricula, MostrarRumo, MostrarPerimetro                   ";
                    sql += " )                                                                   ";
                    sql += " VALUES                                                              ";
                    sql += " (                                                                   ";
                    sql += "   @Id_Grupo, @Id, @Matricula, @Tipo, @Velocidade, @Altitude,        ";
                    sql += "   @CorFonteMatricula, @CorFonteAltitude, @CorLinhas, @CorPerimetro, ";
                    sql += "   @MostrarMatricula, @MostrarRumo, @MostrarPerimetro                ";
                    sql += " )                                                                   ";

                    command = new OleDbCommand(sql, conexao);
                    
                    acft.Id = LerNovoId("Acft");

                    command.Parameters.AddWithValue("@Id_Grupo", acft.donoGrupo.Id);
                    command.Parameters.AddWithValue("@Id", acft.Id);
                    command.Parameters.AddWithValue("@Matricula", acft.Matricula);
                    command.Parameters.AddWithValue("@Tipo", acft.Tipo);
                    command.Parameters.AddWithValue("@Velocidade", acft.Velocidade);
                    command.Parameters.AddWithValue("@Altitude", acft.Altitude);

                    command.Parameters.AddWithValue("@CorFonteMatricula", Cores.toARGB(acft.CorFonteMatricula));
                    command.Parameters.AddWithValue("@CorFonteAltitude", Cores.toARGB(acft.CorFonteAltitude));
                    command.Parameters.AddWithValue("@CorLinhas", Cores.toARGB(acft.CorLinhas));
                    command.Parameters.AddWithValue("@CorPerimetro", Cores.toARGB(acft.CorPerimetro));

                    command.Parameters.AddWithValue("@MostrarMatricula", acft.MostrarMatricula);
                    command.Parameters.AddWithValue("@MostrarRumo", acft.MostrarRumo);
                    command.Parameters.AddWithValue("@MostrarPerimetro", acft.MostrarPerimetro);
                }
                else
                {
                    sql += "UPDATE Acft SET                               ";
                    sql += "       Matricula         = @Matricula,         ";
                    sql += "       Tipo              = @Tipo,              ";
                    sql += "       Velocidade        = @Velocidade,        ";
                    sql += "       Altitude          = @Altitude,          ";

                    sql += "       CorFonteMatricula = @CorFonteMatricula, ";
                    sql += "       CorFonteAltitude  = @CorFonteAltitude,  ";
                    sql += "       CorLinhas         = @CorLinhas,         ";
                    sql += "       CorPerimetro      = @CorPerimetro,      ";

                    sql += "       MostrarMatricula  = @MostrarMatricula,  ";
                    sql += "       MostrarRumo       = @MostrarRumo,       ";
                    sql += "       MostrarPerimetro  = @MostrarPerimetro   ";
                    
                    sql += " WHERE Id = @Id ";

                    command = new OleDbCommand(sql, conexao);

                    command.Parameters.AddWithValue("@Matricula", acft.Matricula);
                    command.Parameters.AddWithValue("@Tipo", acft.Tipo);
                    command.Parameters.AddWithValue("@Velocidade", acft.Velocidade);
                    command.Parameters.AddWithValue("@Altitude", acft.Altitude);

                    command.Parameters.AddWithValue("@CorFonteMatricula", Cores.toARGB(acft.CorFonteMatricula));
                    command.Parameters.AddWithValue("@CorFonteAltitude", Cores.toARGB(acft.CorFonteAltitude));
                    command.Parameters.AddWithValue("@CorLinhas", Cores.toARGB(acft.CorLinhas));
                    command.Parameters.AddWithValue("@CorPerimetro", Cores.toARGB(acft.CorPerimetro));

                    command.Parameters.AddWithValue("@MostrarMatricula", acft.MostrarMatricula);
                    command.Parameters.AddWithValue("@MostrarRumo", acft.MostrarRumo);
                    command.Parameters.AddWithValue("@MostrarPerimetro", acft.MostrarPerimetro);
                    //where
                    command.Parameters.AddWithValue("@Id", acft.Id);
                }

                ExecutarComando(command, manterConexaoAberta);

                ExcluirLocaisAcft(acft);
                InserirLocais(acft.locais);
            }
            
            //

            public void InserirLocais(List<Local> locais, bool manterConexaoAberta = true)
            {
                int i = 1;
                if (locais != null)
                    foreach (var local in locais)
                    {
                        local.Id = 0;
                        local.Ordem = i++;
                        InserirLocal(local, manterConexaoAberta);
                    }
            }

            public void InserirLocal(Local local, bool manterConexaoAberta = true)
            {
                OleDbCommand command = null;
                string sql = "";

                if (local.Id == 0)
                {
                    sql += " INSERT INTO Locais                                ";
                    sql += " (                                                 ";

                    if (local.donoGrupo != null)
                    sql += "   Id_Grupo,                                       ";

                    if (local.donoAcft != null)
                    sql += "   Id_Acft,                                        ";

                    sql += "   Id, Nome, Tipo, Altitude,                       ";
                    sql += "   X, Y,                                           ";
                    //sql += "   CorFonteMatricula, CorFonteAltitude             ";
                    sql += "   Ordem                                           ";
                    sql += " )                                                 ";
                    sql += " VALUES                                            ";
                    sql += " (                                                 ";

                    if (local.donoGrupo != null)
                    sql += "   @Id_Grupo,                                      ";

                    if (local.donoAcft != null)
                    sql += "   @Id_Acft,                                       ";
                    
                    sql += "   @Id, @Nome, @Tipo, @Altitude,                   ";
                    sql += "   @X, @Y,                                         ";
                    //sql += "   @CorFonteMatricula, @CorFonteAltitude           ";
                    sql += "   @Ordem                                          ";
                    sql += " )                                                 ";

                    command = new OleDbCommand(sql, conexao);

                    local.Id = LerNovoId("Locais");

                    if (local.donoGrupo != null)
                    command.Parameters.AddWithValue("@Id_Grupo", local.donoGrupo.Id);

                    if (local.donoAcft != null)
                    command.Parameters.AddWithValue("@Id_Acft", local.donoAcft.Id);

                    command.Parameters.AddWithValue("@Id", local.Id);
                    command.Parameters.AddWithValue("@Nome", local.Nome);
                    command.Parameters.AddWithValue("@Tipo", local.Tipo);
                    command.Parameters.AddWithValue("@Altitude", local.Altitude);
                    command.Parameters.AddWithValue("@X", local.X);
                    command.Parameters.AddWithValue("@Y", local.Y);
                    //command.Parameters.AddWithValue("@CorFonteMatricula", local.CorFonteMatricula);
                    //command.Parameters.AddWithValue("@CorFonteAltitude", local.CorFonteAltitude);
                    command.Parameters.AddWithValue("@Ordem", local.Ordem);
                }
                else
                {
                    /*
                    sql += "UPDATE Locais SET                              ";
                    sql += "       Nome              = @Nome,              ";
                    sql += "       Tipo              = @Tipo,              ";
                    sql += "       Altitude          = @Altitude,          ";

                    sql += "       X                 = @X,                 ";
                    sql += "       Y                 = @Y,                 ";

                    sql += "       CorFonteMatricula = @CorFonteMatricula, ";
                    sql += "       CorFonteAltitude  = @CorFonteAltitude   ";

                    sql += " WHERE Id = @Id ";

                    command = new OleDbCommand(sql, conexao);

                    command.Parameters.AddWithValue("@Nome", local.Nome);
                    command.Parameters.AddWithValue("@Tipo", local.Tipo);
                    command.Parameters.AddWithValue("@Altitude", local.Altitude);
                    command.Parameters.AddWithValue("@X", local.X);
                    command.Parameters.AddWithValue("@Y", local.Y);
                    //command.Parameters.AddWithValue("@CorFonteMatricula", local.);
                    //command.Parameters.AddWithValue("@CorFonteAltitude", local.);
                    //where
                    command.Parameters.AddWithValue("@Id", local.Id);
                    */
                }

                ExecutarComando(command, manterConexaoAberta);
            }

        #endregion

        #region deletes

            public void ExcluirAcft(Acft acft, bool manterConexaoAberta = true)
            {
                string sql = "DELETE FROM Acft     " +
                             " WHERE Id = @Id      ";

                OleDbCommand command = new OleDbCommand(sql, conexao);

                command.Parameters.AddWithValue("@Id", acft.Id);

                ExecutarComando(command, manterConexaoAberta);
            }
            
            public void ExcluirLocaisGrupo(Grupo grupo, bool manterConexaoAberta = true)
            {
                if (grupo == null)
                    return;

                string sql = "DELETE FROM Locais           " +
                             " WHERE Id_Grupo = @Id_Grupo  " +
                             "   AND Id_Acft IS NULL       ";

                OleDbCommand command = new OleDbCommand(sql, conexao);

                command.Parameters.AddWithValue("@Id_Grupo", grupo.Id);

                ExecutarComando(command, manterConexaoAberta);
            }

            public void ExcluirLocaisAcft(Acft acft, bool manterConexaoAberta = true)
            {
                if (acft == null)
                    return;

                string sql = "DELETE FROM Locais        " +
                             " WHERE id_acft = @id_acft ";

                OleDbCommand command = new OleDbCommand(sql, conexao);

                command.Parameters.AddWithValue("@id_acft", acft.Id);

                ExecutarComando(command, manterConexaoAberta);
            }
            
        #endregion

    }

}