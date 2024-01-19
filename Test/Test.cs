using Lib_AttributeValidation;

namespace Test
{
    public class Test
    {
        [Fact]
        public void TestEmail()
        {
            //Arrange 
            string emailValido = "teste@email.com";
            string emailInvalido = "email.invalido";

            // Act & Assert
            Assert.True(Validation.ValidarEmail(emailValido));
            Assert.False(Validation.ValidarEmail(emailInvalido));
        }

        [Fact]
        public void TestPhone()
        {
            //Arrange 
            string emailValido = "1191875-8881";
            string emailInvalido = "918758881";

            // Act & Assert
            Assert.True(Validation.ValidarTelefone(emailValido));
            Assert.False(Validation.ValidarTelefone(emailInvalido));
        }

        [Theory]
        [InlineData("123.456.789-09", true)]  // Substitua com CPFs válidos ou inválidos
        [InlineData("987.655.321-00", false)]
        public void TestValidarCpf(string cpf, bool resultadoEsperado)
        {
            // Act
            bool result = Validation.ValidarCpf(cpf);

            // Assert
            Assert.Equal(resultadoEsperado, result);
        }

        [Theory]
        [InlineData("", 5, false)] //Validar a senha vazia
        [InlineData("gustavo1#", 5, false)] //Validar sem uma letra maiuscula
        [InlineData("GUSTAVO1#", 5, false)] //Validar sem uma letra minuscula
        [InlineData("Gustavo#", 5, false)] //Validar sem o número
        [InlineData("Gustavo1", 5, false)] //Validar sem o caracter especial
        [InlineData("Gustavo1#", 5, true)] //Validar a senha completa
        public void ValidatePassword(string password, int numeroDeCaracteres, bool resultadoEsperado)
        {
            bool result = Validation.ValidatePassword(password, numeroDeCaracteres);

            // Assert
            Assert.Equal(resultadoEsperado, result);
        }

        [Theory]
        [InlineData("01001001", true)] //Valida se o cep e verdadeiro
        [InlineData("01001555", false)]
        public async Task ValidaCep(string cep, bool resultadoEsperado)
        {
            bool result = await Validation.ValidaCep(cep);
            
            Assert.Equal(resultadoEsperado, result);
        }

        [Theory]
        [InlineData("15/05/2083", true)] //Validando a data limite que se pode registrar
        [InlineData("15/05/2023", true)] //Validando a data limite que se pode registrar
        [InlineData("12/09/1800", false)] // Validando uma data menor que 1900
        public void ValidaData(string data, bool resultadoEsperado)
        {
            bool result = Validation.ValidarData(data);

            Assert.Equal(resultadoEsperado, result);
        }

        [Theory]
        [InlineData("05.982.145/0001-59", true)]  // Substitua com CNPJs válidos ou inválidos
        [InlineData("98.765.432/0001-21", false)]
        public void TestValidarCnpj(string cnpj, bool resultadoEsperado)
        {
            // Act
            bool result = Validation.ValidarCnpj(cnpj);

            // Assert
            Assert.Equal(resultadoEsperado, result);
        }

        [Theory]
        [InlineData("http://www.example.com", true)]
        [InlineData("https://subdomain.example.com/path?query=123", true)]
        [InlineData("invalid-url", false)]
        [InlineData("ftp://ftp.example.com", false)]
        public void TestarValidarURL(string url, bool resultadoEsperado)
        {

            // Act (Ação)
            bool resultado = Validation.ValidarURL(url);

            // Assert (Afirmação)
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData("4743321660797238", true)]  // Validar cartão de credito
        [InlineData("4743321660797555", false)]
        [InlineData("4743321660", false)] // Validar o cartao menor que 13 digitos
        [InlineData("47433216607975558", false)] // Validar o cartao maior que 16 digitos
        public void TestValidaCartaoRegraDeLuhn(string cartao, bool resultadoEsperado)
        {
            bool result = Validation.ValidaCartao(cartao);

            Assert.Equal(resultadoEsperado, result);
        }
    }
}