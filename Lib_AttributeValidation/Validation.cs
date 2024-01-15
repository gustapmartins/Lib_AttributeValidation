using Lib_AttributeValidation.Common;
using Lib_AttributeValidation.Cpf;
using System.Globalization;
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


    /// <summary>
    /// Validar a senha
    /// </summary>
    /// <param name="senha"></param>
    /// <param name="numeroDeCaracteres"></param>
    /// <returns>Retorna um valor booleano, se a senha for valida ele trás true se não false</returns>
    public static bool ValidatePassword(string senha, int numeroDeCaracteres)
    {
        // Verificar comprimento mínimo
        if (senha.Length == 0 || senha.Length < numeroDeCaracteres) return false;

        // Verificar se contém pelo menos uma letra maiúscula
        if (!senha.Any(char.IsUpper)) return false;

        // Verificar se contém pelo menos uma letra minúscula
        if (!senha.Any(char.IsLower)) return false;

        // Verificar se contém pelo menos um número
        if (!senha.Any(char.IsDigit)) return false;

        // Verificar se contém pelo menos um caractere especial
        if (!senha.Any(ch => !char.IsLetterOrDigit(ch))) return false;

        return true;
    }

    /// <summary>
    /// Valida o codigo postal
    /// </summary>
    /// <param name="cep"></param>
    /// <returns>Retorna um valor booleano, se o cep for valido ele trás true se não false</returns>
    public static async Task<bool> ValidaCep(string cep)
    {
        string apiUrl = $"https://viacep.com.br/ws/{cep}/json/";

        HttpClient httpClient = new();

        var response = await httpClient.GetAsync(apiUrl);

        if(response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
            // Verifica se a resposta contém um indicativo de CEP não encontrado
            if (!responseData.Contains("erro\": true"))
            {
                // Converte a resposta para um objeto EnderecoViaCep
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Valida o codigo postal
    /// </summary>
    /// <param name="url"></param>
    /// <returns>Retorna um valor booleano, se o cep for valido ele trás true se não false</returns>
    public static bool ValidarURL(string url)
    {
        url = url.Trim();

        Regex pattern = new Regex("@\"^(?:http(s)?:\\/\\/)?[\\w.-]+(?:\\.[\\w\\.-]+)+[\\w\\-\\._~:/?#[\\]@!\\$&'\\(\\)\\*\\+,;=.]+$\" ");
        Match match = pattern.Match(url);
        return match.Success;
    }

    /// <summary>
    /// Valida a data 
    /// </summary>
    /// <param name="data"></param>
    /// <returns>Retorna um valor booleano, se a data for valida, ele trás true se não false</returns>
    public static bool ValidarData(string data)
    {
        DateTime dataConvertida;

        var resultadoData = DateTime.TryParseExact(data, "dd/MM/yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None,out dataConvertida);

        if(resultadoData)
        {
            if(dataConvertida.Year >= 1900 && dataConvertida <= DateTime.Today.AddYears(60))
            {
                return true;
            }else
            {
                return false;
            }
        }

        return resultadoData;
    }

    /// <summary>
    /// Valida o cnpj
    /// </summary>
    /// <param name="cnpj"></param>
    /// <returns>Retorna um valor booleano, se o cep for valido ele trás true se não false</returns>
    public static bool ValidarCnpj(string cnpj)
    {
        // Remover caracteres não numéricos
        var digitosCNPJ = new string(cnpj.Where(char.IsDigit).ToArray());
  
        // Verificar se todos os dígitos são iguais (caso contrário, o CNPJ é inválido)
        if (digitosCNPJ.Distinct().Count() == 1)
        {
            return false;
        }

        // Verificar se o CNPJ tem 14 dígitos
        if (digitosCNPJ.Length != 14)
        {
            return false;
        }

        // Calcular os dígitos verificadores
        int[] multiplicadoresPrimeiroDigito = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int primeiroDigitoVerificador = CnpjValidation.CalcularDigitoVerificador(digitosCNPJ, multiplicadoresPrimeiroDigito);

        int[] multiplicadoresSegundoDigito = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int segundoDigitoVerificador = CnpjValidation.CalcularDigitoVerificador(digitosCNPJ + primeiroDigitoVerificador, multiplicadoresSegundoDigito);

        // Verificar se os dígitos verificadores calculados coincidem com os fornecidos
        return digitosCNPJ.EndsWith($"{primeiroDigitoVerificador}{segundoDigitoVerificador}");
    }
}

