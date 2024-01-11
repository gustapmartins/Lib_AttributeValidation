using Lib_AttributeValidation.Cpf;
using System.Text.RegularExpressions;

namespace Lib_AttributeValidation;

public class Validation
{

    /// <summary>
    /// Classes para validar Email usando Regex
    /// </summary>
    /// <param name="email"></param>
    /// <returns>Retorna um valor booleano, se o email for valido ele trás true se não false</returns>
    public static bool ValidarEmail(string email)
    {
        // Expressão regular para validar e-mail
        string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
        return Regex.IsMatch(email, pattern);
    }

    /// <summary>
    /// Classe para validar Telefone com Regex
    /// </summary>
    /// <param name="telefone"></param>
    /// <returns>Retorna um valor booleano, se o telefone for valido ele trás true se não false</returns>
    public static bool ValidarTelefone(string telefone)
    {
        // Expressão regular para validar números de telefone brasileiros
        string pattern = @"^\(?\d{2}\)?[-.\s]?\d{4,5}[-.\s]?\d{4}$";
        return Regex.IsMatch(telefone, pattern);
    }


    /// <summary>
    /// Classe para validar CPF 
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns>Retorna um valor booleano, se o CPF for valido ele trás true se não false</returns>
    public static bool ValidarCpf(string cpf)
    {
        // Implementação simples para validar CPF
        cpf = CpfValidation.TratarCpf(cpf);

        if (cpf.Length != 11 || !long.TryParse(cpf, out _))
            return false;

        // Calcular o primeiro dígito verificador
        var primeiroDigito = CpfValidation.ValidarPrimeiroDigito(cpf);

        if (primeiroDigito == false)
            return false;

        var segundoDigito = CpfValidation.ValidarSegundoDigito(cpf);

        //calcular o segundo digito verificador
        return segundoDigito;
    }
}
