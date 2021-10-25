using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Contracts.Commom
{
    public class ResponseModel<T>
    {
        public T Data { get; private set; }
        public List<string> ValidationMessages { get; private set; } = new List<string>();
        public bool IsValid => !ValidationMessages.Any();

        public void AtribuirSucesso(T data) => Data = data;
        public void AdicionarErro(string mensagem) => ValidationMessages.Add(mensagem);
        
        public static ResponseModel<T> Sucesso(T data)
        {
            var response = new ResponseModel<T>();
            response.AtribuirSucesso(data);
            return response;
        }

        public static ResponseModel<T> Erro(ValidationResult validator)
        {
            var response = new ResponseModel<T>();
            if (!validator.IsValid)
            {
                foreach (ValidationFailure failure in validator.Errors)
                {
                    response.AdicionarErro(failure.ErrorMessage);
                }
            }

            return response;
        }
    }
}
