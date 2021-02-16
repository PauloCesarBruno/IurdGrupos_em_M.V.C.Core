using IurdGrupos.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IurdGrupos.Models
{
    public class ModelGrupo
    {
        public String Id { get; set; }

        [Display(Name = "Grupo Nome")]
        [MinLength(3, ErrorMessage = "Minimo 03 Caracteres !")]
        public String GrupoNome { get; set; }

        public List<ModelGrupo> ListarTodosGrupos()
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametros();
                List<ModelGrupo> lista = new List<ModelGrupo>();
                ModelGrupo item;
                DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, "Select Id, GrupoNome From tbl_Grupos Order By GrupoNome");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ModelGrupo()
                    {
                        Id = dt.Rows[i]["Id"].ToString(),
                        GrupoNome = dt.Rows[i]["GrupoNome"].ToString()
                    };
                    lista.Add(item);
                    objDAL.FecharConexao();
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Filtrar Grupo Pelo Nome
        //=================================================================================================================================

        public List<ModelGrupo> ListarTodosGruposNome(String nome)
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametros();
                List<ModelGrupo> lista = new List<ModelGrupo>();
                ModelGrupo item;
                DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, $"Select Id, GrupoNome From tbl_Grupos where GrupoNome = '{nome}'");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ModelGrupo()
                    {
                        Id = dt.Rows[i]["Id"].ToString(),
                        GrupoNome = dt.Rows[i]["GrupoNome"].ToString()
                    };
                    lista.Add(item);
                    objDAL.FecharConexao();
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ModelGrupo RetornarGrupoId(int? Id)
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametros();
                ModelGrupo item;
                DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, $"Select Id, GrupoNome From tbl_Grupos Where Id = '{Id}'");

                item = new ModelGrupo()
                {
                    Id = dt.Rows[0]["Id"].ToString(),
                    GrupoNome = dt.Rows[0]["GrupoNome"].ToString()
                };
                objDAL.FecharConexao();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GravarGrupo()
        {
            try
            {
                if (Id != null)
                {
                    DAL objDAL = new DAL();
                    objDAL.LimparParametros();
                    objDAL.AddParametros("Id", Id);
                    objDAL.AddParametros("GrupoNome", GrupoNome);
                    String IdGrupo = objDAL.ExecutarManipulacao(CommandType.StoredProcedure, "Alterar").ToString();
                    objDAL.FecharConexao();
                }
                else
                {
                    DAL objDAL = new DAL();
                    objDAL.LimparParametros();
                    objDAL.AddParametros("GrupoNome", GrupoNome);
                    String IdGrupo = objDAL.ExecutarManipulacao(CommandType.Text, "Insert Into tbl_Grupos(GrupoNome) Values (@GrupoNome)").ToString();
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
                String IdGrupo = objDAL.ExecutarManipulacao(CommandType.StoredProcedure, "Excluir").ToString();
                objDAL.FecharConexao();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
