using GeFinbeta.Repositories;
using GeFinbeta.UI;

Console.WriteLine("Bem-vindo ao GeFinbeta - Gerenciador de Finanças Pessoais!");

GastosRepository repositorio = new GastosRepository();
MenuEscolha menu = new MenuEscolha(repositorio);
menu.Iniciar();