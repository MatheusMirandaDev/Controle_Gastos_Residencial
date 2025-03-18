import { useEffect, useState } from "react"; // Importa os hooks necessários
import "./CadastroPessoa.css"; // Importa os estilos específicos para o cadastro de pessoas
import Lixo from "../../assets/lixo.svg"; // Ícone de exclusão
import Lapis from "../../assets/Lapis.svg"; // Ícone de edição
import api from "../../services/api"; // Importa o serviço de API para interação com o backend

function Cadastro_Pessoa() {
  // Estados para armazenar as informações das pessoas
  const [pessoas, setPessoas] = useState([]); // Lista de pessoas cadastradas
  const [idade, setIdade] = useState(""); // Armazena a idade digitada
  const [nome, setNome] = useState(""); // Armazena o nome digitado

  // Estados para controle de erro nos campos
  const [erroNome, setErroNome] = useState(false); // Indica erro no campo nome
  const [erroIdade, setErroIdade] = useState(false); // Indica erro no campo idade

  // Estados para modo de edição
  const [modoEdicao, setModoEdicao] = useState(false); // Indica se está no modo de edição
  const [idEditando, setIdEditando] = useState(null); // Armazena o ID da pessoa que está sendo editada

  // Função para obter a lista de pessoas do backend
  async function getPessoas() {
    try {
      const pessoaFromApi = await api.get("/Pessoa"); // Requisição GET para buscar as pessoas
      setPessoas(pessoaFromApi.data); // Atualiza o estado com os dados retornados
    } catch (error) {
      console.error("Erro ao obter pessoas:", error); // Exibe erro no console em caso de falha
      
    }
  }

  // Função para criar uma nova pessoa
  async function createPessoa() {
    if (!validarCampos()) return; // Valida os campos antes de prosseguir

    try {
      await api.post("/Pessoa", { nome: nome.trim(), idade: Number(idade) }); // Envia os dados para a API
      resetForm(); // Reseta o formulário após o cadastro
      getPessoas(); // Atualiza a lista de pessoas
    } catch (error) {
      console.error("Erro ao criar pessoa:", error); // Exibe erro no console
      alert("Erro ao criar pessoa."); // Exibe alerta para
    }
  }

  // Função para atualizar uma pessoa existente
  async function putPessoa() {
    if (!validarCampos()) return; // Valida os campos antes de prosseguir

    try {
      await api.put(`/Pessoa/${idEditando}`, {
        nome: nome.trim(),
        idade: Number(idade),
      }); // Atualiza os dados na API
      resetForm(); // Reseta o formulário após a edição
      getPessoas(); // Atualiza a lista de pessoas
    } catch (error) {
      console.error("Erro ao atualizar pessoa:", error); // Exibe erro no console
      alert("Erro ao atualizar pessoa."); // Exibe alerta para o usuário
    }
  }

  // Função para deletar uma pessoa
  async function deletePessoa(id) {
    try {
      await api.delete(`/Pessoa/${id}`); // Envia a requisição DELETE para remover a pessoa
      getPessoas(); // Atualiza a lista após a exclusão
    } catch (error) {
      console.error("Erro ao deletar pessoa:", error); // Exibe erro no console
      alert("Erro ao deletar pessoa."); // Exibe alerta para o usuário
    }
  }

  // Função para validar os campos do formulário
  function validarCampos() {
    const nomeValido = nome.trim() !== ""; // Verifica se o nome foi preenchido
    const idadeValida =
      /^\d+$/.test(idade) && Number(idade) >= 1 && Number(idade) <= 122; // Valida se a idade é um número entre 1 e 122

    setErroNome(!nomeValido); // Define erro no nome se inválido
    setErroIdade(!idadeValida); // Define erro na idade se inválida

    if (!nomeValido || !idadeValida) {
      alert("Nome e idade são obrigatórios e devem ser válidos."); // Exibe alerta em caso de erro
      return false;
    }
    return true; // Retorna verdadeiro se tudo estiver válido
  }

  // Função para preencher os campos ao editar uma pessoa
  function editarPessoa(pessoa) {
    setModoEdicao(true); // Ativa o modo de edição
    setIdEditando(pessoa.id); // Armazena o ID da pessoa que está sendo editada
    setNome(pessoa.nome); // Preenche o campo nome
    setIdade(String(pessoa.idade)); // Preenche o campo idade
  }

  // Função para resetar o formulário
  function resetForm() {
    setNome(""); // Limpa o campo nome
    setIdade(""); // Limpa o campo idade
    setModoEdicao(false); // Desativa o modo de edição
    setIdEditando(null); // Reseta o ID da pessoa em edição
  }

  // Efeito para carregar a lista de pessoas ao montar o componente
  useEffect(() => {
    getPessoas();
  }, []);

  return (
    <div className="container">
      <form>
        <h1>{modoEdicao ? "Editar Pessoa" : "Cadastro de Pessoa"}</h1>
        <input
          className={erroNome ? "erro" : ""}
          placeholder="Nome*"
          type="text"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          required
        />
        <input
          className={erroIdade ? "erro" : ""}
          placeholder="Idade*"
          type="text"
          value={idade}
          onChange={(e) => setIdade(e.target.value)}
          required
        />
        {modoEdicao ? (
          <>
            <button type="button" onClick={putPessoa}>
              Salvar
            </button>
            <button type="button" onClick={resetForm}>
              Cancelar
            </button>
          </>
        ) : (
          <button type="button" onClick={createPessoa}>
            Cadastrar
          </button>
        )}
      </form>
      <h2>Pessoas Cadastradas</h2>
      {pessoas.map((pessoa) => (
        <div key={pessoa.id} className="card">
          <div>
            <p>
              Nome: <span>{pessoa.nome}</span>
            </p>
            <p>
              Idade: <span>{pessoa.idade}</span>
            </p>
          </div>
          <button onClick={() => editarPessoa(pessoa)}>
            <img src={Lapis} alt="Editar" />
          </button>
          <button onClick={() => deletePessoa(pessoa.id)}>
            <img src={Lixo} alt="Excluir" />
          </button>
        </div>
      ))}
    </div>
  );
}

export default Cadastro_Pessoa;
