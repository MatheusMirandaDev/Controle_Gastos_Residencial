# Sistema de Gestão de Gastos Residenciais

O **Sistema de Gestão de Gastos Residenciais** foi desenvolvido para ajudar no controle das finanças domésticas de maneira simples e eficiente. Ele permite o gerenciamento de despesas e receitas de cada pessoa da casa, auxiliando no controle do saldo geral.

## Tecnologias Utilizadas

### Frontend

- **React**
- **JavaScript**
- **CSS**
- **Vite**

### Backend

- **.NET 8.0**
- **C#**
- **Entity Framework Core (SQL Server)**
- **Swagger** para documentação da API

---

## Funcionalidades

### Cadastro de Pessoas

- Criação, visualização e remoção de pessoas cadastradas no sistema.
- **Importante**: Ao excluir uma pessoa, todas as suas transações são removidas automaticamente.
- **Importante**: Pessoas menores de 18 anos só podem registrar **despesas**, não sendo permitido o cadastro de **receitas**.

### Cadastro de Transações

- Registra receitas e despesas, associando cada transação a uma pessoa.
- **Importante**: Para pessoas menores de 18 anos, apenas transações do tipo **despesa** podem ser registradas.

### Consulta de Totais

- Visualiza o total de receitas, despesas e o saldo individual de cada pessoa.
- Visualiza o saldo geral da residência.

---

## Como Usar o Sistema

### 1. Cadastro de Pessoas

- Adicione uma pessoa ao sistema.
- No topo da tela, clique em **Cadastro de Pessoas** para adicionar novas pessoas ao sistema.
- Além de adicionar, você poderá visualizar ou excluir.

### 2. Cadastro de Transações

- Cadastre as transações (despesas ou receitas) para cada pessoa.
- No topo da tela, clique em **Cadastro de Transações**.
- Na aba de transações, você poderá visualizar todas as transações registradas e seus respectivos responsáveis.

### 3. Consulta de Totais

- Consulte os totais de receitas, despesas e saldo de cada pessoa, além do total geral da residência.
- Para acessar, clique em **Consulta de Totais** no topo da tela.

---

## Documentação da API (Backend)

A documentação da API foi gerada utilizando o **Swagger**. Para explorar os endpoints da API, rode o a API e acesse:

```bash
https://localhost:7130/swagger
```

### Endpoints

#### ConsultaTotais

- **GET /ConsultaTotais**
  - Obtém os totais de cada pessoa (Receitas, Despesas e Saldo).
- **GET /ConsultaTotais/gerais**
  - Obtém os totais gerais (Receitas, Despesas, Saldo).

#### Pessoa

- **POST /Pessoa**

  - Cria uma nova pessoa.

- **GET /Pessoa**

  - Obtém uma lista de todas as pessoas cadastradas.

- **DELETE /Pessoa/{id}**
  - Remove uma pessoa do banco de dados com base no ID fornecido.

#### Transacao

- **POST /Transacao**

  - Cria uma nova transação.

- **GET /Transacao**
  - Obtém todas as transações cadastradas.

---

## Como Rodar o Projeto

### Backend (API)

1. Verifique se o .NET 8.0 está instalado:
   ```bash
   dotnet --list-sdks
   ```
   Se o .NET 8.0 não estiver na lista, baixe e instale em:
   https://dotnet.microsoft.com/en-us/download/dotnet/8.0
2. Clone o repositório do backend:
   ```bash
   git clone <url-do-repositorio-backend>
   ```
3. Navegue até a pasta do projeto backend:
   ```bash
    cd ControleDeGastos
   ```
4. Instale as dependências:

   ```bash
   dotnet restore
   ```
5. Crie e aplique as migrações do banco de dados:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
6. Inicie o servidor backend:
   ```bash
   dotnet build ControleDeGastos.sln
   dotnet run --project ControleDeGastos/ControleDeGastos.API.csproj
   ```

O backend estará disponível em: https://localhost:7130

### Frontend

1. Clone o repositório do frontend:
   ```bash
   git clone <url-do-repositorio-frontend>
   ```
2. Navegue até a pasta do projeto frontend:
   ```bash
    cd controle-gastos-frontend
   ```
3. Instale as dependências:
   ```bash
   npm install
   ```
4. Inicie o servidor frontend:
   ```bash
   npm run dev
   ```

O frontend estará disponível em: http://localhost:5173/
