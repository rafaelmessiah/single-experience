using SingleExperience.Entities.BD;
using SingleExperience.Services.Cliente.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SingleExperience.Services.Cliente
{
    public class ClienteService
    {
        ClienteBD clienteBd = new ClienteBD();

        public bool Cadastrar(CadastroClienteModel model)
        {
            try
            {
                clienteBd.Buscar().ForEach(a =>
                {
                    if (a.Cpf == model.Cpf)
                    {
                        throw new Exception("Esse Cpf Já esta cadastrado");
                    }

                    if (a.Email == model.Email)
                    {
                        throw new Exception("Esse Email Já esta cadastrado");
                    }
                });

                clienteBd.Cadastrar(model);
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um Erro:");
                Console.WriteLine(e.Message);
            }

            return true;
        }

       
       
    }


}
