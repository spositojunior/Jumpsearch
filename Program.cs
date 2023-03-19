using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
Inicio:
Console.Title = "Algoritmo JUMPSEARCH";
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Algoritmo desenvolvido por Sposito\n");
Console.Write("Utilizando aplicação console ");
Console.ForegroundColor = ConsoleColor.Magenta;
Console.Write(".NET core 6.0\n\n");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("          *Algoritmo JUMPSEARCH*");
Console.ResetColor();

int lin1 = 1;
int linj = 0;
int lx = 0;
int ly = 9;
int js;
int temp = 0;
bool maisq10 = false;
bool onetap = false;
bool msn = true;
List<int> list = new();
List<int> pRED = new();
List<int> pBLU = new();

CancellationTokenSource source = new CancellationTokenSource();
TimeSpan ts = TimeSpan.FromMilliseconds(3000);

string check;
bool isInList;
bool auto_manu = true;

//E1 - Escolher o modo

Console.SetCursorPosition(0, 6);
Console.WriteLine("Digite 1 para o modo Manual / 2 para o modo Automático / 3 para SAIR.");
RunReadNumber();
Console.Beep();
Console.SetCursorPosition(0, 6);
ClearCurrentConsoleLine();

//E2 - Digitare verificar

do
{
    Console.SetCursorPosition(0, 5);
    if (auto_manu == true)
    {
        Console.Write("\nDigite um valor de " + (lin1 >= 10 ? 0 : 1) + " a 999 para o " + lin1 + "º número.");
        if (lin1 >= 10)
        {
            maisq10 = true;
            Console.Write(" Digite 0 para finalizar.\n");
        }
        else
        {
            Console.Write(" Pelo menos 10 números.\n");
        }
        check = RunReadKey(maisq10);
    }
    else
    {
        Random rand = new();
        Console.WriteLine("\nDigite a quantidade de vetores para serem gerados, de 10 a 200.");
        do
        {
            if (onetap == false)
            {
                temp = Convert.ToInt32(RunReadKey(false));
            }
            if (temp < 10 || temp > 200)
            {
                ClearCurrentConsoleLine();
                Message("Digite um numero de 10 a 200.");
                Console.SetCursorPosition(0, 7);
            }

        } while (temp < 10 || temp > 200);//erro infliezmente
        onetap = true;
        check = (lin1 <= temp - 1 ? rand.Next(1, 999).ToString() : 0.ToString());//remover aqui randomicar
    }

    ClearCurrentConsoleLine();

    isInList = list.IndexOf(int.Parse(check)) != -1;
    if (isInList == false)
    {
        if (linj >= 20)
        {
            linj = 0;
            lx = 0;
            ly += 3;
        }
        lin1++;
        linj++;
        list.Add(int.Parse(check));
        Print(check, lx, ly, true);
        lx += 4;

    }
    else if (auto_manu == true)
    {
        Message("O número " + check + " já existe!");
    };
    Console.SetCursorPosition(0, 5);


} while (check != "0");

//E3 - Sort, printar

Console.Beep();
list.Sort();
linj = 0;
lx = 1;
ly = 9;
foreach (int x in list)
{
    linj++;
    Print(x.ToString(), lx, ly, false);
    lx += 4;
    if (linj >= 20)
    {
        linj = 0;
        lx = 1;
        ly += 3;
    }
}

//E4 - Digitar Pesquisa

Console.SetCursorPosition(0, 6);
ClearCurrentConsoleLine();
Console.WriteLine("Digite um valor existente no vetor para fazer a pesquisa.");
int chk;
int rv = 0;
int tv = 0;
do
{
    Console.SetCursorPosition(0, 7);
    chk = Convert.ToInt32(RunReadKey(maisq10));
    isInList = list.IndexOf(chk) != -1;
    if (isInList == false)
    {
        ClearCurrentConsoleLine();
        Message("O número " + chk + " digitado, não contém no vetor! Digite um que exista");
    }
} while (isInList == false);
linj = 1;
lx = 0;
ly = 9;
js = JumpSearch(list, list.Count(), chk);
Console.SetCursorPosition(0, 19);
foreach (int x in list)
{
    if (pRED.Contains(list.IndexOf(x)))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Print(x.ToString(), lx, ly, true);
    }
    if (pBLU.Contains(list.IndexOf(x)))
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Print(x.ToString(), lx, ly, true);
    }
    if (list.IndexOf(x) == js)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("fim");
        Print(x.ToString(), lx, ly, true);
    }
    if (linj >= 20)
    {
        linj = 0;
        lx = 0;
        ly += 3;
    }
    else
    {
        lx += 4;
    }

    linj++;
}

//E5 - Resultado
source.Cancel();
Console.SetCursorPosition(0, 9);
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("Resultado: ");
Console.ForegroundColor = ConsoleColor.Red;
Console.Write("██ ");
Console.ResetColor();
Console.Write(rv + " casas avançadas, ");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write("██ ");
Console.ResetColor();
Console.Write("percorridos, ");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("██ ");
Console.ResetColor();
Console.Write("achado, ");
Console.Write("██ ");
Console.ResetColor();
Console.Write(tv + " pulados.");
Console.ReadKey();
Console.Clear();
goto Inicio;

///
/// Fim
///
int JumpSearch(/*Lista[]*/List<int> list,/*list length = */int l,/*valor a procurar = */ int v)
{
    int t = 0;
    int r4 = (int)Math.Sqrt(l);
    tv = list.IndexOf(v) + 1;

    while (list[Math.Min(r4, l) - 1] < v)
    {
        t = r4;
        pRED.Add(r4);
        rv++;
        r4 += (int)Math.Sqrt(l);
        tv--;
        if (t >= l) return -1;
    }
    while (list[t] < v)
    {
        t++;
        if (!pRED.Contains(t))
        {
            pBLU.Add(t);
            tv--;
        }
        if (t == Math.Min(r4, l))
            return -1;
        if (list[t] == v)
        {
            return t;
        }
    }
    return t;
}//Method jumpsearch
void RunReadNumber()
{
    ConsoleKeyInfo keyinfo;
    int vai = 0;
    do
    {

        keyinfo = Console.ReadKey(true);
        if (keyinfo.Key == ConsoleKey.D1 || keyinfo.Key == ConsoleKey.NumPad1)
        {
            auto_manu = true;
            vai = 1;
        }
        else if (keyinfo.Key == ConsoleKey.D2 || keyinfo.Key == ConsoleKey.NumPad2)
        {
            auto_manu = false;
            vai = 1;
        }
        else if (keyinfo.Key == ConsoleKey.D3 || keyinfo.Key == ConsoleKey.NumPad3)
        {
            Environment.Exit(0);
        }
    } while (vai == 0);
}//E1 digitado = verificar tipo de escolha
static string RunReadKey(bool maisq10)
{
    ConsoleKeyInfo keyinfo;
    string digitos = "";
    do
    {
        keyinfo = Console.ReadKey(true);

        if (digitos.Length < 3 && keyinfo.Key != ConsoleKey.Backspace && Char.IsNumber(keyinfo.KeyChar) &&
           (digitos.Length != 1 || digitos != "0") && (digitos.Length != 0 || keyinfo.Key != ConsoleKey.D0 || maisq10 != false))
        //&& (digitos.Length != 0 || (keyinfo.Key == ConsoleKey.D2) || dozentos != true) &&
        //(digitos.Length != 2 || digitos != "20" || (keyinfo.Key == ConsoleKey.D0) || dozentos != true))
        {
            Console.Write(keyinfo.KeyChar);
            digitos += keyinfo.KeyChar;
        }

        if (keyinfo.Key == ConsoleKey.Backspace && digitos.Length != 0)
        {
            digitos = digitos.Remove(digitos.Length - 1);
            Console.Write("\b \b");
        }
    }
    while (keyinfo.Key != ConsoleKey.Enter && keyinfo.Key != ConsoleKey.O || digitos.Length == 0);
    return digitos;
}//E2 digitado = evitar e verificar digitos
static void Print(string r, int lx, int ly, bool? b)
{

    string ceta = new('═', 3);
    string? cs = b == true ? "║" : null;
    for (int x = 1; x <= 3; x++)
    {
        Console.SetCursorPosition(lx, b == true ? ly + x : ly + 2);
        if (x == 1 && b == true) Console.Write((lx == 0 ? "╔" : "╦") + ceta + "╗");
        if (x == 2) Console.Write(cs + (r.Length == 1 ? "  " + r : (r.Length == 2 ? " " + r : r)) + cs);
        if (x == 3 && b == true) Console.Write((lx == 0 ? "╚" : "╩") + ceta + "╝");
    }
}//E3 Printar os vetores
static void ClearCurrentConsoleLine()
{
    int currentLineCursor = Console.CursorTop;
    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write(new string(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, currentLineCursor);
}//Limpa a linha atual
void Message(string msg)
{
    Console.Beep();
    Console.SetCursorPosition(6, 9);
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(msg);
    Console.ResetColor();

    if (msn == true)
    {
        msn = false;
        var t = Task.Run(async delegate
        {
            await Task.Delay(ts, source.Token);


            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.SetCursorPosition(6, 9);
            ClearCurrentConsoleLine();
            Console.SetCursorPosition(x, y);
            msn = true;

        });
    }


}//Envia mensagem de alerta em amarelo


async void Disappear()
{
    //tokenSource = new CancellationTokenSource();
    //var token = tokenSource.Token;



    await Task.Delay(3000, source.Token);
    int x = Console.CursorLeft;
    int y = Console.CursorTop;
    Console.SetCursorPosition(6, 9);
    ClearCurrentConsoleLine();
    Console.SetCursorPosition(x, y);
}//  Apaga a mensagem de alerta em amarelo