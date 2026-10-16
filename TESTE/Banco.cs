namespace Teste;

public class Banco
{
    public String Nome {get;set;}
    public int NumConta{get;set;}
    public int Senha{get;set;}
    public double Saldo{get;set;}


    public Banco(string nome, int numConta, int senha, double saldo)
    {
        Nome = nome;
        NumConta = numConta;
        Senha = senha;
        Saldo = saldo;
    }
}