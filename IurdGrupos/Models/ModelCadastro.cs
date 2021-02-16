using IurdGrupos.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IurdGrupos.Models
{
    public class ModelCadastro
    {
        public String Id { get; set; }

        [Required(ErrorMessage = "CampoObrigatório !")]
        [Display(Name = "Nome")]
        [MaxLength(50, ErrorMessage = "Entre 03 e 50 Caracteres !")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "CampoObrigatório !")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O Email não possui um formato válido !")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [MinLength(4, ErrorMessage = "Minimo 04 Caracteres !")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public String Senha { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [MinLength(4, ErrorMessage = "Minimo 04 Caracteres !")]
        [Display(Name = "Confirme à Senha")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage ="A Senha não confere !")]
        public String ConfSenha { get; set; }

        public void InserirUsuario()
        {
            try
            {
                if(Id==null)
                {
                    DAL objDAL = new DAL();
                    objDAL.LimparParametros();
                    objDAL.AddParametros("Nome", Nome);
                    objDAL.AddParametros("Email", Email);
                    objDAL.AddParametros("Senha", Senha);
                    String sql = objDAL.ExecutarManipulacao(CommandType.Text, "Insert Into Login (Nome, Email, Senha) Values (@Nome, @Email, @Senha)").ToString();
                    DataTable dt = objDAL.RetDatatable(sql);
                    objDAL.FecharConexao();
                }
            }
            catch
            {
                //
            }
        }
    }
}
