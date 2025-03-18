import { useEffect, useState } from "react"; // Importa os hooks necessários
import "./CadastroTransacao.css"; // Importa os estilos específicos para o cadastro de transações
import Lixo from "../../assets/lixo.svg"; // Ícone de exclusão
import Lapis from "../../assets/Lapis.svg"; // Ícone de edição
import api from "../../services/api"; // Importa o serviço de API para interação com o backend

function CadastroTransacao() {
  // Estados para armazenar informações das transações, descrição, valor, tipo e pessoa
  const [transacoes, setTransacao] = useState([]); // Armazena todas as transações
  const [descricao, setDescricao] = useState(""); // Armazena a descrição da transação
  const [valor, setValor] = useState(""); // Armazena o valor da transação
  const [tipo, setTipo] = useState("Despesa"); // define o tipo da transação (Despesa ou Receita)
  const [pessoaId, setPessoaId] = useState(""); // Armazena o id da pessoa associada à transação
  const [pessoas, setPessoas] = useState([]); // Armazena todas as pessoas cadastradas
  const [pessoaSelecionada, setPessoaSelecionada] = useState(null); // Armazena a pessoa selecionada para a transação

  // Estados para controle de erro nos campos
  const [erroDescricao, setErroDescricao] = useState(false); // Erro na descrição
  const [erroValor, setErroValor] = useState(false); // Erro no valor
  const [erroPessoaId, setErroPessoaId] = useState(false); // Erro na pessoa selecionada

  // Estados para modo de edição
  const [modoEdicao, setModoEdicao] = useState(false); // Indica se está no modo de edição
  const [idEditando, setIdEditando] = useState(null); // Armazena o ID da pessoa que está sendo editada

  // Função assíncrona para buscar todas as transações da API
  async function getTransacao() {
    try {
      const transacaoFromApi = await api.get("/Transacao"); // Buscar as transações
      setTransacao(transacaoFromApi.data); // Atualiza o estado com as transações retornadas
    } catch (error) {
      console.error("Erro ao obter transação:", error); // Exibe erro no console em caso de falha
    }
  }

  // Função para obter a lista de pessoas do backend
  async function getPessoas() {
    try {
      const pessoaFromApi = await api.get("/Pessoa"); // Requisição GET para buscar as pessoas
      setPessoas(pessoaFromApi.data); // Atualiza o estado com os dados retornados
    } catch (error) {
      console.error("Erro ao obter pessoas:", error); // Exibe erro no console em caso de falha
    }
  }

  // Função para criar uma nova transação
  async function createTransacao() {
    // Valida os campos antes de prosseguir
    if (!validarCampos()) {
      alert(
        "O(s) campo(s) em vermelho é(são) obrigatório(s) e devem estar corretos!"
      );
      return;
    }

    try {
      // Lógica para definir o tipo de transação baseado na idade da pessoa
      const tipoConvertido =
        pessoaSelecionada && pessoaSelecionada.idade < 18
          ? 1 // Menores de idade só podem registrar "Despesa" (1)
          : tipo === "Despesa"
          ? 1 // "Despesa" é representada por 1
          : 0; // "Receita" é representada por 0

      const newTransacao = {
        descricao,
        valor: parseFloat(valor).toFixed(2), // Formata o valor para duas casas decimais
        tipo: tipoConvertido,
        pessoaId: parseInt(pessoaId), // Converte o id da pessoa para inteiro
      };

      await api.post("/Transacao", newTransacao); // Envia a transação para a API

      // Limpa os campos após o cadastro
      setDescricao("");
      setValor("");
      setTipo("Despesa");
      setPessoaId("");
      getTransacao(); // Atualiza a lista de transações
    } catch (error) {
      console.error("Erro ao criar transação:", error.response?.data || error); // Exibe erro no console
      alert("Erro ao criar transação!"); // Exibe um alerta de erro
    }
  }

  // Função para atualizar uma nova transação
  async function putTransacao() {
    //validação dos campos
    if (!validarCampos()) {
      alert(
        "O(s) campo(s) em vermelho é(são) obrigatório(s) e devem estar corretos!"
      );
      return;
    }

    try {
      // Atualiza os dados na API
      await api.put(`/Transacao/${idEditando}`, {
        descricao,
        valor: parseFloat(valor).toFixed(2),
        tipo: tipo === "Despesa" ? 1 : 0,
        pessoaId: parseInt(pessoaId),
      });

      resetForm(); // reseta o formulario
      getTransacao(); //atualiza a lista de transações
    } catch (error) {
      console.error(
        "Erro ao atualizar transação:",
        error.response?.data || error
      ); // Exibe erro no console
      alert("Erro ao deletar transação!"); // Exibe um alerta de erro
    }
  }

  // Função para deletar transações
  async function deleteTransacao(id) {
    try {
      await api.delete(`/Transacao/${id}`); // deleta a transação na API
      getTransacao(); //atualiza a lista de transações
    } catch (error) {
      console.error(
        "Erro ao deletar transação:",
        error.response?.data || error
      ); // Exibe erro no console
      alert("Erro ao deletar transação!"); // Exibe um alerta de erro
    }
  }

  // Função para selecionar a pessoa associada à transação
  function handlePessoaSelect(event) {
    const selectedPessoaId = event.target.value; // Pega o id da pessoa selecionada
    setPessoaId(selectedPessoaId); // Atualiza o id da pessoa

    // Busca a pessoa selecionada pela id
    const pessoa = pessoas.find((p) => p.id === parseInt(selectedPessoaId));
    setPessoaSelecionada(pessoa); // Atualiza a pessoa selecionada
  }

  // Verificação se os inputs foram feitos corretamente
  const validarCampos = () => {
    const descricaoValida = descricao.trim() !== ""; // descrição não pode ser nula
    const valorValido = !isNaN(valor) && parseFloat(valor) > 0; // valor deve ser maior q 1
    const pessoaValida = pessoaId.trim() !== ""; // a transação deve ser associada a uma pessoa

    // Reseta os erros de validação
    setErroDescricao(!descricaoValida);
    setErroValor(!valorValido);
    setErroPessoaId(!pessoaValida);

    return descricaoValida && valorValido && pessoaValida;
  };

  // Função para editar uma transação existente
  function editarTransacao(transacao) {
    // Define que estamos no modo de edição
    setModoEdicao(true);

    // Armazena o ID da transação que está sendo editada
    setIdEditando(transacao.id);

    // Preenche os campos do formulário com os dados da transação
    setDescricao(transacao.descricao);
    setValor(transacao.valor);

    // Define o tipo da transação como "Despesa" ou "Receita"
    // Aqui, assumimos que '1' representa "Despesa" e '0' representa "Receita"
    setTipo(transacao.tipo === 1 ? "Despesa" : "Receita");

    // Armazena o ID da pessoa relacionada à transação
    setPessoaId(transacao.pessoaId.toString());
  }

  // Função para resetar os campos do formulário
  function resetForm() {
    // Limpa os campos do formulário
    setDescricao("");
    setValor("");
    setTipo("Despesa"); // Define o tipo como "Despesa" por padrão
    setPessoaId(""); // Limpa o campo do ID da pessoa

    // Reseta o estado de modo de edição
    setModoEdicao(false);
    setIdEditando(null); // Limpa o ID da transação em edição

    // Reseta os erros de validação
    setErroDescricao(false);
    setErroValor(false);
    setErroPessoaId(false);
  }

  // Efeito para buscar as transações e pessoas ao carregar o componente
  useEffect(() => {
    getTransacao();
    getPessoas();
  }, []);

  return (
    <div className="container">
      <form
        onSubmit={(e) => {
          e.preventDefault(); // Impede o comportamento padrão de recarregar a página ao submeter o formulário
          createTransacao(); // Chama a função para criar uma nova transação
        }}
      >
        <div>
          <h1>{modoEdicao ? "Editar Transação" : "Cadastro de Transação"}</h1>
          <label>Pessoa referente à transação</label>
          <select
            value={pessoaId}
            onChange={handlePessoaSelect} // Atualiza o estado de pessoaId ao selecionar uma pessoa
            className={erroPessoaId ? "input-erro" : ""} // Aplica estilo de erro se houver erro
          >
            <option value="">Selecione uma pessoa*</option>
            {pessoas.map((pessoa) => (
              <option key={pessoa.id} value={pessoa.id}>
                {pessoa.nome} ({pessoa.idade} anos)
              </option>
            ))}
          </select>
        </div>
        <div>
          <label>Valor*</label>
          <input
            type="number"
            value={valor}
            onChange={(e) => setValor(e.target.value)} // Atualiza o estado de valor ao digitar
            className={erroValor ? "input-erro" : ""} // Aplica estilo de erro se houver
          />
        </div>
        <div>
          <label>Descrição*</label>
          <input
            type="text"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)} // Atualiza o estado de descricao ao digitar
            className={erroDescricao ? "input-erro" : ""} // Aplica estilo de erro se houver erro
          />
        </div>
        <div>
          <label>Tipo*</label>
          <select
            value={tipo}
            onChange={(e) => setTipo(e.target.value)} // Atualiza o estado de tipo ao selecionar
            disabled={pessoaSelecionada && pessoaSelecionada.idade < 18} // Desabilita a seleção de "Receita" para menores de 18
          >
            <option value="Despesa">Despesa</option>
            {!(pessoaSelecionada && pessoaSelecionada.idade < 18) && (
              <option value="Receita">Receita</option>
            )}
          </select>
        </div>
        {modoEdicao ? (
          <>
            <button type="button" onClick={putTransacao}>
              Salvar
            </button>
            <button type="button" onClick={resetForm}>
              Cancelar
            </button>
          </>
        ) : (
          <button type="button" onClick={createTransacao}>
            Cadastrar
          </button>
        )}
      </form>

      <h2>Transações Cadastradas</h2>
      <div className="tabela-container">
        {/* Tabela para exibir as transações cadastradas */}
        <table className="tabela-planilha">
          <thead>
            <tr>
              <th>Pessoa</th>
              <th>Valor</th>
              <th>Descrição</th>
              <th>Tipo</th>
              <th>Ação</th>
            </tr>
          </thead>
          <tbody>
            {transacoes.length === 0 ? (
              <tr className="no-transactions">
                <td colSpan="5" style={{ textAlign: "center" }}>
                  Nenhuma transação registrada
                </td>
              </tr>
            ) : (
              transacoes.map((transacao) => (
                <tr key={transacao.id}>
                  <td>{transacao.pessoa?.nome}</td>
                  <td>R$ {transacao.valor}</td>
                  <td className="descricao">{transacao.descricao}</td>
                  <td className={transacao.tipo === 1 ? "despesa" : "receita"}>
                    <strong>
                      {transacao.tipo === 1 ? "Despesa" : "Receita"}
                    </strong>
                  </td>
                  <td>
                    <button
                      className="btn-action"
                      onClick={() => editarTransacao(transacao)}
                    >
                      <img src={Lapis} alt="Editar" />
                    </button>
                    <button
                      className="btn-action"
                      onClick={() => deleteTransacao(transacao.id)}
                    >
                      <img src={Lixo} alt="Excluir" />
                    </button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default CadastroTransacao;