# Imobiliaria NCC

Sistema de gestão imobiliária desenvolvido como MVP para gerenciamento
de **clientes, apartamentos, reservas e vendas**.

O projeto foi desenvolvido utilizando **.NET9.0 + React + SQL Server**, 
com arquitetura baseada em **Clean Architecture**, 
autenticação via **JWT** e ambiente totalmente **containerizado com Docker**.

------------------------------------------------------------------------

# Como rodar o projeto localmente utilizando o Docker

## Pré-requisitos

Antes de iniciar, é necessário ter instalado:

-   Docker
-   Docker Compose

Verifique com:

``` bash
docker --version
docker compose version
```

------------------------------------------------------------------------

## Passo a passo

1 Clone o repositório

``` bash
git clone https://github.com/GabrielLana/ImobiliariaNcc.git
cd imobiliaria-ncc
```

2 Arquivo .env

Em projetos reais, o arquivo .env jamais seria disponibilizado através do github, mas sim um .env.example com os exemplos de chaves utilizados pelo projeto,
mas nesse caso por se tratar de um projeto puramente focado em estudo, disponibilizei já dentro do repositório o arquivo com as configurações necessárias.

3 Suba os containers

``` bash
docker compose up --build
```

4 Acesse a aplicação

Frontend:

    http://localhost

API:

    http://localhost/api

------------------------------------------------------------------------

# Exemplos de requisições para a API

## Login

Endpoint:

    POST /api/login

Exemplo de body:

``` json
{
  "cpf": "12345678900",
  "senha": "12345678"
}
```

Resposta esperada:

``` json
{
  "token": "JWT_TOKEN_AQUI",
  "nome": "Nome do Vendedor"
}
```

------------------------------------------------------------------------

## Criar Cliente

Endpoint:

    POST /api/clientes

Body:

``` json
{
  "nome": "João Silva",
  "cpf": "12345678900",
  "dataNascimento": "1990-01-01",
  "email": "joao@email.com",
  "celular": "31999999999",
  "estadoCivil": "Solteiro"
}
```

------------------------------------------------------------------------

## Listar Apartamentos Disponíveis

Endpoint:

    GET /api/apartamentos/disponivel

Body:

``` json
[
  {
    "reservado": true,
    "id": 3,
    "ocupado": false,
    "metragem": 80,
    "quartos": 3,
    "banheiros": 2,
    "vagas": 2,
    "detalhesApartamento": "Varanda;Suite Master;Armarios Planejados",
    "detalhesCondominio": "Piscina;Sauna;Academia",
    "andar": 3,
    "bloco": 1,
    "valorVenda": 420000,
    "valorCondominio": 500,
    "valorIptu": 150,
    "cep": "33333333",
    "logradouro": "Rua das Flores",
    "bairro": "Centro",
    "numero": "999",
    "estado": "Minas Gerais",
    "cidade": "Belo Horizonte",
    "complemento": null
  }
]
```

------------------------------------------------------------------------

# Estrutura das tabelas no banco de dados

## Tabela: Vendedores
|  Campo            | Tipo      | Descrição |
|  :--- | :---: | ---: |
|  Id               | int       | Identificador do vendedor |
|  DataCriacao      | datetime2 | Data de criação do registro |
|  DataAlteracao    | datetime2 | Data de alteração do registro |
|  Nome             | varchar   | Nome do vendedor |
|  Cpf              | varchar   | CPF do vendedor |
|  DataNascimento   | date      | Data de nascimento |
|  Email            | varchar   | Email |
|  Celular          | varchar   | Telefone |
|  Ativo            | bit       | Indica se o vendedor está ativo |
|  Senha            | varchar   | Hash criado pelo backend para controle de login |
|  Cep              | varchar   | Cep do vendedor |
|  Logradouro       | varchar   | Logradouro do vendedor |
|  Bairro           | varchar   | Bairro do vendedor |
|  Numero           | varchar   | Numero do endereço do vendedor |
|  Complemento      | varchar   | Complemento do endereço, caso haja (nullable) |
|  Setor            | varchar   | Indica qual setor de vendas o vendedor pertence |
|  NumeroRegistro   | varchar   | Numero de registro do funcionário dentro da empresa |

------------------------------------------------------------------------

## Tabela: Clientes

|  Campo            | Tipo      | Descrição |
|  :--- | :---: | ---: |
|  Id               | int       | Identificador do cliente |
|  Nome             | varchar   | Nome do cliente |
|  Cpf              | varchar   | CPF do cliente |
|  DataNascimento   | date      | Data de nascimento |
|  Email            | varchar   | Email |
|  Celular          | varchar   | Telefone |
|  EstadoCivil      | varchar   | Estado civil |
|  Ativo            | bit       | Indica se o cliente está ativo |

------------------------------------------------------------------------

## Tabela: Apartamentos

|  Campo             | Tipo |
|  :--- | :---: |
|  Id                | int |
|  Metragem          | int |
|  Quartos           | int |
|  Banheiros         | int |
|  Vagas             | int |
|  Andar             | int |
|  Bloco             | int |
|  ValorVenda        | decimal |
|  ValorCondominio   | decimal |
|  ValorIptu         | decimal |
|  Cep               | varchar |
|  Logradouro        | varchar |
|  Bairro            | varchar |
|  Cidade            | varchar |
|  Estado            | varchar |
|  Numero            | varchar |
|  Complemento       | varchar |
|  Ocupado           | bit |

------------------------------------------------------------------------

## Tabela: Reservas

|  Campo           | Tipo |
|  :--- | :---: |
|  Id              | int |
|  IdCliente       | int |
|  IdApartamento   | int |
|  Ativo           | bit |

**IdCliente é chave estrangeira referênciando Clientes (Id)**

**IdApartamento é chave estrangeira referênciando Apartamentos (Id)**

------------------------------------------------------------------------

## Tabela: Vendas

  Campo           | Tipo |
|  :--- | :---: |
|  Id              | int |
|  IdCliente       | int |
|  IdApartamento   | int |
|  IdVendedor      | int |

**IdCliente é chave estrangeira referênciando Clientes (Id)**

**IdApartamento é chave estrangeira referênciando Apartamentos (Id)**

**IdVendedor é chave estrangeira referênciando Vendedores (Id)**

------------------------------------------------------------------------

# Como gerar e utilizar o token JWT

## 1 Autenticação

O token JWT é gerado através do endpoint de login:

    POST /api/login

Após autenticar, a API retorna um token:

``` json
{
  "token": "JWT_TOKEN_AQUI",
  "nome": "Nome do Vendedor"
}
```

Para fins de teste, existem 3 vendedores pré cadastrados no banco de dados:

|  Cpf             | Senha |
|  :--- | ---: |
|  11111111111     | Teste123@ |
|  22222222222     | SenhaForte478! |
|  33333333333     | PasswordTest994# |

------------------------------------------------------------------------

## 2 Utilização do token

O token deve ser enviado no header das requisições protegidas:

    Authorization: Bearer SEU_TOKEN

Exemplo:

``` http
GET /api/clientes
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI...
```

------------------------------------------------------------------------

# Considerações e decisões técnicas

Aqui deixo as considerações e decisões que foram tomadas durante o desenvolvimento:

## Backend

- Escolhi utilizar o **Clean Architecture** por se tratar de uma arquitetura bem limpa e bastante útil para projetos pequenos como esse.
- Junto dele para facilitar nas injeções de dependência resolvi utilizar a lib Mediatr, que funciona muito bem pra essa arquitetura e evita 
    uma grande injeção de serviços dentro dos construtores para as controllers.
- As camadas foram separadas da seguinte forma: 
    -   Domain 
    -   Application 
    -   Infrastructure 
    -   WebAPI
- Para validações de input foi utilizada a lib FluentValidation, que facilita a criação das regras e a implementação de forma automática para os handlers.
- Autenticação utilizando JWT token, definindo com que somente requisições autenticadas podem acessar os controllers da aplicação.
- A autenticação é feita também utilizando PasswordHasher da microsoft para melhor segurança.
- Para fazer o controle de login, decidi criar uma tabela Vendedores com informações de cada um, além dos dados necessários para efetuar o login. Essa tabela
    não era prevista no cenário inicial, porém foi fundamental para o projeto, pois em um cenário real os funcionários da empresa teriam algum tipo de registro com suas credenciais.
- Para grande maioria das tabelas, tomei a liberdade de adicionar campos que existiriam em um sistema de imobiliária real, para que mesmo um projeto MVP possa ter uma melhor apresentação.
- Criada uma classe de controle de excessões (middleware) e classes de excessões com tratamento de status code correto para cada, de forma que os retornos sejam previsíveis, 
    e nenhuma stack trace seja exibida ao cliente.
- Testes unitários criados utilizando xUnit, com auxílio de libs como Faker e Fixture, cobrindo as partes principais do projeto (Auth, Handlers, Models e Validators).

### Melhorias futuras

Algumas melhorias que gostaria de ter implementado porém pelo escopo e tempo, achei melhor deixar como débito técnico:

- Implementar melhor DDD, com ValueObjects para CPF, Email, Celular e outros tipos de dados necessários.
- Implementar validações mais acertivas e mais completas, e até mesmo uma busca de CPF via api externa (exemplo SERPRO), afim de validar a veracidade do CPF informado.
- Implementar logs cercando toda a camada de application, utilizando a própria lib da microsoft Loggers, visando uma melhor rastreabilidade de eventos e erros do sistema.
- Implementar paginação e rotas com filtragem para maior flexibilidade de uso das mesmas.
- Implementar refresh token para renovação da autenticação.

------------------------------------------------------------------------

## Frontend

- Desenvolvido utilizando **React + Vite**.
- O consumo de APIs é feito utilizando o **Axios**.
- Middleware para tratamento de erros, para que o sistema tenha mensagens padronizadas.
- Interface simples e funcional para ilustrar o fluxo proposto no cenário, sem foco em design ou usabilidade.
- Controle automático de sessão utilizando localstorage para gravar o token do usuário atual.

### Melhorias futuras

- Melhor padronização de identidade visual (cores e logos).
- Melhor design no geral do site.
- Uma melhor arquitetura e organização da estrutura dos arquivos.

------------------------------------------------------------------------

## Infraestrutura

- Projeto containerizado com **Docker**.
- Orquestração utilizando **Docker Compose**
- Utilização do **Nginx como Reverse Proxy**
- Banco de dados **SQL Server**


# Observações

Este projeto foi desenvolvido como um **MVP funcional**, com foco em
demonstrar:

-   Arquitetura limpa
-   Integração frontend/backend
-   Fluxos completos de negócio
-   Containerização da aplicação

O foco do projeto foi muito mais voltado ao backend e infraestrutura do que frontend, por isso há muito espaço de melhoria dentro do projeto react, 
  porém ele serve muito bem para ilustrar o cenário de utilização do negócio e exemplifica muito bem as regras de cada módulo do sistema.

------------------------------------------------------------------------

# Autor

Gabriel Carnevali