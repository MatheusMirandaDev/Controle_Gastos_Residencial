# Backend - API (.NET 8)

Esta é a API do **Sistema de Gestão de Gastos Residenciais**, responsável por armazenar e processar os dados de usuários, transações e cálculos financeiros.

## 📌 Tecnologias Utilizadas
- **.NET 8.0**
- **C#**
- **Entity Framework Core (SQL Server)**
- **Swagger** para documentação da API

## 📖 Documentação da API
A API conta com uma documentação interativa via **Swagger**.

**Acesse em:**  
🔗 [https://localhost:7130/swagger]

## 🔗 Principais Endpoints

### 🔹 Pessoas
- **POST /Pessoa** → Cadastra uma nova pessoa.
- **GET /Pessoa** → Lista todas as pessoas cadastradas.
- **DELETE /Pessoa/{id}** → Remove uma pessoa do sistema.

### 🔹 Transações
- **POST /Transacao** → Registra uma transação (despesa ou receita).
- **GET /Transacao** → Retorna todas as transações cadastradas.

### 🔹 Totais
- **GET /ConsultaTotais** → Obtém os totais individuais (Receitas, Despesas, Saldo).
- **GET /ConsultaTotais/gerais** → Obtém os totais gerais (Receitas, Despesas, Saldo).

---

## 🛠️ Configuração do Banco de Dados

O projeto utiliza **Entity Framework Core** e o banco de dados SQL Server.  

Caso precise rodar as migrações manualmente, utilize os comandos:  
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```