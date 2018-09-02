using Eventos.IO.TestesAutomatizados.Config;
using TechTalk.SpecFlow;
using Xunit;

namespace Eventos.IO.TestesAutomatizados.CadastroOrganizador
{
    [Binding]
    public class CadastroDeOrganizadorSteps
    {
        public SeleniumHelper Browser;

        public CadastroDeOrganizadorSteps()
        {
            Browser = SeleniumHelper.Instance();
        }

        // AAA > Arrange, Act, Assert

        // Arrange

        [Given(@"que o Organizador está no site, na página inicial")]
        public void DadoQueOOrganizadorEstaNoSiteNaPaginaInicial()
        {
            var url = Browser.NavegarParaUrl(ConfigurationHelper.SiteUrl);
            Assert.Equal(ConfigurationHelper.SiteUrl, url);
        }
        
        [Given(@"clica no link de registro")]
        public void DadoClicaNoLinkDeRegistro()
        {
            Browser.ClicarLinkPorTexto("Registre-se");
        }
        
        [Given(@"preenche os campos com os valores")]
        public void DadoPreencheOsCamposComOsValores(Table table)
        {
            Browser.PreencherTextBoxPorId(table.Rows[0][0], table.Rows[0][1]);
            Browser.PreencherTextBoxPorId(table.Rows[1][0], table.Rows[1][1]);
            Browser.PreencherTextBoxPorId(table.Rows[2][0], table.Rows[2][1]);
            Browser.PreencherTextBoxPorId(table.Rows[3][0], table.Rows[3][1]);
            Browser.PreencherTextBoxPorId(table.Rows[4][0], table.Rows[4][1]);
        }

        // Act

        [When(@"clicar no botao registrar")]
        public void QuandoClicarNoBotaoRegistrar()
        {
            Browser.ClicarBotaoPorId("Registrar");
        }

        // Assert

        [Then(@"Será registrado e redirecionado com sucesso")]
        public void EntaoSeraRegistradoERedirecionadoComSucesso()
        {
            var returnText = Browser.ObterTextoElementoPorId("saudacaoUsuario");
            Assert.Contains("olá marcus almeida", returnText.ToLower());
            Browser.ObterScreenShot("EvidenciaCadastro");
        }

        // **************************** Cenários de falha  ******************************

        [Then(@"Recebe uma mensagem de erro de CPF já cadastrado")]
        public void EntaoRecebeUmaMensagemDeErroDeCPFJaCadastrado()
        {
            var result = Browser.ObterTextoElementoPorClasse("alert-danger");
            Assert.Contains("cpf ou e-mail já cadastrado", result.ToLower());

            Browser.ObterScreenShot("CPF_Erro");
        }

        [Then(@"recebe uma mensagem de erro de email já cadastrado")]
        public void EntaoRecebeUmaMensagemDeErroDeEmailJaCadastrado()
        {
            var result = Browser.ObterTextoElementoPorClasse("alert-danger");
            Assert.Contains("is already taken", result.ToLower());

            Browser.ObterScreenShot("Email_Erro");
        }

        [Then(@"Recebe uma mensagem de erro de que a password requer numero")]
        public void EntaoRecebeUmaMensagemDeErroDeQueAPasswordRequerNumero()
        {
            var result = Browser.ObterTextoElementoPorClasse("alert-danger");
            Assert.Contains("passwords must have at least one digit ('0'-'9')", result.ToLower());
        }

        [Then(@"Recebe uma mensagem de erro de que a password requer letra maiuscula")]
        public void EntaoRecebeUmaMensagemDeErroDeQueAPasswordRequerLetraMaiuscula()
        {
            var result = Browser.ObterTextoElementoPorClasse("alert-danger");
            Assert.Contains("passwords must have at least one uppercase ('a'-'z')", result.ToLower());
        }

        [Then(@"Recebe uma mensagem de erro de que a password requer letra minuscula")]
        public void EntaoRecebeUmaMensagemDeErroDeQueAPasswordRequerLetraMinuscula()
        {
            var result = Browser.ObterTextoElementoPorClasse("alert-danger");
            Assert.Contains("passwords must have at least one lowercase ('a'-'z')", result.ToLower());
        }

        [Then(@"Recebe uma mensagem de erro de que a password requer caracter especial")]
        public void EntaoRecebeUmaMensagemDeErroDeQueAPasswordRequerCaracterEspecial()
        {
            var result = Browser.ObterTextoElementoPorClasse("alert-danger");
            Assert.Contains("passwords must have at least one non alphanumeric character", result.ToLower());
        }

        [Then(@"Recebe uma mensagem de erro de que a password esta em tamanho inferior ao permitido")]
        public void EntaoRecebeUmaMensagemDeErroDeQueAPasswordEstaEmTamanhoInferiorAoPermitido()
        {
            var result = Browser.ObterTextoElementoPorClasse("text-danger");
            Assert.Contains("a senha deve possuir no mínimo 6 caracteres", result.ToLower());
        }


        [Then(@"Recebe uma mensagem de erro de que a password estao diferentes")]
        public void EntaoRecebeUmaMensagemDeErroDeQueAPasswordEstaoDiferentes()
        {
            var result = Browser.ObterTextoElementoPorClasse("text-danger");
            Assert.Contains("as senhas não conferem", result.ToLower());
        }

    }
}
