using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsole
{
    internal class Program
    {

        static int ExibirMenu()
        {
            int op = 0;
            Console.Clear();
            Console.WriteLine("============================");
            Console.WriteLine("Agenda Modo Console");
            Console.WriteLine("============================");
            Console.WriteLine("Selecione Abaixo:");
            Console.WriteLine("(1) - Exibir Contatos");
            Console.WriteLine("(2) - Inserir Contato");
            Console.WriteLine("(3) - Alterar Contato");
            Console.WriteLine("(4) - Excluir Contato");
            Console.WriteLine("(5) - Localizar Contato");
            Console.WriteLine("(6) - Sair");
            Console.Write("Opção: ");
            op = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            return op;

        }

        static void ExibirContatos(string[] nome, string[] email, int tl)
        {

            //Acredito que precise ajustar para listar corretamente uma lista de array!

            if (tl > 0)
            {

                for (int i = 0; i < tl; i++)
                {
                    Console.WriteLine("Nome: {0} - E-Mail: {1} ", nome[i], email[i]);
                }
            }
            else
            {
                Console.WriteLine("Não existe contato para exibir");
            }


        }

        static void InserirContato(ref string[] nome, ref string[] email, ref int tl)
        {
            /* o REF utilizado antes do string, informa que irá alterar a variável de entrada do parâmetro
             Ou seja, se a variável fora dessa função entrar quando for alterado aqui na função vai alterar lá fora!
           */

            try
            {
                if (tl >= 200)
                {
                    Console.WriteLine("Limite de dados de 200 atigindo!!!");
                }
                else
                {
                    Console.Write("Nome: ");
                    nome[tl] = Console.ReadLine();
                    Console.Write("E-mail: ");
                    email[tl] = Console.ReadLine();
                    int pos = LocalizarContato(email, tl, email[tl]);
                    if (pos == -1)
                    {
                        tl++;

                    }
                    else
                    {
                        Console.WriteLine("Usuário já cadastrado");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.ReadKey();
            }


        }

        static int LocalizarContato(string[] email, int tl, string emailContato)
        {
            int pos = -1;
            int i = 0;
            while (i < tl && email[i] != emailContato)
            {
                i++;
            }
            if (i < tl) pos = i;
            return pos;
        }

        static void AlterarContato(ref string[] nome, ref string[] email, ref int tl)
        {
            try
            {
                Console.WriteLine("Selecione pelo E-Mail o Contato o que será modificado");
                Console.WriteLine("");
                Console.Write("Digite o E-mail: ");
                string emailContato = Console.ReadLine();
                int pos = LocalizarContato(email, tl, emailContato);

                if (pos != -1)
                {
                    Console.WriteLine("Alterar dados do Contato");
                    Console.Write("Nome: ");
                    string novoNome = Console.ReadLine();
                    Console.Write("E-mail: ");
                    string novoEmail = Console.ReadLine();

                    if (LocalizarContato(email, tl, novoEmail) == -1)
                    {
                        nome[pos] = novoNome;
                        email[pos] = novoEmail;
                        Console.WriteLine("Email Alterado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Já existe um contato com esse e-mail");
                    }



                }
                else
                {
                    Console.WriteLine("Contato '{0}' não encontrado", emailContato);

                }



                //Console.Write("Nome: ");
                //string nomeContato = Console.ReadLine();
                //Console.Write("E-mail: ");
                //string emailContato = Console.ReadLine();
                //int pos = LocalizarContato(email, tl, emailContato);
                //if (pos != -1)
                //{
                //    nome[pos] = nomeContato;

                //}
                //else
                //{
                //    Console.WriteLine("Contato não encontradok");
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);

            }

        }

        static bool ExcluirContato(ref string[] nome, ref string[] email, ref int tl, string emailContato)
        {
            bool deleted = false;
            int pos = -1;
            pos = LocalizarContato(email, tl, emailContato);
            if (pos != -1)
            {
                for (int i = pos; i < tl - 1; i++)
                {
                    nome[i] = nome[i + 1];
                    email[i] = email[i + 1];
                }
                deleted = true;
                tl--;
            }
            return deleted;
        }

        static void Main(string[] args)
        {
            // Dados da agenda(Temporário);
            string[] nome = new string[200];
            string[] email = new string[200];
            string emailContato = "";

            //tamanho lógico
            int tl = 0;
            int op = 0;
            int pos = 0;

            //Carregar dados do arquivo Texto
            BackupAgenda.nomeArquivo = "dados.txt";
            BackupAgenda.RestaurarDados(ref nome, ref email, ref tl);

            while (op != 6)
            {
                op = ExibirMenu();

                switch (op)
                {
                    case 1:
                        // Exibir os dados
                        Console.WriteLine("============================");
                        Console.WriteLine("Exibir Contatos");
                        Console.WriteLine("============================");
                        ExibirContatos(nome, email, tl);
                        Console.ReadKey();
                        break;
                    case 2:
                        // Inserir um contato
                        Console.WriteLine("============================");
                        Console.WriteLine("Inserir Contato");
                        Console.WriteLine("============================");
                        InserirContato(ref nome, ref email, ref tl);
                        break;
                    case 3:
                        // Alterar um contato
                        Console.WriteLine("============================");
                        Console.WriteLine("Alterar Contato");
                        Console.WriteLine("============================");
                        Console.WriteLine("");
                        Console.WriteLine("Lista de Contatos");
                        Console.WriteLine("============================");
                        Console.WriteLine("");
                        ExibirContatos(nome, email, tl);
                        Console.WriteLine("");
                        Console.WriteLine("============================");
                        AlterarContato(ref nome, ref email, ref tl);
                        Console.ReadKey();
                        break;
                    case 4:
                        // Excluir um contato
                        Console.WriteLine("============================");
                        Console.WriteLine("Excluir Contato");
                        Console.WriteLine("============================");
                        Console.WriteLine("");
                        Console.WriteLine("Lista de Contatos");
                        Console.WriteLine("============================");
                        Console.WriteLine("");
                        ExibirContatos(nome, email, tl);
                        Console.WriteLine("");
                        Console.WriteLine("============================");
                        Console.Write("E-mail: ");
                        emailContato = Console.ReadLine();
                        if (ExcluirContato(ref nome, ref email, ref tl, emailContato))
                        {
                            Console.WriteLine("Registro contado Excluido");
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        // Localizar um contato
                        Console.WriteLine("============================");
                        Console.WriteLine("Localizar Contato");
                        Console.WriteLine("============================");

                        Console.Write("E-mail: ");
                        emailContato = Console.ReadLine();
                        pos = LocalizarContato(email, tl, emailContato);
                        if (pos != -1)
                        {
                            Console.WriteLine("============================");
                            Console.WriteLine("E-Mail Encontrado: ");
                            Console.WriteLine("Nome: {0} - E-Mail: {1} ", nome[pos], email[pos]);
                        }
                        else
                        {
                            Console.WriteLine("E-Mail não encontado");
                        }
                        Console.ReadKey();

                        break;
                }

            }
            //Salvar dados no arquivo texto
            BackupAgenda.SalvarDados(ref nome, ref email, ref tl);
        }
    }
}
