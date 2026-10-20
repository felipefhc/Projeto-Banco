using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.Quic;
using System.Xml.Serialization;

namespace Bancofhc;

public class Program
{
    static int menuDisplay()
    {
        Console.WriteLine("\n==== Menu do banco ====");
        Console.WriteLine("1 - Criar conta");
        Console.WriteLine("2 - entrar na conta");
        Console.WriteLine("3 - Verificar contas");
        Console.WriteLine("4 - Sair");
        int choice = LerInteiro("\nDigite uma opção (1 a 4) : ");
        return choice;
    }

    static string LerNome(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string entrada = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(entrada) && entrada.All(c => char.IsLetter(c) || c == ' '))
                return entrada.Trim();

            Console.WriteLine("Nome inválido, use apenas letras.");
        }
    }

    static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);

            if (int.TryParse(Console.ReadLine(), out int valor))
                return valor;

            Console.WriteLine("Valor inválido, digite apenas números inteiros.");
        }
    }

    static double LerDouble(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);

            if (double.TryParse(Console.ReadLine(), out double valor))
                return valor;

            Console.WriteLine("Valor inválido, digite um número.");
        }
    }

    static List<Banco> listaContas = new List<Banco>();
    static void menuOpcao(int choice)
    {
        switch (choice)
        {
            case 1:

                Console.WriteLine("\n@@@@@@@@ Criar Conta @@@@@@@@");

                string nome = LerNome("\nDigite seu nome : ");

                int numConta = LerInteiro("\nDigite o número da conta : ");

                int senha = LerInteiro("\nDigite a senha da conta : ");

                double saldo = LerDouble("\nDigite o saldo inicial da conta : ");

                Console.Write("\nConta criada com sucesso!");
                listaContas.Add(new Banco(nome, numConta, senha, saldo));


                Console.WriteLine("\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                break;

            case 2:
                if (listaContas.Count == 0)
                {
                    Console.WriteLine("\nNão existe conta criada.");
                    break;
                }

                int verificarNumero = LerInteiro("\nDigite o numero da conta que deseja entrar : ");

                bool Acesso = false;

                int posicaoVetor = -1;

                for (int i = 0; i < listaContas.Count; i++)
                {
                    if (listaContas[i].NumConta == verificarNumero)
                    {
                        posicaoVetor = i;

                        for (int j = 1; j <= 3; j++)
                        {

                            int verificarSenha = LerInteiro($"\nDigite a senha da conta | {listaContas[i].NumConta} | : ");

                            if (listaContas[i].Senha == verificarSenha)
                            {
                                Console.WriteLine($"\nAcesso concedido a conta '{listaContas[i].NumConta}', Bem vindo(a) - {listaContas[i].Nome} -");
                                Acesso = true;
                                break;
                            }
                            else if (j == 3)
                            {
                                Console.WriteLine("Conta bloqueada.");
                            }
                            else
                            {
                                Console.WriteLine($"Acesso negado, você tem mais {3 - j} tentativas");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nConta não existe nos registros.");
                    }
                }

                if (Acesso == true)
                {
                    int choiceConta = 0;
                    while (choiceConta != 4)
                    {
                        Console.WriteLine("\n+++ Menu da sua Conta +++");
                        Console.WriteLine("1 - Depositar Valor");
                        Console.WriteLine("2 - Sacar valor");
                        Console.WriteLine("3 - Ver Saldo");
                        Console.WriteLine("4 - Sair da conta");

                        choiceConta = LerInteiro("\nDigite uma opção (1 a 4): ");

                        switch (choiceConta)
                        {
                            case 1:
                                double depositarValor = LerDouble("\nDigite o valor que deseja Depositar : ");

                                if (depositarValor <= 0)
                                {
                                    Console.WriteLine("\nNão é possivel depositar esse valor...");
                                    break;
                                }
                                listaContas[posicaoVetor].Saldo += depositarValor;
                                Console.WriteLine($"Saldo da sua conta é R${listaContas[posicaoVetor].Saldo}");

                                break;

                            case 2:
                                if (listaContas[posicaoVetor].Saldo <= 0)
                                {
                                    Console.WriteLine("\nNão possui saldo.");
                                    break;

                                }

                                double sacarValor = LerDouble("\nDigite o valor que deseja sacar : ");

                                if (sacarValor <= 0 || listaContas[posicaoVetor].Saldo < sacarValor)
                                {
                                    Console.WriteLine("\nNão possui saldo suficiente para sacar esse valor.");
                                    break;
                                }

                                listaContas[posicaoVetor].Saldo -= sacarValor;
                                Console.WriteLine($"\nSaldo da sua conta é R${listaContas[posicaoVetor].Saldo}");
                                break;

                            case 3:
                                Console.WriteLine($"\nSaldo da sua conta é R${listaContas[posicaoVetor].Saldo}");
                                break;

                            case 4:
                                Console.WriteLine("\nSaindo da conta...");
                                break;

                            default:
                                Console.WriteLine("\nopção invalida.");
                                break;
                        }
                    }
                }
                break;

            case 3:
                if (listaContas.Count == 0)
                {
                    Console.WriteLine("\nNenhuma conta criada ainda.");
                    break;
                }

                Console.WriteLine("\n==== Contas cadastradas ====");
                foreach (var conta in listaContas)
                {
                    Console.WriteLine(conta.NumConta);
                }
                break;
        }
    }




    public static void Main()
    {
        int choice = 0;

        while (choice != 4)
        {
            choice = menuDisplay();
            menuOpcao(choice);
        }
    }
}
