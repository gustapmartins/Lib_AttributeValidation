namespace Lib_AttributeValidation.Common;

public class CnpjValidation
{
    protected internal static int CalcularDigitoVerificador(string baseNumerica, int[] multiplicadores)
    {
        int soma = 0;

        for (int i = 0; i < multiplicadores.Length; i++)
        {
            soma += int.Parse(baseNumerica[i].ToString()) * multiplicadores[i];
        }

        int resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
