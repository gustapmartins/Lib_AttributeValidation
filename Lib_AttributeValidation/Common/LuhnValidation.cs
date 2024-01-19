namespace Lib_AttributeValidation.Common;

public class LuhnValidation
{
    protected internal static bool ValidarLuhn(string numeros)
    {
        int[] numero = numeros.Select(c => int.Parse(c.ToString())).ToArray();

        SomaDosDigitosAlternados(numero);

        SomaDosDigitosRemanescentes(numero);

        return numero.Sum() % 10 == 0;    
    }

    protected internal static void SomaDosDigitosAlternados(int[] numero)
    {
        for (int i = numero.Length - 2; i >= 0; i -= 2)
        {
            //Começo da direita para a esquerda a multiplicação
            int dobro = numero[i] * 2;
            // Se o dobro for maior que 9, subtrai 9
            numero[i] = (dobro > 9) ? dobro - 9 : dobro;
        }
    }

    protected internal static void SomaDosDigitosRemanescentes(int[] numero)
    {
        for (int i = numero.Length - 1; i >= 0; i -= 2)
        {
            //Começo da direita para a esquerda a multiplicação
            numero[i] = numero[i];
        }
    }
}