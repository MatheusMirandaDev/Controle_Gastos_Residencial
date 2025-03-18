# 🏠 Sistema de Gestão de Gastos Residenciais 💰

O **Sistema de Gestão de Gastos Residenciais** ajuda no controle financeiro doméstico de forma simples e eficiente. Ele permite o gerenciamento de receitas e despesas individuais, mantendo o saldo geral da residência sempre atualizado.

---

## 📌 Tecnologias Utilizadas

### 🌐 Frontend

- **React**
- **JavaScript**
- **CSS**
- **Vite**

### 🖥 Backend

- **.NET 8.0**
- **C#**
- **Entity Framework Core (SQL Server)**
- **Swagger** para documentação da API

---

## 🔗 Funcionalidades

### 🔹 Gerenciamento de Pessoas

- Cadastro, edição e remoção de pessoas no sistema.
- **Atenção**: A exclusão de uma pessoa **remove todas as suas transações**.
- **Restrição**: Pessoas menores de **18 anos** podem apenas registrar **despesas**.

### 🔹 Controle de Transações

- Cadastro de receitas e despesas associadas a uma pessoa.
- Edição e exclusão de transações já registradas.

### 🔹 Relatórios e Totais

- Visualização do saldo individual de cada pessoa.
- Consulta do saldo geral da residência.

---

## 🛠️ Como Usar o Sistema

### 1️⃣ Cadastro de Pessoas

- Acesse a aba **Cadastro de Pessoas**.
- Adicione, edite ou exclua usuários.

### 2️⃣ Cadastro de Transações

- Vá para **Cadastro de Transações**.
- Registre receitas e despesas.

### 3️⃣ Consulta de Totais

- Acesse **Consulta de Totais** para visualizar os saldos individuais e o total da residência.

---

## 📖 Documentação da API (Swagger)

A API possui uma documentação interativa via **Swagger**. Para acessá-la, execute o backend e abra:

🔗 [Swagger UI](https://localhost:7130/index.html)

### 🔹 Endpoints

#### 📌 Consulta de Totais

- `GET /ConsultaTotais` → Obtém os totais individuais.
- `GET /ConsultaTotais/gerais` → Obtém o saldo geral.

#### 📌 Pessoa

- `POST /Pessoa` → Cria uma nova pessoa.
- `GET /Pessoa` → Lista todas as pessoas.
- `PUT /Pessoa/{id}` → Edita uma pessoa.
- `DELETE /Pessoa/{id}` → Remove uma pessoa e suas transações.

#### 📌 Transação

- `POST /Transacao` → Registra uma nova transação.
- `GET /Transacao` → Lista todas as transações.
- `PUT /Transacao/{id}` → Edita uma transação.
- `DELETE /Transacao/{id}` → Remove uma transação.

---

## 🛠 Como Rodar o Projeto

### 🖥 Backend (API)

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
   dotnet run
   ```

O backend estará disponível em: https://localhost:7130

### 🌐 Frontend

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
