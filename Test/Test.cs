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
    }
}