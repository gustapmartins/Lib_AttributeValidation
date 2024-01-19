namespace Lib_AttributeValidation.Cpf;

public class CpfValidation
{
    protected internal static string TratarCpf(string cpf)
    {
        return cpf.Replace(".", "").Replace("-", "");
    }

    protected internal static bool ValidarPrimeiroDigito(string cpf)
    {

        // Calcular o primeiro dígito verificador
        int soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(cpf[i].ToString()) * (10 - i);
        }

        //Pegando o resto da divisão
        int resto = (soma * 10) % 11;

        int digito1 = resto < 10 ? resto : 0;

        //Verificando se a conta deu o mesmo digito do cpf
        return int.Parse(cpf[9].ToString()) == digito1;
    }

    protected internal static bool ValidarSegundoDigito(string cpf)
    {
        int soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(cpf[i].ToString()) * (11 - i);
        }
        //Pegando o resto da divisão
        int resto = (soma * 10) % 11;

        int digito2 = resto < 10 ? resto : 0;

        //Verificando se a conta deu o mesmo digito do cpf
        return int.Parse(cpf[10].ToString()) == digito2;
    }
}
