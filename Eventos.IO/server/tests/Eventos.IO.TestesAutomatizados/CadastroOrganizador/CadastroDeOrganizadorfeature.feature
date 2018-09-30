Funcionalidade: Cadastro de Organizadorpassword
	Um organizador fará seu cadastro pelo site
	para poder gerenciar seus próprios eventos
	Ao terminar o cadastro receberá uma notificação
	de sucesso ou de falha.

@TesteAutomatizadoCadastroDeOrganizadorComSucesso

Cenário: Cadastro de Organizador com Sucesso
	Dado que o Organizador está no site, na página inicial
	E clica no link de registro
	E preenche os campos com os valores
		| Campo           | Valor           |
		| nome            | Marcus Almeida  |
		| cpf             | 30390600822     |
		| email           | teste@teste.com |
		| password        | Teste@123       |
		| confirmPassword | Teste@123       |
	Quando clicar no botao registrar
	Então Será registrado e redirecionado com sucesso
	
@TesteAutomatizadoCadastroOrganizadorFalha

Cenário: Cadastro de Organizador com CPF já utilizado
	Dado que o Organizador está no site, na página inicial
	E clica no link de registro
	E preenche os campos com os valores
		| Campo           | Valor                         |
		| nome            | Marcus Almeida                |
		| cpf             | 30390600822                   |
		| email           | contato12@eduardopires.net.br |
		| password        | Teste@123                     |
		| confirmPassword | Teste@123                     |
	Quando clicar no botao registrar
	Entao Recebe uma mensagem de erro de CPF já cadastrado

@TesteAutomatizadoCadastroOrganizadorFalha

Cenário: Cadastro de Organizador com Email já utilizado
	Dado que o Organizador está no site, na página inicial
	E clica no link de registro
	E preenche os campos com os valores
		| Campo           | Valor           |
		| nome            | Marcus Almeida  |
		| cpf             | 30390600821     |
		| email           | teste@teste.com |
		| password        | Teste@123       |
		| confirmPassword | Teste@123       |
	Quando clicar no botao registrar
	Entao recebe uma mensagem de erro de email já cadastrado

@TesteAutomatizadoCadastroOrganizadorFalha

Cenário: Cadastro de Organizador com password sem números
	Dado que o Organizador está no site, na página inicial
	E clica no link de registro
	E preenche os campos com os valores
		| Campo           | Valor           |
		| nome            | Marcus Almeida  |
		| cpf             | 30390600822     |
		| email           | teste@teste.com |
		| password        | Teste@          |
		| confirmPassword | Teste@          |
	Quando clicar no botao registrar
	Entao Recebe uma mensagem de erro de que a password requer numero

@TesteAutomatizadoCadastroOrganizadorFalha

Cenário: Cadastro de Organizador com password sem Maiuscula
	Dado que o Organizador está no site, na página inicial
	E clica no link de registro
	E preenche os campos com os valores
		| Campo           | Valor           |
		| nome            | Marcus Almeida  |
		| cpf             | 30390600822     |
		| email           | teste@teste.com |
		| password        | teste@123       |
		| confirmPassword | teste@123       |
	Quando clicar no botao registrar
	Entao Recebe uma mensagem de erro de que a password requer letra maiuscula

@TesteAutomatizadoCadastroOrganizadorFalha

Cenário: Cadastro de Organizador com password sem minuscula
	Dado que o Organizador está no site, na página inicial
	E clica no link de registro
	E preenche os campos com os valores
		| Campo           | Valor           |
		| nome            | Marcus Almeida  |
		| cpf             | 30390600822     |
		| email           | teste@teste.com |
		| password        | TESTE@123       |
		| confirmPassword | TESTE@123       |
	Quando clicar no botao registrar		
	Entao Recebe uma mensagem de erro de que a password requer letra minuscula

@TesteAutomatizadoCadastroOrganizadorFalha

Cenário: Cadastro de Organizador com password sem caracter especial
	Dado que o Organizador está no site, na página inicial
	E clica no link de registro
	E preenche os campos com os valores
		| Campo           | Valor           |
		| nome            | Marcus Almeida  |
		| cpf             | 30390600822     |
		| email           | teste@teste.com |
		| password        | Teste123        |
		| confirmPassword | Teste123        |
	Quando clicar no botao registrar		
	Entao Recebe uma mensagem de erro de que a password requer caracter especial

@TesteAutomatizadoCadastroOrganizadorFalha
		
Cenário: Cadastro de Organizador com password em tamanho inferior ao permitido
	Dado que o Organizador está no site, na página inicial
	E clica no link de registro
	E preenche os campos com os valores
		| Campo           | Valor           |
		| nome            | Marcus Almeida  |
		| cpf             | 30390600822     |
		| email           | teste@teste.com |
		| password        | Te123           |
		| confirmPassword | Te123           |
	Quando clicar no botao registrar
	Entao Recebe uma mensagem de erro de que a password esta em tamanho inferior ao permitido

@TesteAutomatizadoCadastroOrganizadorFalha

Cenário: Cadastro de Organizador com password diferentes
	Dado que o Organizador está no site, na página inicial
	E clica no link de registro
	E preenche os campos com os valores
		| Campo           | Valor           |
		| nome            | Marcus Almeida  |
		| cpf             | 30390600822     |
		| email           | teste@teste.com |
		| password        | Teste@123       |
		| confirmPassword | Teste@124       |
	Quando clicar no botao registrar
	Entao Recebe uma mensagem de erro de que a password estao diferentes