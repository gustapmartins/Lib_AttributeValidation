using Lib_AttributeValidation;

namespace Test
{
    public class UnitTest1
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
        public void TestValidarCpf(string cpf, bool expectedResult)
        {
            // Act
            bool result = Validation.ValidarCpf(cpf);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}