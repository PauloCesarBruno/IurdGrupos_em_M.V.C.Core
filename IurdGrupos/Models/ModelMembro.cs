using IurdGrupos.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IurdGrupos.Models
{
    public class ModelMembro
    {
        public String Id { get; set; }

        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "Minimo 03 Caracteres !")]
        public String Nome { get; set; }

        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O Email não possui um formato válido !")]
        public String Email { get; set; }

        public List<ModelMembro> ListarTodosMembros()
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametros();
                List<ModelMembro> lista = new List<ModelMembro>();
                ModelMembro item;
                DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, "Select Id, Nome, Email From tbl_Usuarios Order By Nome");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ModelMembro()
                    {
                        Id = dt.Rows[i]["Id"].ToString(),
                        Nome = dt.Rows[i]["Nome"].ToString(),
                        Email = dt.Rows[i]["Email"].ToString()
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

        // Filtrar Membro Pelo Nome
        //=================================================================================================================================

        public List<ModelMembro> ListarTodosMembrosNome(String nome)
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametros();
                List<ModelMembro> lista = new List<ModelMembro>();
                ModelMembro item;
                DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, $"Select Id, Nome, Email From tbl_Usuarios Where Nome = '{nome}'");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ModelMembro()
                    {
                        Id = dt.Rows[i]["Id"].ToString(),
                        Nome = dt.Rows[i]["Nome"].ToString(),
                        Email = dt.Rows[i]["Email"].ToString()
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

        public ModelMembro RetornarMembroId(int? Id)
        {
            try
            {
                DAL objDAL = new DAL();
                objDAL.LimparParametros();
                ModelMembro item;
                DataTable dt = objDAL.ExecutarConsulta(CommandType.Text, $"Select Id, Nome, Email From tbl_Usuarios Where Id = '{Id}'");

                item = new ModelMembro()
                {
                    Id = dt.Rows[0]["Id"].ToString(),
                    Nome = dt.Rows[0]["Nome"].ToString(),
                    Email = dt.Rows[0]["Email"].ToString()
                };
                objDAL.FecharConexao();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GravarMembro()
        {
            try
            {
                if (Id != null)
                {
                    DAL objDAL = new DAL();
                    objDAL.LimparParametros();
                    objDAL.AddParametros("Id", Id);
                    objDAL.AddParametros("Nome", Nome);
                    objDAL.AddParametros("Email", Email);
                    String IdMembro = objDAL.ExecutarManipulacao(CommandType.StoredProcedure, "AlterarMembro").ToString();
                    objDAL.FecharConexao();
                }
                else
                {
                    DAL objDAL = new DAL();
                    objDAL.LimparParametros();
                    objDAL.AddParametros("Nome", Nome);
                    objDAL.AddParametros("Email", Email);
                    String IMembro = objDAL.ExecutarManipulacao(CommandType.Text, "Insert Into tbl_Usuarios(Nome, Email) Values (@Nome, @Email)").ToString();
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
                String IdMembro = objDAL.ExecutarManipulacao(CommandType.StoredProcedure, "ExcluirMembro").ToString();
                objDAL.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
