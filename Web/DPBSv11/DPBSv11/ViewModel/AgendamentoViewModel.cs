using System;
using System.ComponentModel.DataAnnotations;

namespace DPBSv11.ViewModel
{
    public class AgendamentoViewModel
    {
        //atributos do model Agendamento
        //public int AgendamentoViewModelId { get; set; }
      
        //[Display(Name = "Data do agendamento")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DATA { get; set; }


        [Display(Name = "Hora do agendamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH/mm}", ApplyFormatInEditMode = true)]
        public DateTime HORA { get; set; }

       
        [Display(Name = "Selecione um funcionário:")]
        public string COD_FUNCIONARIO { get; set; }

        
        [Display(Name = "Escolha um serviço:")]
        public string COD_SERVICO { get; set; }

        public int COD_CLIENTE { get; set; }




        [Display(Name = "Digite seu nome:")]
        public string SITUACAO { get; set; }



        public int COD_AGENDAMENTO { get; set; }

        //atributos do model TB_CLIENTE

        
        [Display(Name = "Digite seu nome:")]
        public string NOME { get; set; }



      
        [Display(Name = "Digite seu CPF:")]
        public string CPF { get; set; }


      
        [Display(Name = "Digite seu e-mail:")]
        public string EMAIL { get; set; }

        public decimal VALOR { get; set; }
        public string SERVICO { get; set; }

        
    }
}