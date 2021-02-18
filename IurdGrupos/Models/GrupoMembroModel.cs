using IurdGrupos.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IurdGrupos.Models
{
    public class GrupoMembroModel
    {
        public int Id { get; set; }
        public String IdGrupo { get; set; }
        public String GrupoNome { get; set; }
        public String IdUsuario { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }

        public List<GrupoMembroModel> RetornarListagem()
        {
            List<GrupoMembroModel> lista = new List<GrupoMembroModel>();
            GrupoMembroModel item;
            DAL objDAL = new DAL();
            objDAL.LimparParametros();

            // Consulta Para Listar a Relação -> INNER JOIN

            DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, "Select tbl_GrupoUsuario.id, IdGrupo, GrupoNome, IdUsuario, Nome, Email " +
                "From tbl_GrupoUsuario Inner Join tbl_Grupos on tbl_Grupos.Id = tbl_GrupoUsuario.IdGrupo " +
                "Inner Join tbl_Usuarios on IdUsuario = tbl_Usuarios.Id Order By tbl_GrupoUsuario.IdGrupo");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new GrupoMembroModel()
                {
                    Id = int.Parse(dt.Rows[i]["Id"].ToString()),
                    IdGrupo = dt.Rows[i]["IdGrupo"].ToString(),
                    GrupoNome = dt.Rows[i]["GrupoNome"].ToString(),
                    IdUsuario = dt.Rows[i]["IdUsuario"].ToString(),
                    Nome = dt.Rows[i]["Nome"].ToString(),
                    Email = dt.Rows[i]["Email"].ToString()
                };
                lista.Add(item);
                objDAL.FecharConexao();
            }
            return lista;
        }

        public List<GrupoMembroModel> RetornarListagemNome(String grupoNome)
        {
            List<GrupoMembroModel> lista = new List<GrupoMembroModel>();
            GrupoMembroModel item;
            DAL objDAL = new DAL();
            objDAL.LimparParametros();

            // Consulta Para Listar a Relação -> INNER JOIN

            DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, $"Select tbl_GrupoUsuario.id, IdGrupo, GrupoNome, IdUsuario, Nome, Email From tbl_GrupoUsuario Inner Join tbl_Grupos on tbl_Grupos.Id = tbl_GrupoUsuario.IdGrupo Inner Join tbl_Usuarios on IdUsuario = tbl_Usuarios.Id Where GrupoNome = '{grupoNome}' Order By tbl_GrupoUsuario.IdGrupo");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new GrupoMembroModel()
                {
                    Id = int.Parse(dt.Rows[i]["Id"].ToString()),
                    IdGrupo = dt.Rows[i]["IdGrupo"].ToString(),
                    GrupoNome = dt.Rows[i]["GrupoNome"].ToString(),
                    IdUsuario = dt.Rows[i]["IdUsuario"].ToString(),
                    Nome = dt.Rows[i]["Nome"].ToString(),
                    Email = dt.Rows[i]["Email"].ToString()
                };
                lista.Add(item);
                objDAL.FecharConexao();
            }
            return lista;
        }

        public void Inserir()
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametros();

                String sql = "Insert Into tbl_GrupoUsuario(IdGrupo, IdUsuario)" +
                    $"Values('{IdGrupo}', '{IdUsuario}')";                    

                DataTable dt = objDAL.RetDatatable(sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = "Insert Into tbl_Usuarios (Nome, Email)" +
                        $"Values({dt.Rows[i]["Nome"]}, {dt.Rows[i]["Email"]}";
                    objDAL.RetDatatable(sql);
                }

                for (int g = 0; g < dt.Rows.Count; g++)
                {
                    sql = "Insert Into tbl_Grupos (GrupoNome)" +
                        $"Values({dt.Rows[g]["GrupoNome"]}";
                    objDAL.RetDatatable(sql);
                    objDAL.FecharConexao();
                }
            }
            catch
            {
                //
            }
        }

        public void Excluir(int? Id)
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametros();
                objDAL.AddParametros("Id", Id);
                String IdMembro = objDAL.ExecutarManipulacao(CommandType.Text, $"Delete tbl_GrupoUsuario Where Id = '{Id}'").ToString();
                objDAL.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Para não ter que codificar aqi onde já esta codificado apenas Istancio.
        public List<ModelGrupo> RetornarListaGrupo()
        {
            return new ModelGrupo().ListarTodosGrupos();
        }

        // Para não ter que codificar aqi onde já esta codificado apenas Istancio.
        public List<ModelMembro> RetornarListaMembro()
        {
            return new ModelMembro().ListarTodosMembros();
        }
    }
}
