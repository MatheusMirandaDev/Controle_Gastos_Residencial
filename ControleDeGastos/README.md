# Backend - API (.NET 8)

Esta é a API do **Sistema de Gestão de Gastos Residenciais**, responsável por armazenar e processar os dados de usuários, transações e cálculos financeiros.

---

## 📌 Tecnologias Utilizadas

- **.NET 8.0**
- **C#**
- **Entity Framework Core (SQL Server)**
- **Swagger** para documentação da API

---

## 📖 Documentação da API

A API conta com uma documentação interativa via **Swagger**.

**Acesse em:**  
🔗 https://localhost:7130/index.html

---

## 🔗 Endpoints Principais

### 📌 Pessoas

- `POST /Pessoa` → Cadastra uma nova pessoa.
- `GET /Pessoa` → Lista todas as pessoas cadastradas.
- `PUT /Pessoa/{id}` → Atualiza os dados de uma pessoa.
- `DELETE /Pessoa/{id}` → Remove uma pessoa do sistema e suas transações.

### 📌 Transações

- `POST /Transacao` → Registra uma nova transação (despesa ou receita).
- `GET /Transacao` → Retorna todas as transações cadastradas.
- `PUT /Transacao/{id}` → Atualiza os dados de uma transação.
- `DELETE /Transacao/{id}` → Remove uma transação pelo ID.

### 📌 Totais

- `GET /ConsultaTotais` → Obtém os totais individuais (Receitas, Despesas e Saldo).
- `GET /ConsultaTotais/gerais` → Obtém os totais gerais (Receitas, Despesas e Saldo).

---

## 🛠️ Configuração do Banco de Dados

O projeto utiliza **Entity Framework Core** e o banco de dados SQL Server.

### 🔹 Ajustando a Conexão

Antes de rodar a API, certifique-se de configurar a **string de conexão** no arquivo `appsettings.json`:

```json
    "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ControleGastosDB;Trusted_Connection=True;"
    }
```

### 🔹 Rodando as migrations

Caso precise rodar as migrações manualmente, utilize os comandos:  
 `bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    `
