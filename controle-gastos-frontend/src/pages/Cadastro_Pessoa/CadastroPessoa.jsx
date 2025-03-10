import { useEffect, useState } from "react";
import "./CadastroPessoa.css";
import Lixo from "../../assets/lixo.svg";
import api from "../../services/api";

function Cadastro_Pessoa() {
  // Estado para armazenar a lista de pessoas cadastradas
  const [pessoa, setUsers] = useState([]);
  // Estados para armazenar os dados do formulário
  const [idade, setIdade] = useState("");
  const [nome, setNome] = useState("");
  // Estados para controle de erros nos campos de nome e idade
  const [erroNome, setErroNome] = useState(false);
  const [erroIdade, setErroIdade] = useState(false);

  // Função para obter a lista de pessoas cadastradas da API
  async function getPessoa() {
    try {
      const pessoaFromApi = await api.get("/Pessoa");
      setUsers(pessoaFromApi.data); // Atualiza o estado com a resposta da API
    } catch (error) {
      console.error("Erro ao obter pessoas:", error); // Tratamento de erro se a API falhar
    }
  }

  // Função que cria uma nova pessoa no sistema
  async function createPessoa() {
    const idadeNumero = Number(idade); // Converte o valor da idade para número
    const nomeValido = nome.trim() !== ""; // Verifica se o nome não está vazio
    const idadeValida = /^\d+$/.test(idade) && idadeNumero >= 1 && idadeNumero <= 122; // Valida se a idade está no formato correto e dentro do intervalo aceitável

    setErroNome(!nomeValido); // Atualiza o estado de erro do nome
    setErroIdade(!idadeValida); // Atualiza o estado de erro da idade

    if (!nomeValido || !idadeValida) {
      alert("Campo obrigatório para preenchimento!"); // Exibe um alerta se os dados estiverem inválidos
      return;
    }

    try {
      await api.post("/Pessoa", {
        nome: nome.trim(), // Nome da pessoa
        idade: idadeNumero, // Idade da pessoa
      });
      getPessoa(); // Atualiza a lista de pessoas após o cadastro
      setNome(""); // Limpa o campo de nome
      setIdade(""); // Limpa o campo de idade
    } catch (error) {
      console.error("Erro ao criar pessoa:", error); // Tratamento de erro no caso de falha ao criar a pessoa
    }
  }

  // Função para deletar uma pessoa do sistema
  async function deletePessoa(id) {
    console.log("Tentando deletar a pessoa com ID:", id);
    try {
      if (id) {
        const response = await api.delete(`/Pessoa/${id}`);
        console.log("Resposta do backend:", response);
        // Atualiza a lista de pessoas após a exclusão
        setUsers(pessoa.filter((pessoa) => pessoa.id !== id));
      } else {
        console.log("ID inválido:", id); // Exibe erro caso o ID seja inválido
      }
    } catch (error) {
      console.error(
        "Erro ao deletar:",
        error.response ? error.response.data : error.message
      );
    }
    getPessoa(); // Recarrega a lista de pessoas
  }

  // Hook para buscar a lista de pessoas assim que o componente for montado
  useEffect(() => {
    getPessoa();
  }, []);

  // Função para validar a idade (apenas números entre 1 e 122)
  function limiteIdade(event) {
    const valor = event.target.value;
    if (
      /^\d*$/.test(valor) && // Verifica se o valor contém apenas números
      (valor === "" || (Number(valor) >= 1 && Number(valor) <= 122)) // Verifica se a idade está no intervalo válido
    ) {
      setIdade(valor); // Atualiza o estado com a idade
      setErroIdade(false); // Limpa o erro da idade
    }
  }

  // Função para permitir apenas caracteres alfabéticos e espaços no campo de nome
  function caracteresPermitidos(event) {
    const valor = event.target.value;
    if (/^[A-Za-zÀ-ÖØ-öø-ÿ\s]*$/.test(valor)) {
      setNome(valor); // Atualiza o estado com o nome
      setErroNome(false); // Limpa o erro do nome
    }
  }

  return (
    <div className="container">
      <form>
        <h1>Cadastro de Pessoa</h1>
        {/* Campo para nome da pessoa */}
        <input
          className={erroNome ? "erro" : ""}
          placeholder="Nome*"
          type="text"
          name="name"
          value={nome}
          onChange={caracteresPermitidos} // Valida caracteres permitidos
          required
        />
        {/* Campo para idade da pessoa */}
        <input
          className={erroIdade ? "erro" : ""}
          placeholder="Idade*"
          type="text"
          name="age"
          value={idade}
          onChange={limiteIdade} // Valida limite de idade
          required
        />
        {/* Botão para criar a pessoa */}
        <button type="button" onClick={createPessoa}>
          Cadastrar
        </button>
      </form>
      <h2>Pessoas Cadastradas</h2>
      {/* Exibe a lista de pessoas cadastradas */}
      {pessoa.map((pessoa, index) => (
        <div key={pessoa.id || index} className="card">
          <div>
            <p>
              Nome: <span>{pessoa.nome}</span>
            </p>
            <p>
              Idade: <span>{pessoa.idade}</span>
            </p>
          </div>
          {/* Botão para deletar a pessoa */}
          <button
            onClick={() => {
              console.log("ID da pessoa para deletar:", pessoa.id);
              deletePessoa(pessoa.id); // Chama a função de deletar
            }}
          >
            <img src={Lixo} />
          </button>
        </div>
      ))}
    </div>
  );
}

export default Cadastro_Pessoa;
