using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace Teste;

public class Program
{
    static int menuDisplay()
    {
        Console.WriteLine("\n==== Menu do banco ====");
        Console.WriteLine("1 - Criar conta");
        Console.WriteLine("2 - entrar na conta");
        Console.WriteLine("3 - Verificar contas");
        Console.WriteLine("4 - Sair");
        Console.Write("\nDigite uma opção (1 a 4) : ");
        int choice = Convert.ToInt32(Console.ReadLine());
        return choice;
    }

    static List<Banco> listaContas = new List<Banco>();
    static void menuOpcao(int choice)
    {
        switch (choice)
        {
            case 1:

                Console.WriteLine("\n@@@@@@@@ Criar Conta @@@@@@@@");

                Console.Write("\nDigite seu Nome : ");
                string nome = Console.ReadLine() ?? string.Empty;

                Console.Write("\nDigite o numero da conta : ");
                int numConta = Convert.ToInt32(Console.ReadLine());

                Console.Write("\nDigite a senha da conta : ");
                int senha = Convert.ToInt32(Console.ReadLine());

                Console.Write("\nDigite o saldo inicial da conta : ");
                double saldo = Convert.ToDouble(Console.ReadLine());

                Console.Write("\nConta criada com sucesso!");
                listaContas.Add(new Banco(nome, numConta, senha, saldo));


                Console.WriteLine("\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                break;

            case 2:
                Console.Write("\nDigite o numero da conta que deseja entrar : ");
                int verificarNumero = Convert.ToInt32(Console.ReadLine());

                bool Acesso = false;

                int posicaoVetor = -1;

                for (int i = 0; i < listaContas.Count; i++)
                {
                    if (listaContas[i].NumConta == verificarNumero)
                    {


                        posicaoVetor = i;

                        for (int j = 1; j <= 3; j++)
                        {

                                Console.Write($"\nDigite a senha da conta {listaContas[i].NumConta} : ");
                                int verificarSenha = Convert.ToInt32(Console.ReadLine());

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
                        Console.Write("\nDigite uma opção (1 a 4): ");

                        choiceConta = Convert.ToInt32(Console.ReadLine());

                        switch (choiceConta)
                        {
                            case 1:
                                Console.Write("\nDigite o valor que deseja Depositar : ");
                                double depositarValor = Convert.ToDouble(Console.ReadLine());

                                if (depositarValor <= 0)
                                {
                                    Console.WriteLine("\nNão é possivel depositar esse valor...");
                                }
                                else
                                {
                                    listaContas[posicaoVetor].Saldo += depositarValor;
                                    Console.WriteLine($"Saldo da sua conta é R${listaContas[posicaoVetor].Saldo}");
                                }
                                break;
                        }
                    }
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