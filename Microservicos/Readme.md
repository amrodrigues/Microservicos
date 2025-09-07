Projeto de Microservi�os de Vendas e Estoque
Este � um projeto de arquitetura de microservi�os, composto por dois servi�os principais: Vendas e Estoque. A comunica��o entre eles � feita usando requisi��es HTTP para verifica��o de estoque e um sistema de mensageria RabbitMQ para a notifica��o de atualiza��es.

Tecnologias e Ferramentas
.NET 6.0: Framework para construir as APIs.

Entity Framework Core: ORM (Object-Relational Mapper) para o acesso a banco de dados.

SQL Server: Banco de dados relacional para persistir os dados.

RabbitMQ: Ferramenta de mensageria para comunica��o ass�ncrona entre os servi�os.

Swagger/OpenAPI: Para documenta��o e teste interativo das APIs.

Identity: Para o gerenciamento de usu�rios e autentica��o via JWT.

Estrutura do Projeto
O projeto � organizado em duas pastas de projeto, cada uma com sua pr�pria API e responsabilidades:

Vendas: Servi�o respons�vel por gerenciar os pedidos de venda.

VendasController: Controla o fluxo de cria��o e consulta de pedidos.

AuthController: Gerencia o registro e login de usu�rios, usando autentica��o JWT.

VendasDbContext: Contexto do banco de dados para os pedidos, integrado com Identity para dados de usu�rios.

Estoque: Servi�o respons�vel por gerenciar o estoque de produtos.

ProdutosController: Oferece endpoints para a consulta e atualiza��o do estoque.

EstoqueDbContext: Contexto do banco de dados para os produtos.

Worker: Um servi�o em segundo plano que escuta mensagens do RabbitMQ para atualizar o estoque ap�s um pedido.

Configura��o e Execu��o
Pr�-requisitos
Certifique-se de ter instalado:

.NET 6 SDK

SQL Server

RabbitMQ Server: Em execu��o em localhost.

Ferramenta dotnet-ef: Para migra��es do Entity Framework (dotnet tool install --global dotnet-ef).

Strings de Conex�o
Ambos os projetos possuem strings de conex�o em seus arquivos appsettings.json. Certifique-se de que est�o configuradas para apontar para suas inst�ncias do SQL Server. Os nomes dos bancos de dados s�o VendasIdentityDb e EstoqueDb.

Migra��es de Banco de Dados
Para cada projeto, abra um terminal na respectiva pasta e execute os comandos para criar e aplicar as migra��es:

Para o projeto Vendas:

dotnet ef migrations add InitialVendas --project .\src\VendasService\
dotnet ef database update --project .\src\VendasService\

Para o projeto Estoque:

dotnet ef migrations add InitialEstoque --project .\src\EstoqueService\
dotnet ef database update --project .\src\EstoqueService\

Note: Voc� precisa ter a solu��o aberta no Visual Studio ou usar a flag --startup-project para que o EF encontre o projeto principal.

Executar a Aplica��o
Para executar todos os projetos, use o Visual Studio ou a linha de comando. No terminal, execute dotnet run em cada uma das pastas do projeto (Vendas e Estoque).

Endpoints da API
Vendas: https://localhost:5001/swagger

Estoque: https://localhost:5062/swagger

Use o Swagger UI para testar os endpoints de cada servi�o.